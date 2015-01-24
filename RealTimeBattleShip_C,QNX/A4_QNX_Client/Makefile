#
#	Makefile for ipc client
#

DEBUG = -g -fno-stack-protector
CC = qcc
LD = qcc

TARGET = -Vgcc_ntox86
#TARGET = -Vgcc_ntoarmv7le

CFLAGS += $(TARGET) $(DEBUG)
LDFLAGS+= $(TARGET) $(DEBUG)
BINS = A4_QNX_Client

all: $(BINS)

clean:
	rm -f *.o *.tmp $(BINS)

A4_QNX_Client.o: A4_QNX_Client.c server_reply_msg.h client_send_msg.h 