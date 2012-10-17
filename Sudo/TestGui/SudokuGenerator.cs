using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudo
{
	public class SudokuGenerator
	{
		int gridSize;
		int gridSizeSqrt;
		List<Square> SquaresToTry;
		Stack<Square> SquaresFiguredOut;
		Stack<Square> BacktrackStack;
		List<int> possibleValuesForSquare;
			
		public SudokuGenerator(int n) {
			this.gridSizeSqrt = n;
			this.gridSize = n * n;
			this.SquaresToTry = new List<Square>();
			this.SquaresFiguredOut = new Stack<Square>();
			this.BacktrackStack = new Stack<Square>();
			this.possibleValuesForSquare = new List<int>();
			for(int i = 1; i <= this.gridSize; i++){
				possibleValuesForSquare.Add(i);
			}
		}
		
		public void InitializeSquaresToTry() {
			for(int row = 1; row <= gridSize; row++) {
				for(int column = 1; column <= gridSize; column++) {
					var newSquare = new Square();
					newSquare.XCoordinate = column;
					newSquare.YCoordinate = row;
					newSquare.BlockNumber = ((int)Math.Floor((double)(row - 1) / (double)(gridSizeSqrt)) * gridSizeSqrt)
										  + (int)Math.Ceiling((double)column / (double)(gridSizeSqrt));
					newSquare.PossibleValues = new List<int>(possibleValuesForSquare);
					SquaresToTry.Add(newSquare);
				}
			}
	
		}
		
		public void Enumerate() {
			Square current = new Square();
			bool takeFromFiguredOut = false;
			
			while(SquaresToTry.Count > 0 || BacktrackStack.Count > 0) {	
				if(takeFromFiguredOut){
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
					takeFromFiguredOut = false;
				}
				else {
					BacktrackStack.Push(current);
					takeFromFiguredOut = true;
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
			return false;
		}
		
		public void RefreshPossibleValues(){
			foreach(var square in SquaresToTry){
				RefreshSquare(square);	
			}
		}
		
		public void RefreshSquare(Square current){
			current.PossibleValues = new List<int>();
			foreach(var possibleValue in possibleValuesForSquare){
				current.Number = possibleValue;
				if(!DoesSquareInSameZoneShareValue(current)){
					current.PossibleValues.Add(possibleValue);	
				}
			}
			current.Number = null;
		}
		
		public void PrintGrid(){	
			int i = 0;
			System.IO.StreamWriter file = new System.IO.StreamWriter("./sudoku.html");
			file.WriteLine("<html><body>");
			string color = "";
			foreach(var square in SquaresFiguredOut.OrderBy(y => y.YCoordinate).ThenBy(x => x.XCoordinate)){
				
				
				
				file.Write("<font color=\"" + color + "\">" + square.Number + "</font>   ");
				if((i+1)%gridSize == 0) file.WriteLine("<br>");
				i++;
			}
		}
	}
}

