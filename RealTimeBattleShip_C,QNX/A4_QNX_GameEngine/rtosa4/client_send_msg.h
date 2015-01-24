/*
 * client_send_msg.h
 *
 *  Created on: 2014-12-10
 *      Author: Kyle
 */

#ifndef CLIENT_SEND_MSG_H_
#define CLIENT_SEND_MSG_H_

#include <sys/siginfo.h>
#include <stdint.h>
#include "server_reply_msg.h"


// client message type
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
} client_send_msg_type;


// client_send_msg_t is the structure  of the client’s sending message, it uses the client_send_msg_type type
typedef struct client_send_msg_s
{
    //! what type of message is this
    client_send_msg_type type;

    struct sigevent ev;

} client_send_msg_t;

#endif /* CLIENT_SEND_MSG_H_ */
