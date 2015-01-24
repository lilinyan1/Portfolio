/*
 * FILE:             A4_QNX_ClientUI.c
 * PROJECT:          PROG2150 - Real-Time Operating Systems - Assignment #4
 * PROGRAMMER:       Linyan Becky Li & Kyle Stevenson
 * AVAILABLE DATE:   12-12-2014
 * DESCRIPTION:      The client ui:
 * 					- send moving, status request event to client via pps
 * 					- capable of exit client and client UI
 * 					- receive x, y  cordinates from client via pps
 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <fcntl.h>
#include <pthread.h>

char
		moveUp[] =
				"@inputs\nmoveUp::event\nmoveDown::\nmoveLeft::\nmoveRight::\nautoMove::\ndisconnect::\nstatus::\n";
char
		moveDown[] =
				"@inputs\nmoveUp::\nmoveDown::event\nmoveLeft::\nmoveRight::\nautoMove::\ndisconnect::\nstatus::\n";
char
		moveLeft[] =
				"@inputs\nmoveUp::\nmoveDown::\nmoveLeft::event\nmoveRight::\nautoMove::\ndisconnect::\nstatus::\n";
char
		moveRight[] =
				"@inputs\nmoveUp::\nmoveDown::\nmoveLeft::\nmoveRight::event\nautoMove::\ndisconnect::\nstatus::\n";
char
		autoMove[] =
				"@inputs\nmoveUp::\nmoveDown::\nmoveLeft::\nmoveRight::\nautoMove::event\ndisconnect::\nstatus::\n";
char
		disconnect[] =
				"@inputs\nmoveUp::\nmoveDown::\nmoveLeft::\nmoveRight::\nautoMove::\ndisconnect::event\nstatus::\n";
char
		status[] =
				"@inputs\nmoveUp::\nmoveDown::\nmoveLeft::\nmoveRight::\nautoMove::\ndisconnect::\nstatus::event\n";
char
		ups[] =
				"@inputs\nmoveUp::\nmoveDown::\nmoveLeft::\nmoveRight::\nautoMove::\ndisconnect::\nstatus::\n";
int fd; //file descriptor

/* The mutex is of the type "pthread_mutex_t". */
pthread_mutex_t displayMutex;
void *readPPS();
//char *extract(char const *trgt, char *val, char *buf);
char* extract(const char* key, char* value, char* location);

char c = 0; //for user input
int quit = 0; //quit flag

int main(int argc, char *argv[]) {
	int fd_write; //file descriptor

	fd_write = open("/pps/uiToClient", O_RDWR | O_TRUNC | O_CREAT);
	write(fd_write, ups, strlen(ups));
	pthread_create(NULL, NULL, &readPPS, NULL);

	while (quit == 0) {

		//display menu
		printf("Testing Interface\n");
		printf("------------------\n");
		printf("[0]: Status \n");
		printf("[1]: Move up \n");
		printf("[2]: Move down \n");
		printf("[3]: Move left \n");
		printf("[4]: Move right \n");
		printf("[5]: Auto move \n");
		printf("[6]: Quit\n");
		printf("Please Choose an Option...\n");

		c = getchar();

		//write to pps the desired command
		if (c == '0') {
			write(fd_write, status, strlen(status));

		} else if (c == '1') {
			write(fd_write, moveUp, strlen(moveUp));

		} else if (c == '2') {
			write(fd_write, moveDown, strlen(moveDown));

		} else if (c == '3') {
			write(fd_write, moveLeft, strlen(moveLeft));

		} else if (c == '4') {
			write(fd_write, moveRight, strlen(moveRight));

		} else if (c == '5') {
			write(fd_write, autoMove, strlen(autoMove));
		} else if (c == '6') {
			//user would like to quit
			write(fd_write, disconnect, strlen(disconnect));
			quit = 1;
		}
	}
	return EXIT_SUCCESS;
}

void *readPPS() {
	char* p; //char pointer that points to where x and y coordinates are extracted

	int len; //the length of string that was read from pps
	char buf[256]; //the buffer for what was read from pps
	char buf2[256] = ""; // a copy of buf for validating purpose
	char x[64] = ""; //a place holder for the extracted value of x
	char y[64] = ""; //a place holder for the extracted value of y
	char value[56] = ""; // the buffer for the x or y coordinate that are extracted
	char* i = NULL; // char pointer for a key word, in this case "x::"
	int isInitialRun = 1;

	fd = open("/pps/clientToUi?wait", O_RDONLY | O_CREAT);
	while (fd > -1) {
		// read got blocked when waiting for an update to the clientToUi file

		len = read(fd, buf, sizeof(buf));

		strcpy(buf2, buf);

		if (isInitialRun == 1) //skipping the first pps read
		{
			isInitialRun = 0;
			continue;
		}
		if ((len > 0) && (strtok(buf2, "x") != NULL)) {
			buf[len] = 0;
			p = buf;

			p = extract("x::", x, p);
			p = extract("y::", y, p);

			sprintf(buf, "X=%s Y=%s\n", x, y);

			// set text in lable
			printf("%s\n", buf);
			memset(buf, 0, sizeof(buf));
			memset(x, 0, sizeof(x));
			memset(y, 0, sizeof(y));
		}
	}


}

/*
 *   NAME      	: 	extract
 *   PARAMETER 	: 	const char* trgt	: the target that extract() is looking for from
 *   				char* val			: exacted string buffer
 *   				char* buf			: char pointer
 *   PURPOSE 	: 	extract value from targeted string, put it to a char pointer named "val"
 *					referenced from the QNX sample called qTstat
 */
char *extract(char const *trgt, char *val, char *buf) {
	char *p, *q;

	p = buf;
	p = strstr(p, trgt);
	if (p) {
		p += strlen(trgt);
		q = strchr(p, '\n');
		*q = 0;
		strcpy(val, p);
		return q + 1;
	} else
		return buf;
}
