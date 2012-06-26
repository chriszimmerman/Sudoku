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
		
		public SudokuEnumerator(int gridSize) {
			this.gridSizeSqrt = gridSize;
			this.gridSize = gridSize * gridSize;
			this.SquaresToTry = new List<Square>();
			this.SquaresFiguredOut = new Stack<Square>();
			this.BacktrackStack = new Stack<Square>();
			this.possibleValues = new List<int>();
			for(int i = 1; i <= this.gridSize; i++){
				possibleValues.Add(i);
			}
		}
		
		public void CreateSudoku() {
			InitializeSquaresToTry();
		}
		
		public void InitializeSquaresToTry() {
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
			
			Enumerate();
		}
		
		private int GetBlockNumber(int column, int row) {
			return ((int)Math.Floor((double)(row - 1) / (double)(gridSizeSqrt)) * gridSizeSqrt)
			      + (int)Math.Ceiling((double)column / (double)(gridSizeSqrt));
		}
		
		public void Enumerate() {
			Square current = new Square();
			bool takeFromSquaresFiguredOut = false;
			
			while(SquaresToTry.Count > 0 || BacktrackStack.Count > 0) {	
				if(takeFromSquaresFiguredOut){
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
			PrintGrid();
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
			return false;
		}
		
		public void RefreshPossibleValues(){
			foreach(var square in SquaresToTry){
				RefreshSquare(square);	
			}
		}
		
		public void RefreshSquare(Square current){
			current.PossibleValues = new List<int>();
			foreach(var possibleValue in possibleValues){
				current.Number = possibleValue;
				if(!DoesSquareInSameZoneShareValue(current)){
					current.PossibleValues.Add(possibleValue);	
				}
			}
			current.Number = null;
		}
		
		public void PrintGrid(){	
			int i = 0;
			
			foreach(var square in SquaresFiguredOut.OrderBy(y => y.YCoordinate).ThenBy(x => x.XCoordinate)){
				Console.Write(square.Number + " ");
				if((i+1) % gridSize == 0) Console.WriteLine();
				i++;
			}
		}
	}
}

