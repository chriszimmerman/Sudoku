using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuEnumerator
{
    public class Driver
    {
        public static void Main(String[] args)
        {
            SudokuEnumerator grid = new SudokuEnumerator(2);
			//grid.PrintGrid(grid.ConvertToArray());
			//grid.PrintGrid(grid.MakePartialSolution(Difficulty.Hard));
			grid.SolvePartialSolution();
			
			grid.PrintGrid(grid.ConvertToArray());
			return;
        }
    }
}