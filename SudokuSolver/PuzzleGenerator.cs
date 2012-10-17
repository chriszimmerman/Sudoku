using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SudokuEnumerator
{
	public class PuzzleGenerator : SudokuPuzzle
	{
		public PuzzleGenerator(int gridLengthSqrt = 3) {
			this.gridLengthSqrt = gridLengthSqrt;
			this.gridLength = gridLengthSqrt * gridLengthSqrt;
			this.SquaresToTry = new List<Square>();
			this.SquaresFiguredOut = new Stack<Square>();
			this.BacktrackStack = new Stack<Square>();
			this.PossibleValues = new List<int>();
			
			GeneratePossibleValues(this.gridLength);
			InitializeSquaresToTry();
			Enumerate();
		}
		
		public int?[,] MakePartialSolution(Difficulty difficulty) {
			int hintsToTakeAway;
			int totalSquares = gridLength * gridLength;
			
			switch(difficulty){
				case Difficulty.Easy:
					hintsToTakeAway = totalSquares / 2;
					break;
				case Difficulty.Medium:
					hintsToTakeAway = totalSquares / 3;
					break;
				case Difficulty.Hard:
					hintsToTakeAway = totalSquares / 4;
					break;
				default:
					hintsToTakeAway = 0;
					break;
			}
			
			Random random = new Random();
			var solutionSquares = new List<Square>(SquaresFiguredOut);
			var squaresToClear = solutionSquares.OrderBy(x => random.Next()).Take(hintsToTakeAway);
			foreach(var square in squaresToClear){
				square.Number = null;
			}
			
			int? [,] partialSolution = new int?[gridLength, gridLength];
			foreach(var square in solutionSquares){
				if(square.Number != null)
					partialSolution[square.XCoordinate-1, square.YCoordinate-1] = square.Number;	
			}
			
			return partialSolution;
		}
	}
}