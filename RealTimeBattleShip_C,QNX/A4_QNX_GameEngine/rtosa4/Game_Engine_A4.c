/*
 * FILE:             Game_Engine_A4.c
 * PROJECT:          PROG2150 - Real-Time Operating Systems - Assignment #4
 * PROGRAMMER:       Linyan Becky Li & Kyle Stevenson
 * AVAILABLE DATE:   12-12-2014
 * DESCRIPTION:      Game_Engine:
 * 					- initialize shared memory
 * 					- receive command from server
 * 					- update appropriate x, y coordinates via shared memory and conditional variable
 * 					- share appropriate x, y coordinates via shared memory and conditional variable with the server
 */
#include "battleship_shmem.h"

int main(int argc, char *argv[]) {
	//game engine
	int fd = 0; 					//shared memo file descriptor
	int ret = 0;
	battleship_shmem_t* dataPtr;	//pointer to shared mem structure
	pthread_mutexattr_t myattr;		//mutex attribute

	/* open/create the shared memory */
	fd = shm_open("/myshmemobject", O_RDWR | O_CREAT, S_IRWXU);

	if (fd == -1) {
		printf("error creating the shared memory object.\n");
		exit(EXIT_FAILURE);
	}

	/* Set the size of the shared memory object */
	ftruncate(fd, sizeof(battleship_shmem_t));

	/* Get a pointer to the shared memory object */
	dataPtr = mmap(0, sizeof(battleship_shmem_t), PROT_READ | PROT_WRITE,
			MAP_SHARED, fd, 0);

	// initializing shared memory mutex
	pthread_mutexattr_init(&myattr);
	pthread_mutexattr_setpshared(&myattr, PTHREAD_PROCESS_SHARED);
	pthread_mutex_init(&dataPtr->myshmemmutex, &myattr);

	//initialize the shared memory condition variable
	pthread_cond_init(&dataPtr->condvar, NULL);
	dataPtr->infoToCheck = 1;
	dataPtr->shared_ship_info.x = 0;
	dataPtr->shared_ship_info.y = 0;

	while (1) {

		// lock mutex
		pthread_mutex_lock(&dataPtr->myshmemmutex);

		//check if there is any info ready
		while (dataPtr->infoToCheck == 1) {


			//no info to check so begin waiting for a condition signal from server
			//and unlock the mutex
			printf("Game Engine : waiting for info from sever\n");
			ret = pthread_cond_wait(&dataPtr->condvar, &dataPtr->myshmemmutex);

			//we have been signaled to wake up. and now have the mutex lock
			//check again if any info is ready
		}

		//since there is info to check, now we can take whatever action is needed
		// read the command that the server sent. - do appropriate action
		printf("Game Engine : executing command\n");
		switch (dataPtr->command) {
		case eClientMoveUp:
			//move client up 1
			dataPtr->shared_ship_info.y++;
			break;
		case eClientMoveDown:
			//move client down 1
			dataPtr->shared_ship_info.y--;
			break;
		case eClientMoveLeft:
			//move client left
			dataPtr->shared_ship_info.x--;
			break;
		case eClientMoveRight:
			//move client right 1
			dataPtr->shared_ship_info.x++;
			break;
		case eClientMoveAuto:
			dataPtr->shared_ship_info.x++;
			dataPtr->shared_ship_info.y++;
			//automove client
			break;
		default:
			break;

		}
		//turn infotocheck back to 1
		dataPtr->infoToCheck = 1;

		//signal condition variable to notify server that udpating has completed
		pthread_cond_signal(&dataPtr->condvar);
		//release the mutex before we leave.
		pthread_mutex_unlock(&dataPtr->myshmemmutex);
	}

	pthread_mutexattr_destroy(&myattr);
	pthread_mutex_destroy(&dataPtr->myshmemmutex);
	close(fd); // Closing the file descriptor
	munmap(dataPtr, sizeof(battleship_shmem_t)); // removing the mapping
	shm_unlink("/myshmemobject"); //delete the shared memory object

	return EXIT_SUCCESS;
}
