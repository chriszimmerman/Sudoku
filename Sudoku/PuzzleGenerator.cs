using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku {
    public class PuzzleGenerator : SudokuPuzzle {

        public int?[,] PartialSolutionAs2DArray { get; set; }

        public PuzzleGenerator(int gridLengthSqrt = 3) {
            this.GridLengthSqrt = gridLengthSqrt;
            this.GridLength = gridLengthSqrt * gridLengthSqrt;
            this.SquaresToTry = new List<Square>();
            this.SquaresFiguredOut = new Stack<Square>();
            this.BacktrackStack = new Stack<Square>();
            this.PossibleValues = new List<int>();

            GeneratePossibleValues(this.GridLength);
            InitializeSquaresToTry();
            Enumerate();
        }

        public List<Square> MakePartialSolution(Difficulty difficulty) {
            int hintsToTakeAway;
            int totalSquares = GridLength * GridLength;

            switch (difficulty) {
                case Difficulty.Easy:
                    hintsToTakeAway = totalSquares / 4;
                    break;
                case Difficulty.Medium:
                    hintsToTakeAway = totalSquares / 3;
                    break;
                case Difficulty.Hard:
                    hintsToTakeAway = totalSquares / 2;
                    break;
                default:
                    hintsToTakeAway = 0;
                    break;
            }

            Random randomSeed = new Random();
            var solutionSquares = new List<Square>(SquaresFiguredOut);
            var squaresToClear = solutionSquares.OrderBy(x => randomSeed.Next()).Take(hintsToTakeAway);
            foreach (var square in squaresToClear) {
                square.Number = null;
            }

            int?[,] partialSolution = new int?[GridLength, GridLength];
            foreach (var square in solutionSquares) {
                if (square.Number != null)
                    partialSolution[square.XCoordinate - 1, square.YCoordinate - 1] = square.Number;
            }

            PartialSolutionAs2DArray = partialSolution;

            return solutionSquares;
        }
    }
}