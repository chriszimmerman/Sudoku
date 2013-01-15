using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sudoku {
    public class SudokuGridView : Grid {
        private const int squareSize = 30;
        private SudokuGridPresenter presenter;

        public SudokuGridView(int size, int?[,] gridNumbers) {
            Height = size * squareSize;
            Width = size * squareSize;

            CreateRowAndColumnDefinitions(size);

            for (var row = 0; row < size; row++) {
                for (var column = 0; column < size; column++) {
                    var panel = new SudokuPanel(row, column, gridNumbers[column, row], squareSize, squareSize);
                    panel.MouseDown += PanelClick;

                    panel.PutBackGroundOnPanel();

                    SetRow(panel, row);
                    SetColumn(panel, column);
                    this.Children.Add(panel);
                }
            }
            presenter = new SudokuGridPresenter {SudokuGrid = gridNumbers};
        }

        private void CreateRowAndColumnDefinitions(int blockWidth) {
            for (int rowAndColumnNumbers = 0; rowAndColumnNumbers < blockWidth; rowAndColumnNumbers++) {
                RowDefinitions.Add(new RowDefinition());
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public void PanelClick(object sender, RoutedEventArgs e) {
            var panel = (SudokuPanel)sender;
            if (panel.IsLocked) return;
            if (panel.Number == null) {
                panel.Number = 1;
            }
            else
                panel.Number = (panel.Number % 9) + 1;

            panel.PutBackGroundOnPanel();
            presenter.UpdateGrid(panel);

            //TODO: Check for win condition
            //if(presenter.CheckForWinCondition())
        }
    }
}