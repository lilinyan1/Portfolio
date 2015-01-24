//Authors: Amshar Basheer and Linyan (Becky) Li
//Project Name: TilePuzzle
//File Name: Spot.cs
//Date: 2014-12-09
//Description: Contains Spot class that is used to make up the spots 2D array (holds spotNum, tileNum, and canMove -- used to keep track of state of board)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilePuzzle
{
    public class Spot
    {
        private bool canMove; //keeps track of if tile in this spot currently movable
        private int tileNum; //tile number that is currently found in this spot
        private int spotNum; //represents location/spot on board       

       
        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }

        public int TileNum
        {
            get { return tileNum; }
            set { tileNum = value; }
        }

        public int SpotNum
        {
            get { return spotNum; }
            set { spotNum = value; }
        }

        
    }
}
