using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuEnumerator
{
	public class SudokuEnumerator
	{
		int gridSize;
		int gridSizeSqrt;
		List<int> possibleValues;
		List<Square> SquaresToTry;
		Stack<Square> SquaresFiguredOut;
		Stack<Square> BacktrackStack;
		Stack<Square> PartialSolution;
		
		public SudokuEnumerator(int gridSize = 3) {
			this.gridSizeSqrt = gridSize;
			this.gridSize = gridSize * gridSize;
			this.SquaresToTry = new List<Square>();
			this.SquaresFiguredOut = new Stack<Square>();
			this.BacktrackStack = new Stack<Square>();
			this.PartialSolution = new Stack<Square>();
			this.possibleValues = new List<int>();
			
			for(int i = 1; i <= this.gridSize; i++){
				possibleValues.Add(i);
			}
			
			InitializeSquaresToTry();
			Enumerate();
		}
		
		private void InitializeSquaresToTry() {
			for(int row = 1; row <= gridSize; row++) {
				for(int column = 1; column <= gridSize; column++) {
					var newSquare = new Square();
					newSquare.XCoordinate = column;
					newSquare.YCoordinate = row;
					newSquare.BlockNumber = GetBlockNumber(column, row);
					newSquare.PossibleValues = new List<int>(possibleValues);
					SquaresToTry.Add(newSquare);
				}
			}
		}
		
		private int GetBlockNumber(int column, int row) {
			return ((int)Math.Floor((double)(row - 1) / (double)(gridSizeSqrt)) * gridSizeSqrt)
			      + (int)Math.Ceiling((double)column / (double)(gridSizeSqrt));
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
			foreach(var possibleValue in possibleValues){
				current.Number = possibleValue;
				if(!DoesSquareInSameZoneShareValue(current)){
					current.PossibleValues.Add(possibleValue);	
				}
			}
			current.Number = null;
		}
		
		public int?[,] ConvertToArray(){
			int?[,] grid = new int?[gridSize, gridSize];
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
			Random random = new Random();
			
			switch(difficulty){
				case Difficulty.Easy:
					hintsToTakeAway = 45;
					break;
				case Difficulty.Medium:
					hintsToTakeAway = 54;
					break;
				case Difficulty.Hard:
					hintsToTakeAway = 12;
					break;
				default:
					hintsToTakeAway = 0;
					break;
			}
			
			var solutionSquares = new List<Square>(SquaresFiguredOut);
			
			var squaresToClear = solutionSquares.OrderBy(x => random.Next()).Take(hintsToTakeAway);
			foreach(var square in squaresToClear){
				square.Number = null;
			}
			
			int? [,] partialSolution = new int?[gridSize, gridSize];
			foreach(var square in solutionSquares){
				if(square.Number != null)
					partialSolution[square.XCoordinate-1, square.YCoordinate-1] = square.Number;	
			}
			
			return partialSolution;
		}
		
		public void PrintGrid(int?[,] grid){				
			for(int row = 0; row < gridSize; row++){
				for(int column = 0; column < gridSize; column++){
					if(grid[column, row] != null)
						Console.Write(grid[column, row] + " ");
					else
						Console.Write("_ ");
				}
				Console.WriteLine();
			}
		}
		
		public void SolvePartialSolution(){
			
			SquaresFiguredOut = new Stack<Square>();
			SquaresToTry = new List<Square>();
			PartialSolution = new Stack<Square>();
			BacktrackStack = new Stack<Square>();
			string input = "_ 5 2 _ _ _ 9 _ 1 " +
				   		   "9 _ _ _ _ _ 5 8 _ " +
						   "3 _ _ _ 1 _ _ _ _ " +
						   "_ _ _ _ 3 7 _ 5 9 " +
						   "_ _ 9 4 _ _ _ _ _ " +
						   "_ _ _ 9 _ _ 6 2 _ " +
						   "8 6 _ _ _ 3 _ _ _ " +
						   "_ 9 _ 7 _ 8 _ 4 _ " +
						   "4 _ _ 2 _ _ _ _ 8";
			
			var digits = input.Split(' ');
			int length = (int)Math.Sqrt(digits.Length);
			
			int col = 1;
			int row = 1;
			
			foreach(var digit in digits){
				var square = new Square();
				square.XCoordinate = col;
				square.YCoordinate = row;
				square.BlockNumber = GetBlockNumber(col, row);
				
				if(digit != "_"){
					square.Number = Convert.ToInt32(digit);
					PartialSolution.Push(square);
				}
				else{
					SquaresToTry.Add(square);	
				}
				
				col++;
				if(col > length){
					row++;
					col = 1;
				}
			}
			RefreshPossibleValues();
			Enumerate();
		}
	}
}