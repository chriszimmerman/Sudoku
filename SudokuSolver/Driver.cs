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
            Square square = new Square(1,2,3);
            square.printSquareInfo();

            SudokuSet grid = new SudokuSet(2);
            grid.printGrid();
            return;
        }
    }
}
