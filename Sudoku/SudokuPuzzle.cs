using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Sudoku {
  public abstract class SudokuPuzzle {
    protected int gridLength;
    protected int gridLengthSqrt;
    protected List<int> PossibleValues;
    protected List<Square> SquaresToTry;
    protected Stack<Square> SquaresFiguredOut;
    protected Stack<Square> BacktrackStack;

    public SudokuPuzzle() { }

    protected void GeneratePossibleValues(int maxNumber) {
      for (int i = 1; i <= maxNumber; i++) {
        PossibleValues.Add(i);
      }
    }

    protected void InitializeSquaresToTry() {
      for (var row = 1; row <= gridLength; row++) {
        for (var column = 1; column <= gridLength; column++) {
          var newSquare = new Square {
                XCoordinate = column,
                YCoordinate = row,
                BlockNumber = GetBlockNumber(column, row),
                PossibleValues = new List<int>(PossibleValues)
          };
          SquaresToTry.Add(newSquare);
        }
      }
    }

    protected int GetBlockNumber(int column, int row) {
      return ((int)Math.Floor((double)(row - 1) / (double)(gridLengthSqrt)) * gridLengthSqrt)
            + (int)Math.Ceiling((double)column / (double)(gridLengthSqrt));
    }

    protected void Enumerate() {
      Square current = new Square();
      bool takeFromSquaresFiguredOut = false;

      while (SquaresToTry.Count > 0 || BacktrackStack.Count > 0) {
        if (takeFromSquaresFiguredOut && SquaresFiguredOut.Count > 0) {
          current = SquaresFiguredOut.Pop();
        }
        else {
          if (BacktrackStack.Any()) {
            current = BacktrackStack.Pop();
            RefreshSquare(current);
          }
          else {
            current = SquaresToTry.OrderBy(x => x.PossibleValues.Count()).Take(1).First();
            SquaresToTry.Remove(current);
          }
        }

        if (IsValidValueFoundForSquare(current)) {
          SquaresFiguredOut.Push(current);
          takeFromSquaresFiguredOut = false;
        }
        else {
          BacktrackStack.Push(current);
          takeFromSquaresFiguredOut = true;
        }
        RefreshPossibleValues();
      }
    }

    protected bool IsValidValueFoundForSquare(Square current) {
      var random = new Random();

        while (current.PossibleValues.Count > 0) {
            int valueToTry = current.PossibleValues.OrderBy(x => random.Next()).Take(1).First();
            current.Number = valueToTry;
            current.PossibleValues.Remove(valueToTry);

            if (!DoesSquareInSameZoneShareValue(current)) {
                return true;
            }
        }
      current.Number = null;
      return false;
    }

    protected virtual bool DoesSquareInSameZoneShareValue(Square current) {
        return SquaresFiguredOut.Any(square => 
                                        (square.XCoordinate == current.XCoordinate 
                                      || square.YCoordinate == current.YCoordinate
                                      || square.BlockNumber == current.BlockNumber) && square.Number == current.Number);
    }

      protected void RefreshPossibleValues() {
      foreach (var square in SquaresToTry) {
        RefreshSquare(square);
      }
    }

    private void RefreshSquare(Square current) {
      current.PossibleValues = new List<int>();
      foreach (var possibleValue in PossibleValues) {
        current.Number = possibleValue;
        if (!DoesSquareInSameZoneShareValue(current)) {
          current.PossibleValues.Add(possibleValue);
        }
      }
      current.Number = null;
    }

    public virtual int?[,] ConvertToArray() {
      var grid = new int?[gridLength, gridLength];

      foreach (var square in SquaresFiguredOut) {
        grid[square.XCoordinate - 1, square.YCoordinate - 1] = square.Number;
      }

      return grid;
    }

    public void PrintGrid(int?[,] grid) {
      for (var row = 0; row < gridLength; row++) {
        for (var column = 0; column < gridLength; column++) {
          if (grid[column, row] != null)
            Console.Write(grid[column, row] + " ");
          else
            Console.Write("_ ");
        }
        Console.WriteLine();
      }
    }
  }
}
