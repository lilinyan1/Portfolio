//Authors: Amshar Basheer and Linyan (Becky) Li
//Project Name: TilePuzzle
//File Name: MoveValues.cs
//Date: 2014-12-09
//Description: Contains MoveValues class that is used to make up the moveMatrix 2D array (holds X and Y used to move any tile to any spot)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilePuzzle
{
    //class contains int moveX and int moveY that represent actual values to use in margin move statement 
    //to get tile (remember always relative to starting loc) to destSpot.
    public class MoveValues
    {
        int moveX;
        int moveY;

        //ctor that takes values used to initialize struct
        public MoveValues(int _moveX, int _moveY)
        {
            moveX = _moveX;
            moveY = _moveY;
        }

        //Have a getter for each data member (no setter needed because won't ever need to change initial values set by ctor)
        public int GetMoveX
        {
            get { return moveX; }
        }

        public int GetMoveY
        {
            get { return moveY; }
        }
    }
}
