using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku {
  public class Square {
    private int xCoordinate;
    private int yCoordinate;
    private int blockNumber;
    private int? number;
    private List<int> possibleValues;

    public int XCoordinate {
      get { return xCoordinate; }
      set { xCoordinate = value; }
    }

    public int YCoordinate {
      get { return yCoordinate; }
      set { yCoordinate = value; }
    }

    public int BlockNumber {
      get { return blockNumber; }
      set { blockNumber = value; }
    }

    public int? Number {
      get { return number; }
      set { number = value; }
    }

    public List<int> PossibleValues {
      get { return possibleValues; }
      set { possibleValues = value; }
    }

    public Square() : this(0, 0, 0, null) { possibleValues = new List<int>(); }
    public Square(int xCoordinate, int yCoordinate, int blockNumber, int? number) {
      XCoordinate = xCoordinate;
      YCoordinate = yCoordinate;
      BlockNumber = blockNumber;
      Number = number;
      possibleValues = new List<int>();
    }

    public void PrintSquareInfo() {
      Console.WriteLine("X: " + XCoordinate + "\tY: " + YCoordinate + "\tBlock: " + BlockNumber + "\tValue: " + Number + "\n");
    }
  }
}
