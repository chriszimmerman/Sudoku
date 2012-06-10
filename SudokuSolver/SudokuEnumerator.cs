using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuEnumerator
{
	public class SudokuEnumerator
	{
		int gridSize;
		int gridSizeSqrt;
		List<Square> SquaresToTry;
		Stack<Square> SquaresFiguredOut;
		
		public SudokuEnumerator(int gridSize)
		{
			this.gridSizeSqrt = gridSize;
			this.gridSize = gridSize * gridSize;
			this.SquaresToTry = new List<Square>();
			this.SquaresFiguredOut = new Stack<Square>();
		}
		
		public void InitializeSquaresToTry()
		{
			for(int row = 1; row <= gridSize; row++)
			{
				for(int column = 1; column <= gridSize; column++){
					var newSquare = new Square();
					newSquare.XCoordinate = column;
					newSquare.YCoordinate = row;
					newSquare.BlockNumber = ((int)(row / (gridSizeSqrt + 1))) * gridSizeSqrt 
										  + (int)(column / (gridSizeSqrt + 1)) + 1;
					newSquare.Number = 1;
					SquaresToTry.Add(newSquare);
				}
			}
		}
		
		public void Enumerate(){
			Square current = new Square();
			bool takeFromStack = false;
			
			while(SquaresToTry.Count > 0){
				Random random = new Random();
				if(takeFromStack){
					SquaresToTry.Add(current);
					current = SquaresFiguredOut.Pop();
				}
				else{
					current = SquaresToTry.OrderBy(x => random.Next()).Take(1).First();	
					SquaresToTry.Remove(current);
				}
				
				takeFromStack = !IsValidValueFoundForSquare(current);
			}
		}
				
		private bool IsValidValueFoundForSquare(Square current){
			for(int i = current.Number; i <= gridSize; i++){
				current.Number = i;
				if(!DoesSquareInSameZoneShareValue(current)){
					SquaresFiguredOut.Push(current);
					return true;
				}
			}
			current.Number = 1;
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
		
		public void PrintGrid(){
			
			int i = 0;
			
			foreach(var square in SquaresFiguredOut.OrderBy(y => y.YCoordinate).ThenBy(x => x.XCoordinate)){
				Console.Write(square.Number + " ");
				if((i+1)%gridSize == 0) Console.WriteLine();
				i++;
			}
		}
	}
}

