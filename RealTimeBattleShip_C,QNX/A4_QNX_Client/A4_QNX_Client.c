/*
 * FILE:             A4_QNX_Client.c
 * PROJECT:          PROG2150 - Real-Time Operating Systems - Assignment #4
 * PROGRAMMER:       Linyan Becky Li & Kyle Stevenson
 * AVAILABLE DATE:   12-12-2014
 * DESCRIPTION:      The client :
 *						- receive client's request from client UI via input pps
 *						- send client's request to server via message passing
 *						- receive updated x y coordinates from server via the message that server replied
 *						- send updated x y coordinates to client UI via output pps
 *
 */

// include files
#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <string.h>
#include <errno.h>
#include <sys/siginfo.h>
#include <sys/neutrino.h>
#include <sys/dispatch.h>
#include <sched.h>
#include "client_send_msg.h"

// constant
#define PROGNAME "Client: "

// receive buffer
union recv_msg {
	struct _pulse pulse;
	short type;
} recv_buf;

// prototypes & global
void *pulseHandler();

int save_server_CoId = -1;
int chid, rcvid, self_coid;
client_send_msg_type cMsg;
server_reply_msg_t sMsg;
char server_name[STRING_MAX] = "";
int fd2 = 0; // a file descriptor for the uiToClient PPS file

