using System;
using System.Collections.Generic;

namespace SudokuEnumerator
{
	public class SudokuEnumerator
	{
		int gridSize;
		int gridSizeSqrt;
		List<Square> SquaresToTry;
		
		public SudokuEnumerator(int gridSize)
		{
			this.gridSizeSqrt = gridSize;
			this.gridSize = gridSize * gridSize;
		}
		
		public void InitializeSquaresToTry()
		{
			SquaresToTry = new List<Square>();
			for(int row = 1; row <= gridSize; row++)
			{
				for(int column = 1; column <= gridSize; column++){
					var newSquare = new Square();
					newSquare.XCoordinate = column;
					newSquare.YCoordinate = row;
					newSquare.BlockNumber = (int)(row / (gridSizeSqrt + 1)) * gridSizeSqrt 
										  + (int)(column / (gridSizeSqrt + 1)) + 1;
					newSquare.Number = 0;
					SquaresToTry.Add(newSquare);
				}
			}
			
			foreach(var square in SquaresToTry)
			{
				square.PrintSquareInfo();
			}
		}
	}
}

