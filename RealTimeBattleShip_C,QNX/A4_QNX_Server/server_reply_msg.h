/*
* FILE:             server_reply_msg.h
* PROJECT:          PROG2150 - Real-Time Operating Systems - Assignment #2
* PROGRAMMER:       Linyan Becky Li & Kyle Stevenson
* AVAILABLE DATE:   10-15-2014
* DESCRIPTION:      contains structure of server's reply message
*/

#ifndef SERVER_REPLY_MSG_H
#define SERVER_REPLY_MSG_H
#define STRING_MAX 200
#define DISCONNECT_PULSE_CODE 2
#define START_HEALTH 100
#define START_X 0
#define START_Y 0
#define START_ANGLE 0
#define CLIENT_MAX 10

// Server reply message type
typedef enum
{
    //! Generic ack reply that server use to
    //! indicate the client message is received. reply one string
    eServerAck,
    //! Server replys to client the current world state, reply an array of struct
    eServerStateOfWorldReply,
    //! server reply with the client's x.y cordinates
    eServerClientStatus,


} server_reply_type;


//! Info for a ship
// world status is an array of ship_info_t
// i.e.
// ship_info_t world_status[];
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
} ship_info_t;


// a client table is an array of client_table_entry_t
// i.e.
// client_table_entry_t client_table[MAX_CLIENT_SIZE];
//
typedef struct client_table_entry_s
{
   //! client connection ID.
   //! coid of -1 indicate this entry is invalid.
   int coid;
   //! information about the client's ship, including
   //! position and health status of the ship.
   ship_info_t ship_info;

} client_table_entry_t;


// server's reply message type
typedef struct server_reply_msg_s
{
    //! what type of reply is this
	server_reply_type type;

    //! content of the reply
    union
    {
        //! world state
		client_table_entry_t client_table[CLIENT_MAX];
        char reply_str[STRING_MAX];
        ship_info_t ship_info;
    } reply;
} server_reply_msg_t;

#endif
