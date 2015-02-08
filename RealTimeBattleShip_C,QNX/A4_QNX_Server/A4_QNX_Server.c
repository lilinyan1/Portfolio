/*
 * FILE:             A4_QNX_Server.c
 * PROJECT:          PROG2150 - Real-Time Operating Systems - Assignment #4
 * PROGRAMMER:       Linyan Becky Li & Kyle Stevenson
 * AVAILABLE DATE:   12-12-2014
 * DESCRIPTION:      Server:
 * 						- receive request from client via message passing
 * 						- send commands  to the game engine via shared memory
 * 						- read updated x y coordinates via shared memory and conditional variable
 * 						- send updated x y coordinates via message passing
 */

// include files
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <sys/neutrino.h>
#include <sys/dispatch.h>
#include <pthread.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>

#include <string.h>
#include <unistd.h>
#include <sys/mman.h>

#include "battleship_shmem.h"


#define PROGNAME "Server: "

name_attach_t *att_msg;

int connect_count = 0;

char server_name[STRING_MAX] = "";

int main(int argc, char *argv[]) {

	int i = 0;
	int rcvid;
	server_reply_msg_t reply_buf;  //reply messages to be sent to client
	client_send_msg_type rcv_buf;	//messaqges to receive from the client
	struct _msg_info msg_info;

	int fd = 0;
	battleship_shmem_t* dataPtr;	//pointer to shared mem structure

	/* open/create the shared memory */
	fd = shm_open("/myshmemobject", O_RDWR, S_IRWXU);

	if (fd == -1) {
		printf("%s: error creating the shared memory object. %s.\n", PROGNAME,
				strerror(errno));
		exit(EXIT_FAILURE);
	}

	/* Get a pointer to the shared memory object */
	dataPtr = mmap(0, sizeof(battleship_shmem_t), PROT_READ | PROT_WRITE, MAP_SHARED, fd, 0);

	// command argument is optional, the default server name is server
	if (argc == 1) {
		strcpy(server_name, "server");
	} else if (argc == 2) {
		strcpy(server_name, argv[1]);
	} else {
		printf("Instruction: MeleeBattleServer [server_name]\n");
		exit(EXIT_FAILURE);
	}

	// set buffer so string in printf only show up when there is /n at the end of it, or when the buffer is full
	setvbuf(stdout, NULL, _IOLBF, 0);

	//create server with a name attached for client's connection
	att_msg = name_attach(NULL, server_name, 0);
	if (NULL == att_msg) {
		perror(PROGNAME "server_name name_attach()");
		exit(EXIT_FAILURE);
	}

//1.			keep receiving client'msg (until disconnect )
	while (1) {
		int i = 0;
		printf(PROGNAME "receiving msg\n");

		//wait here for client message
		rcvid = MsgReceive(att_msg->chid, &rcv_buf, sizeof(rcv_buf),&msg_info);

		if (-1 == rcvid) {

			continue;
		}

		// received a regular message
		else if (rcvid > 0) {
			printf(PROGNAME "got msg from %d\n", rcvid);

			switch (rcv_buf) {

			//		parse client's msg to get command for server
			// send command to game engine through shared memory
			//		use CONDVAR to trigger game engine
			// server is blocked until new cordinates have been updated (CONDVAR?)

			// RECIEVED CLIENT CONNECTION MESSAGE
			case eClientConnection:
				//acknowledge client when reaching max client
				printf(
						PROGNAME "got connection msg from client %X, add it to client table\n",
						rcvid);
				connect_count++;
				if (connect_count == CLIENT_MAX) {
					printf(
							PROGNAME "Reach maximum limit on the client number\n");
					reply_buf.type = eServerAck;
					strcpy(reply_buf.reply.reply_str,
							"Server: Reach maximum limit on the client number");
					MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
					break;
				}

				//acknowledge connection
				strcpy(reply_buf.reply.reply_str, "Server: welcome\n");
				//reply to client with welcome message
				MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
				printf(PROGNAME "sent welcome message to client: %X\n", rcvid);
				break;

		// RECIEVED CLIENT DISCONNECTION MESSAGE
			case eClientDisconnect:
				printf(PROGNAME "Got disconnected msg from %d\n", rcvid);
				// decrease connection count
				connect_count--;

				// reply to client with disconnection acknowledgement
				strcpy(reply_buf.reply.reply_str,
						"Server: You're disconnected");
				MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
				break;

				// RECIEVED CLIENT I'M HERE MESSAGE
			case eClientIsAlive:
				printf("Got status request msg \n");
				//put the ship_info from each client into the reply message
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
				break;

				// RECEIVED CLIENT MOVE UP MESSAGE
			case eClientMoveUp:
				printf("Got move up msg\n");

				//now add to shared memory and notify game engine
				pthread_mutex_lock (&dataPtr->myshmemmutex);
				dataPtr->command = eClientMoveUp;
				dataPtr->infoToCheck = 0;
				//wake up the game engine
				pthread_cond_signal (&dataPtr->condvar);

				while (dataPtr->infoToCheck != 1) {										// server wait for game engine to update coordinators
					pthread_cond_wait (&dataPtr->condvar, &dataPtr->myshmemmutex);     	// unlock mutex while waiting
				}
				//safe to read from shared memory
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				 //process coordinators

				dataPtr->infoToCheck = 0;
				//unlock mutex
				pthread_mutex_unlock (&dataPtr->myshmemmutex);

				//reply to client
				printf ("Server:  got data from game engine\n");
				reply_buf.type = eServerClientStatus;
				MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
				break;

				// RECEIVED CLIENT MOVE DOWN MESSAGE
			case eClientMoveDown:
				printf("Got move down msg\n");
				//now add to shared memory and notify game engine
				pthread_mutex_lock (&dataPtr->myshmemmutex);
				dataPtr->command = eClientMoveDown;
				dataPtr->infoToCheck = 0;
				pthread_cond_signal (&dataPtr->condvar);

				while (dataPtr->infoToCheck != 1) {										// server wait for game engine to update coordinators
					pthread_cond_wait (&dataPtr->condvar, &dataPtr->myshmemmutex);     	// unlock mutex while waiting
				}
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				 //process coordinators

				dataPtr->infoToCheck = 0;

				pthread_mutex_unlock (&dataPtr->myshmemmutex);
				//reply to client
				reply_buf.type = eServerClientStatus;
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
				break;

				// RECEIVED CLIENT MOVE LEFT MESSAGE
			case eClientMoveLeft:
				printf("Got move left msg\n");
				//now add to shared memory and notify game engine
				pthread_mutex_lock (&dataPtr->myshmemmutex);
				dataPtr->command = eClientMoveLeft;
				dataPtr->infoToCheck = 0;
				pthread_cond_signal (&dataPtr->condvar);

				while (dataPtr->infoToCheck != 1) {										// server wait for game engine to update coordinators
					pthread_cond_wait (&dataPtr->condvar, &dataPtr->myshmemmutex);     	// unlock mutex while waiting
				}
				//read from shared mem
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				 //process coordinators

				dataPtr->infoToCheck = 0;

				pthread_mutex_unlock (&dataPtr->myshmemmutex);
				//reply to client
				reply_buf.type = eServerClientStatus;
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
				break;

				// RECEIVED CLIENT MOVE RIGHT MESSAGE
			case eClientMoveRight:
				printf("Got move right msg\n");
				//now add to shared memory and notify game engine
				pthread_mutex_lock (&dataPtr->myshmemmutex);
				dataPtr->command = eClientMoveRight;
				dataPtr->infoToCheck = 0;
				pthread_cond_signal (&dataPtr->condvar);

				while (dataPtr->infoToCheck != 1) {										// server wait for game engine to update coordinators
					pthread_cond_wait (&dataPtr->condvar, &dataPtr->myshmemmutex);     	// unlock mutex while waiting
				}
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				 //process coordinators

				dataPtr->infoToCheck = 0;

				pthread_mutex_unlock (&dataPtr->myshmemmutex);
				//reply to client
				reply_buf.type = eServerClientStatus;
				memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
				MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));
				break;

				// RECEIVED CLIENT AUTOMOVE MESSAGE
			case eClientMoveAuto:
				printf("Got auto move msg\n");
				   //now add to shared memory and notify game engine
					pthread_mutex_lock (&dataPtr->myshmemmutex);
					dataPtr->command = eClientMoveAuto;
					dataPtr->infoToCheck = 0;
					pthread_cond_signal (&dataPtr->condvar);

					while (dataPtr->infoToCheck != 1) {										// server wait for game engine to update coordinators
						pthread_cond_wait (&dataPtr->condvar, &dataPtr->myshmemmutex);     	// unlock mutex while waiting
					}
					memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
					 //process coordinators

					dataPtr->infoToCheck = 0;

					pthread_mutex_unlock (&dataPtr->myshmemmutex);
					//reply to cllient
					reply_buf.type = eServerClientStatus;
					memcpy(&reply_buf.reply.ship_info, &dataPtr->shared_ship_info, sizeof(ship_info_t));
					MsgReply(rcvid, 0, &reply_buf, sizeof(reply_buf));

				break;


			default:
				/* some other unexpect message */
				printf(PROGNAME "unexpect message type: %d\n", rcv_buf);
				MsgError(rcvid, ENOSYS);
				break;
			}
		}


	}

	close(fd); // Closing the file descriptor
	munmap(dataPtr, sizeof(battleship_shmem_t)); // removing the mapping
	shm_unlink("/myshmemobject"); //delete the shared memory object

	return 0;
}
