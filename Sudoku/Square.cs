using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku {
    public class Square {
        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }

        public int BlockNumber { get; set; }

        public int? Number { get; set; }

        public List<int> PossibleValues { get; set; }

        public Square() : this(0, 0, 0, null) { PossibleValues = new List<int>(); }
        public Square(int xCoordinate, int yCoordinate, int blockNumber, int? number) {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            BlockNumber = blockNumber;
            Number = number;
            PossibleValues = new List<int>();
        }

        public void PrintSquareInfo() {
            Console.WriteLine("X: " + XCoordinate + "\tY: " + YCoordinate + "\tBlock: " + BlockNumber + "\tValue: " + Number + "\n");
        }
    }
}
