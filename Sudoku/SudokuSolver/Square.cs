using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    public class Square
    {
        private int xCoordinate;
        private int yCoordinate;
        private int number;

        public int XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public int YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public Square(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Number = 0; 
        }

        public Square(int xCoordinate, int yCoordinate, int number)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Number = number;
        }

        public void printSquareInfo()
        {
            Debug.Write("X: " + XCoordinate + "\tY: " + YCoordinate + "\tValue: " + Number + "\tPoopballs\n");
        }
    }
}
