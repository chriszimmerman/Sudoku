using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuEnumerator
{
    public class SudokuSet
    {
        private int baseNumber;

        public int BaseNumber
        {
            get { return baseNumber; }
            set { baseNumber = value; }
        }

        private HashSet<Square> sudokuGrid = new HashSet<Square>();

        private HashSet<HashSet<Square>> rows = new HashSet<HashSet<Square>>();
        private HashSet<HashSet<Square>> columns = new HashSet<HashSet<Square>>();
        private HashSet<HashSet<Square>> blocks = new HashSet<HashSet<Square>>();

        public SudokuSet(int baseNumber)
        {
            BaseNumber = baseNumber;
            populateGrid();
        }

        public void populateGrid()
        {
            for(int i = 1; i <= BaseNumber * BaseNumber; i++)
            {
                for(int j = 1; j <= BaseNumber * BaseNumber; j++)
                {
                    sudokuGrid.Add(new Square(i, j));
                }
            }
        }

        public void printGrid()
        {
            foreach (var square in sudokuGrid)
            {
                square.printSquareInfo();
            }
        }
    }


}
