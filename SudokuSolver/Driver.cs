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
			grid.CreateSudoku();
            return;
        }
    }
}