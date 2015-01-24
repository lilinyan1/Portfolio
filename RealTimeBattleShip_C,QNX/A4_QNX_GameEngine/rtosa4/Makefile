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
BINS = Game_Engine_A4

all: $(BINS)

clean:
	rm -f *.o *.tmp $(BINS)

Game_Engine_A4.o: Game_Engine_A4.c