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
            SudokuEnumerator grid = new SudokuEnumerator(3);
			//grid.PrintGrid(grid.ConvertToArray());
			//grid.PrintGrid(grid.MakePartialSolution(Difficulty.Hard));
			//grid.SolvePartialSolution("/home/chris/code/Sudoku/SudokuSolver/bin/Debug/test.txt");
			grid.PrintGrid(grid.ConvertToArray());
			grid.SolvePartialSolution("/home/chris/code/Sudoku/SudokuSolver/bin/Debug/test2.txt");
			
			grid.PrintGrid(grid.ConvertToArray());
			return;
        }
    }
}