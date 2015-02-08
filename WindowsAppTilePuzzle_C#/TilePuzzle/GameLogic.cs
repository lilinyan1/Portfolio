//Authors: Amshar Basheer and Linyan (Becky) Li
//Project Name: TilePuzzle
//File Name: GameLogic.cs
//Date: 2014-12-09
//Description: Contains GameLogic class, which is made up of 4 static methods: InitArray (used to initialize spots 2D array of objects), 
//  IsSolvable (used to check if solvable after a randomized scramble), UpdateCanMoveSpots (updates CanMove's in spots), and CheckIfSolved used to check solution.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilePuzzle
{
    class GameLogic
    {
        //the following method used to initialize a 2D array of objects was adopted 
        //from http://stackoverflow.com/questions/3301678/how-to-declare-an-array-of-objects-in-c-sharp
        public static T[,] InitArray<T>(int length1, int length2) where T : new()
        {
            T[,] array = new T[length1, length2];
            for (int i = 0; i < length1; i++)
            {
                for (int j = 0; j < length2; j++)
                {
                    array[i, j] = new T();
                }

            }

            return array;
        }

        //the following algorithm code borrowed from http://www.codeproject.com/Articles/332458/Puzzle
        //it is used to check if the puzzle is solvable after randomly scrambling the tiles
        public static bool IsSolvable()
        {
            var n = 0;
            int whereEmpty = 0;
            for (var i = 15; i > 0; i--)
            {

                var num1 = i;
                var num2 = i - 1;

                //loop through 2D array of spots and determine which spots our tiles are at and which spot is empty
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (MainPage.spots[j, k].SpotNum == num1)
                        {
                            num1 = MainPage.spots[j, k].TileNum;
                        }
                        if (MainPage.spots[j, k].SpotNum == num2)
                        {
                            num2 = MainPage.spots[j, k].TileNum;
                        }
                        if (MainPage.spots[j, k].TileNum == 16)
                        {
                            whereEmpty = MainPage.spots[j, k].SpotNum;
                        }
                    }
                }

                if (num1 > num2)
                {
                    n++;
                }
            }

            var emptyPos = whereEmpty;
            return n % 2 == (emptyPos + emptyPos / 4) % 2 ? true : false;
        }

        //Method Name: UpdateCanMoveSpots
        //Parameters: none
        //Return: void
        //Description: used to update CanMove's in spots -- means it updates which tiles are movable based on where current empty spot is
        public static void UpdateCanMoveSpots()
        {
            int whereEmpty = 0;
            int emptyLocIndex1 = 0;
            int emptyLocIndex2 = 0;

            //loop through 2D array of spots and set canMove to false at each spot, and make note of empty spot spotNum and indexes
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    MainPage.spots[i, j].CanMove = false;

                    if (MainPage.spots[i, j].TileNum == 16)
                    {
                        whereEmpty = MainPage.spots[i, j].SpotNum;
                        emptyLocIndex1 = i;
                        emptyLocIndex2 = j;
                    }
                }
            }

            //now need to determine which canMove's should be set to true based on where empty spot is 
            //note there are 9 possible general cases
            /*
             * 4 corner cases: where each corner case involves setting a different two adjacent spots to true
             * 4 edge cases: where each edge has two of the same type of case where the same 3 adjacent directions will be set to true
             * 1 middle case: has 4 of the same type of case where all four directions around will be set to true 
             * Array of Spot objects represent game board
             *  [0,0]  [0,1]  [0,2]  [0,3] 
             *  [1,0]  [1,1]  [1,2]  [1,3] 
             *  [2,0]  [2,1]  [2,2]  [2,3] 
             *  [3,0]  [3,1]  [3,2]  [3,3] 
             */
            switch (whereEmpty)
            {
                //top left corner case: tile below and tile to the right become true
                case 1:
                    MainPage.spots[emptyLocIndex1 + 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 + 1].CanMove = true;
                    break;

                //top right corner case: tile below and tile to the left become true
                case 4:
                    MainPage.spots[emptyLocIndex1 + 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 - 1].CanMove = true;
                    break;

                //bottom left corner case: tile above and tile to the right become true
                case 13:
                    MainPage.spots[emptyLocIndex1 - 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 + 1].CanMove = true;
                    break;

                //bottom right corner case: tile above and tile to the left become true
                case 16:
                    MainPage.spots[emptyLocIndex1 - 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 - 1].CanMove = true;
                    break;

                //top edge cases: left, right, and below become true
                case 2:
                case 3:
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 - 1].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 + 1].CanMove = true;
                    MainPage.spots[emptyLocIndex1 + 1, emptyLocIndex2].CanMove = true;
                    break;

                //left edge cases: above, below, and right become true
                case 5:
                case 9:
                    MainPage.spots[emptyLocIndex1 - 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1 + 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 + 1].CanMove = true;
                    break;

                //right edge cases: above, below, and left become true
                case 8:
                case 12:
                    MainPage.spots[emptyLocIndex1 - 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1 + 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 - 1].CanMove = true;
                    break;

                //bottom edge cases: left, right, and above become true
                case 14:
                case 15:
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 - 1].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 + 1].CanMove = true;
                    MainPage.spots[emptyLocIndex1 - 1, emptyLocIndex2].CanMove = true;
                    break;

                //middle cases: all around become true (above, below, left, and right)
                case 6:
                case 7:
                case 10:
                case 11:
                    MainPage.spots[emptyLocIndex1 - 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1 + 1, emptyLocIndex2].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 - 1].CanMove = true;
                    MainPage.spots[emptyLocIndex1, emptyLocIndex2 + 1].CanMove = true;
                    break;

            }
        }

        //Method Name: CheckIfSolved
        //Parameters: none
        //Return: bool solved: true if puzzle solved, otherwise false if not solved
        //Description: determines if puzzle is currently solved and returns true if so, otherwise false if not
        public static bool CheckIfSolved()
        {
            bool solved = true; //assume solved unless proven not solved by checking for a mismatch between spot numbers and tile numbers

            //loop through 2D array of spots and check if any mismatch between spot numbers and tile numbers -- if mismatch found then not solved, otherwise if no mismatches means solved
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (MainPage.spots[i, j].SpotNum != MainPage.spots[i, j].TileNum) //checking for mismatch
                    {
                        //if found mismatch
                        solved = false;
                        break;
                    }
                }
            }

            return solved;
        }

    }
}
