using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku {
    public class SudokuGridPresenter {
        public int?[,] SudokuGrid { get; set; }

        public void UpdateGrid(SudokuPanel panel) {
            SudokuGrid[panel.Column, panel.Row] = panel.Number;
        }

        public bool CheckForWinCondition() {
            foreach (var number in SudokuGrid) {
                if (!number.HasValue) return false;
            }
            return true;
        }
    }
}
