using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SudokuEnumerator
{
	public class SudokuEnumerator
	{
		int gridLength;
		int gridLengthSqrt;
		List<int> PossibleValues;
		List<Square> SquaresToTry;
		Stack<Square> SquaresFiguredOut;
		Stack<Square> BacktrackStack;
		Stack<Square> PartialSolution;
		
		public SudokuEnumerator(int gridLengthSqrt = 3) {
			this.gridLengthSqrt = gridLengthSqrt;
			this.gridLength = gridLengthSqrt * gridLengthSqrt;
			this.SquaresToTry = new List<Square>();
			this.SquaresFiguredOut = new Stack<Square>();
			this.BacktrackStack = new Stack<Square>();
			this.PartialSolution = new Stack<Square>();
			this.PossibleValues = new List<int>();
			
			GeneratePossibleValues(this.gridLength);
			InitializeSquaresToTry();
			Enumerate();
		}
		
		private void GeneratePossibleValues(int maxNumber)
		{
			for(int i = 1; i <= maxNumber; i++){
				PossibleValues.Add(i);
			}
		}
		
		private void InitializeSquaresToTry() {
			for(int row = 1; row <= gridLength; row++) {
				for(int column = 1; column <= gridLength; column++) {
					var newSquare = new Square();
					newSquare.XCoordinate = column;
					newSquare.YCoordinate = row;
					newSquare.BlockNumber = GetBlockNumber(column, row);
					newSquare.PossibleValues = new List<int>(PossibleValues);
					SquaresToTry.Add(newSquare);
				}
			}
		}
		
		private int GetBlockNumber(int column, int row) {
			return ((int)Math.Floor((double)(row - 1) / (double)(gridLengthSqrt)) * gridLengthSqrt)
			      + (int)Math.Ceiling((double)column / (double)(gridLengthSqrt));
		}
		
		private void Enumerate() {
			Square current = new Square();
			bool takeFromSquaresFiguredOut = false;
			
			while(SquaresToTry.Count > 0 || BacktrackStack.Count > 0) {	
				if(takeFromSquaresFiguredOut && SquaresFiguredOut.Count > 0){
					current = SquaresFiguredOut.Pop();
				}
				else {
					if(BacktrackStack.Any()){
						current = BacktrackStack.Pop();
						RefreshSquare(current);
					}
					else{
						current = SquaresToTry.OrderBy(x => x.PossibleValues.Count()).Take(1).First();	
						SquaresToTry.Remove(current);
					}
				}
					
				if(IsValidValueFoundForSquare(current)){
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

		private bool IsValidValueFoundForSquare(Square current){
			Random random = new Random();
			int valueToTry;
			
			while(current.PossibleValues.Count > 0){
				valueToTry = current.PossibleValues.OrderBy(x => random.Next()).Take(1).First();
				current.Number = valueToTry;
				current.PossibleValues.Remove(valueToTry);
				
				if(!DoesSquareInSameZoneShareValue(current)){
					return true;
				}	
			}
			current.Number = null;
			return false;
		}
		
		private bool DoesSquareInSameZoneShareValue(Square current){
			foreach(var square in SquaresFiguredOut){
				if((square.XCoordinate == current.XCoordinate
					|| square.YCoordinate == current.YCoordinate
					|| square.BlockNumber == current.BlockNumber)
					&& square.Number == current.Number){
						return true;
				}
			}
			
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
		
		private void RefreshPossibleValues(){
			foreach(var square in SquaresToTry){
				RefreshSquare(square);	
			}
		}
		
		private void RefreshSquare(Square current){
			current.PossibleValues = new List<int>();
			foreach(var possibleValue in PossibleValues){
				current.Number = possibleValue;
				if(!DoesSquareInSameZoneShareValue(current)){
					current.PossibleValues.Add(possibleValue);	
				}
			}
			current.Number = null;
		}
		
		public int?[,] ConvertToArray(){
			int?[,] grid = new int?[gridLength, gridLength];
			foreach(var square in SquaresFiguredOut){
				grid[square.XCoordinate-1, square.YCoordinate-1] = square.Number;
			}
			foreach(var square in PartialSolution){
				grid[square.XCoordinate-1, square.YCoordinate-1] = square.Number;
			}
			return grid;
		}
		
		public int?[,] MakePartialSolution(Difficulty difficulty){
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
		
		public void PrintGrid(int?[,] grid){				
			for(int row = 0; row < gridLength; row++){
				for(int column = 0; column < gridLength; column++){
					if(grid[column, row] != null)
						Console.Write(grid[column, row] + " ");
					else
						Console.Write("_ ");
				}
				Console.WriteLine();
			}
		}
		
		public void SolvePartialSolution(string filename){
			SquaresFiguredOut = new Stack<Square>();
			SquaresToTry = new List<Square>();
			PartialSolution = new Stack<Square>();
			BacktrackStack = new Stack<Square>();
			
			var digits = ParseFile(filename);
			int length = (int)Math.Sqrt(digits.Count);
			
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
	}
}