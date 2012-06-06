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
            //Square square = new Square(1,2,3, 0);
            //square.PrintSquareInfo();

            SudokuEnumerator grid = new SudokuEnumerator(3);
			
			grid.InitializeSquaresToTry();
            return;
        }
    }
}