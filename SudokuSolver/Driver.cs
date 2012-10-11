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
			/*PuzzleGenerator p = new PuzzleGenerator(2);
			p.MakePartialSolution(Difficulty.Easy);
			p.PrintGrid(p.ConvertToArray());
			*/
			SudokuEnumerator s = new SudokuEnumerator(2);
			s.SolvePartialSolution("/home/chris/code/Sudoku/SudokuSolver/bin/Debug/test2.txt");
			s.PrintGrid(s.ConvertToArray());
			
			PuzzleSolver q = new PuzzleSolver();
			q.SolvePartialSolution("/home/chris/code/Sudoku/SudokuSolver/bin/Debug/test.txt");
			q.PrintGrid(q.ConvertToArray());
			
			//grid.SolvePartialSolution("/home/chris/code/Sudoku/SudokuSolver/bin/Debug/test.txt");
			//grid.PrintGrid(grid.ConvertToArray());
			//grid.SolvePartialSolution("/home/chris/code/Sudoku/SudokuSolver/bin/Debug/test2.txt");
			
			//grid.PrintGrid(grid.ConvertToArray());
			return;
        }
    }
}