int main(int argc, char *argv[]) {

	char writeBuff[256] = { 0 }; // a buffer for contents from the clientToUi PPS file

	pthread_t pulseHandler_pId;
	int i = 0; // a iterator
	int fd = 0; // a file descriptor for the uiToClient PPS file
	int len = 0; // the length of a string
	int x = 0; // client's x coordinate
	int y = 0; // client's y coordinate

	//open the pps file to write to
	fd = open("/pps/uiToClient?wait", O_RDONLY | O_CREAT);
	fd2 = open("/pps/clientToUi", O_WRONLY | O_CREAT | O_TRUNC);

	// command argument is optional, the default server name is server
	if (argc == 1) {
		strcpy(server_name, "server");
	} else if (argc == 2) {
		strcpy(server_name, argv[1]);
	} else {
		printf("Instruction: MeleeBattleClient [server_name]\n");
		exit(EXIT_FAILURE);
	}

	// set buffer so string in printf only show up when there is /n at the end of it, or when the buffer is full
	setvbuf(stdout, NULL, _IOLBF, 0);

	// start connecting to server
	printf(PROGNAME "Connecting to server \n");
	save_server_CoId = name_open(server_name, NULL);
	while (save_server_CoId == -1) {
		sleep(1);
		save_server_CoId = name_open(server_name, 0);
	}

	printf("Got server's ID: %d\n", save_server_CoId);
	// create a channel to listen to pulse
	chid = ChannelCreate(0);
	if (-1 == chid) {
		perror(PROGNAME "ChannelCreate");
		exit(EXIT_FAILURE);
	}
	self_coid = ConnectAttach(0, 0, chid, _NTO_SIDE_CHANNEL, 0);
	if (-1 == self_coid) {
		perror(PROGNAME "ConnectAttach");
		exit(EXIT_FAILURE);
	}

	cMsg = eClientConnection;
	printf("sending msg to: %d\n", save_server_CoId);
	if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg, sizeof(sMsg))) {
		perror(PROGNAME "MsgSend");
		exit(EXIT_FAILURE);
	}

	printf("sent msg to: %d\n", save_server_CoId);
	// print server's reply
	printf("%s", sMsg.reply.reply_str);

	//while files are valid
	while (fd != -1) {
		printf("read uitoclient pps\n");

		char readBuff[256] = { 0 }; // a buffer for contents from the uiToClient PPS file
		// 	(block) readPPS/receive event from client UI via pps
		len = read(fd, readBuff, sizeof(readBuff));

		if (strstr(readBuff, "status::event") != NULL) {

			cMsg = eClientIsAlive;
			printf("receive status request\n");

			// (block) send msg to server to send moving event by message passing
			if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg,
					sizeof(sMsg))) {
				perror(PROGNAME "MsgSend");
				exit(EXIT_FAILURE);
			}

			//  (writePPS)/send new x,y cordinates to UI via pps
			sprintf(writeBuff, "@clientToUi\nx::%d\ny::%d\n",
					sMsg.reply.ship_info.x, sMsg.reply.ship_info.y);
			write(fd2, writeBuff, strlen(writeBuff));

			//clear the buffers
			memset(writeBuff, 0, sizeof(writeBuff));
			memset(readBuff, 0, sizeof(readBuff));
		}
		//move up
		//if string is found in buffer
		else if (strstr(readBuff, "moveUp::event") != NULL) {
			//y++;
			cMsg = eClientMoveUp;
			printf("receive move up event\n");

			// (block) send msg to server to send moving event by message passing
			if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg,
					sizeof(sMsg))) {
				perror(PROGNAME "MsgSend");
				exit(EXIT_FAILURE);
			}
			// (writePPS)/send new x,y cordinates to UI via pps
			sprintf(writeBuff, "@clientToUi\nx::%d\ny::%d\n",
					sMsg.reply.ship_info.x, sMsg.reply.ship_info.y);
			write(fd2, writeBuff, strlen(writeBuff));

			//clear the buffers
			memset(writeBuff, 0, sizeof(writeBuff));
			memset(readBuff, 0, sizeof(readBuff));
		}
		//move down
		else if (strstr(readBuff, "moveDown::event") != NULL) {
			//y--;
			//decrement y and write update to pps
			cMsg = eClientMoveDown;
			printf("receive move down event\n");

			// (block) send msg to server to send moving event by message passing
			if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg,
					sizeof(sMsg))) {
				perror(PROGNAME "MsgSend");
				exit(EXIT_FAILURE);
			}

			//  (writePPS)/send new x,y cordinates to UI via pps
			sprintf(writeBuff, "@clientToUi\nx::%d\ny::%d\n",
					sMsg.reply.ship_info.x, sMsg.reply.ship_info.y);
			write(fd2, writeBuff, strlen(writeBuff));

			//clear the buffers
			memset(writeBuff, 0, sizeof(writeBuff));
			memset(readBuff, 0, sizeof(readBuff));

		}
		//move left
		else if (strstr(readBuff, "moveLeft::event") != NULL) {
			//x--;
			cMsg = eClientMoveLeft;
			printf("receive move left event\n");

			// (block) send msg to server to send moving event by message passing
			if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg,
					sizeof(sMsg))) {
				perror(PROGNAME "MsgSend");
				exit(EXIT_FAILURE);
			}

			//  (writePPS)/send new x,y cordinates to UI via pps
			sprintf(writeBuff, "@clientToUi\nx::%d\ny::%d\n",
					sMsg.reply.ship_info.x, sMsg.reply.ship_info.y);
			write(fd2, writeBuff, strlen(writeBuff));
			//clear the buffers
			memset(writeBuff, 0, sizeof(writeBuff));
			memset(readBuff, 0, sizeof(readBuff));
		}
		//move right
		else if (strstr(readBuff, "moveRight::event") != NULL) {

			cMsg = eClientMoveRight;
			printf("receive move right event\n");

			// (block) send msg to server to send moving event by message passing (thread 1)
			if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg,
					sizeof(sMsg))) {
				perror(PROGNAME "MsgSend");
				exit(EXIT_FAILURE);
			}

			sprintf(writeBuff, "@clientToUi\nx::%d\ny::%d\n",
					sMsg.reply.ship_info.x, sMsg.reply.ship_info.y);
			write(fd2, writeBuff, strlen(writeBuff));
			//clear the buffers
			memset(writeBuff, 0, sizeof(writeBuff));
			memset(readBuff, 0, sizeof(readBuff));
		}
		//auto move
		else if (strstr(readBuff, "autoMove::event") != NULL) {
			int i = 0;

			cMsg = eClientMoveAuto;
			printf("receive move right event\n");

			for (i = 0; i < 10; i++) {
				// (block) server reply with sMsg
				if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg,
						sizeof(sMsg))) {
					perror(PROGNAME "MsgSend");
					exit(EXIT_FAILURE);
				}

				//(writePPS)/send new x,y cordinates to UI via pps
				sprintf(writeBuff, "@clientToUi\nx::%d\ny::%d\n",
						sMsg.reply.ship_info.x, sMsg.reply.ship_info.y);
				write(fd2, writeBuff, strlen(writeBuff));
				sleep(1);
			}
			//clear the buffers
			memset(writeBuff, 0, sizeof(writeBuff));
			memset(readBuff, 0, sizeof(readBuff));

		} else if (strstr(readBuff, "disconnect::event") != NULL) {

			cMsg = eClientDisconnect;
			printf("receive disconnect event\n");

			// (block) send msg to server to send moving event by message passing (thread 1)
			if (MsgSend(save_server_CoId, &cMsg, sizeof(cMsg), &sMsg,
					sizeof(sMsg))) {
				perror(PROGNAME "MsgSend");
				exit(EXIT_FAILURE);
			}
			printf("sent disconnect msg to: %d\n", save_server_CoId);
			// print server's reply
			printf("%s", sMsg.reply.reply_str);

			//clear the buffers
			memset(writeBuff, 0, sizeof(writeBuff));
			memset(readBuff, 0, sizeof(readBuff));
			break;
		}

		//clear the buffers
		memset(writeBuff, 0, sizeof(writeBuff));
		memset(readBuff, 0, sizeof(readBuff));

	}//end while
	return EXIT_SUCCESS;
}

