using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Sudo {
	public class PuzzleSolver : SudokuPuzzle {
		Stack<Square> PartialSolution;
		
		public PuzzleSolver () {
			this.SquaresToTry = new List<Square>();
			this.SquaresFiguredOut = new Stack<Square>();
			this.BacktrackStack = new Stack<Square>();
			this.PossibleValues = new List<int>();
		}
		
		public void SolvePartialSolution(string filename){
			SquaresFiguredOut = new Stack<Square>();
			SquaresToTry = new List<Square>();
			PartialSolution = new Stack<Square>();
			BacktrackStack = new Stack<Square>();
			
			var digits = ParseFile(filename);
			int length = (int)Math.Sqrt(digits.Count);
			this.gridLength = length;
			this.gridLengthSqrt = (int)Math.Sqrt(this.gridLength);
			GeneratePossibleValues(length);
			
            for (int row = 1; row <= length; row++) {
                for(int col = 1; col <= length; col++) {
                    var square = new Square();
                    square.XCoordinate = col;
                    square.YCoordinate = row;
                    square.BlockNumber = GetBlockNumber(col, row);

                    if (digits[(row - 1) * length + col - 1] != "_")
                    {
                        square.Number = Convert.ToInt32(digits[(row - 1) * length + col - 1]);
                        PartialSolution.Push(square);
                    }
                    else
                    {
                        SquaresToTry.Add(square);
                    }
                }    
            }
			RefreshPossibleValues();
			Enumerate();
		}
		
		private List<string> ParseFile(string filename){
			var squares = File.ReadAllText(filename).ToString().Split(' ', '\n').ToList();
			return squares.Take(squares.Count-1).ToList();
		}
		
		protected override bool DoesSquareInSameZoneShareValue (Square current) {
			if(base.DoesSquareInSameZoneShareValue(current))
				return true;
			
			foreach(var square in PartialSolution){
				if((square.XCoordinate == current.XCoordinate
					|| square.YCoordinate == current.YCoordinate
					|| square.BlockNumber == current.BlockNumber)
					&& square.Number == current.Number){
						return true;
				}
			}
			return false;
		}
		
		public override int?[,] ConvertToArray() {
			int?[,] grid = base.ConvertToArray();
			
			foreach(var square in PartialSolution){
				grid[square.XCoordinate-1, square.YCoordinate-1] = square.Number;
			}
			return grid;
		}
	}
}

