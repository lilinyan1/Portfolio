/*
* FILE:             battleship_shmem.h
* PROJECT:          PROG2150 - Real-Time Operating Systems - Assignment #4
* PROGRAMMER:       Linyan Becky Li & Kyle Stevenson
* AVAILABLE DATE:   11-27-2014
* DESCRIPTION:      contains the shared memory data type
*/
/*
 * battleship_shmem.h
 *
 *  Created on: Nov 27, 2014
 *      Author: kstevenson-cc
 */


#ifndef BATTLESHIP_SHMEM_H_
#define BATTLESHIP_SHMEM_H_

#include <pthread.h>
#include "client_send_msg.h"
#include "server_reply_msg.h"

#define MAX_COMM_LEN    100

/* Here we define the contents of our shared memory object
 * using a structure. All processes that want to access the
 * shared memory object should use this structure to access
 * the proper contents. */


typedef struct {
	/* A mutex for synchronizing shmem access */
    pthread_mutex_t  myshmemmutex;
    pthread_cond_t condvar;
    int infoToCheck;
    /* A text string */
    client_send_msg_type	command;

    //condition variable

    //state of the world struct
    ship_info_t shared_ship_info;

    /* Anything you want can be in the shared memory */

} battleship_shmem_t;


#endif /* BATTLESHIP_SHMEM_H_ */
