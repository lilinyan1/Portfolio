/*
 * battleship_shmem.h
 *
 *  Created on: Nov 27, 2014
 *      Author: kstevenson-cc
 */

#ifndef BATTLESHIP_SHMEM_H_
#define BATTLESHIP_SHMEM_H_

#include "client_send_msg.h"
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <fcntl.h>
#include <string.h>
#include <unistd.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <pthread.h>

#define MAX_COMM_LEN    100

/* Here we define the contents of our shared memory object
 * using a structure. All processes that want to access the
 * shared memory object should use this structure to access
 * the proper contents. */

/*
typedef enum
{
	//! client is requesting a connection
	eClientConnection,
	//! client is disconnecting
	eClientDisconnect,
	//! client indicates to server that it's
	//! still connected and alive
	eClientIsAlive,
	//! client indicates to server that it's moving up
	eClientMoveUp,
	//! client indicates to server that it's moving down
	eClientMoveDown,
	//! client indicates to server that it's moving left
	eClientMoveLeft,
	//! client indicates to server that it's moving right
	eClientMoveRight,
	//! client indicates to server that it's moving right
	eClientMoveAuto,
} client_send_msg_type; */

//! Info for a ship
// world status is an array of ship_info_t
// i.e.
// ship_info_t world_status[];
/*
typedef struct ship_info_s
{
	//! X position of the ship
	uint32_t x;
	//! Y position of the ship
	uint32_t y;

	//! angle of the ship, in 45 degrees increment
	//! i.e. 0, 45, 90, 135, etc.
	uint32_t angle;
    //! Health of the ship, in term of number of HP left
    //! @note   The requirement on ship health is unclear at the moment.
    //!         This struct may need further expansion in the future.
    uint32_t health;
} ship_info_t; */

// a client table is an array of client_table_entry_t
// i.e.
// client_table_entry_t client_table[MAX_CLIENT_SIZE];
//
/*
typedef struct client_table_entry_s
{
   //! client connection ID.
   //! coid of -1 indicate this entry is invalid.
   int coid;
   //! information about the client's ship, including
   //! position and health status of the ship.
   ship_info_t ship_info;

} client_table_entry_t; */

typedef struct {
	/* A mutex for synchronizing shmem access */
    pthread_mutex_t  myshmemmutex;
    pthread_cond_t condvar;
	int infoToCheck;   //false
    /* A text string */
    client_send_msg_type command;
    //condition variable

    //state of the world struct
    ship_info_t shared_ship_info;

    /* Anything you want can be in the shared memory */

} battleship_shmem_t;




#endif /* BATTLESHIP_SHMEM_H_ */
