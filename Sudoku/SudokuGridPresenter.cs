using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sudoku {
    public class SudokuGridPresenter {
        public int?[,] SudokuGrid { get; set; }
        public List<Square> SudokuGridSquares { get; set; }
        public SudokuPuzzleView View;

        public void UpdateGrid(SudokuPanel panel) {
            SudokuGrid[panel.Column, panel.Row] = panel.Number;
            SudokuGridSquares.First(s => s.XCoordinate == panel.Column + 1 && s.YCoordinate == panel.Row + 1).Number = panel.Number;
            if (CheckForWinCondition())
            {
                this.View.WinLabel.Visibility = Visibility.Visible;
            }
        }

        public bool CheckForWinCondition() {
            foreach (var number in SudokuGrid) {
                if (!number.HasValue) return false;
            }

            if (SudokuGridSquares.GroupBy(s => new {s.Number, s.XCoordinate}).Count() < 81)
                return false;
            if (SudokuGridSquares.GroupBy(s => new {s.Number, s.YCoordinate}).Count() < 81)
                return false;
            if (SudokuGridSquares.GroupBy(s => new {s.Number, s.BlockNumber}).Count() < 81)
                return false;

            return true;
        }
    }
}
