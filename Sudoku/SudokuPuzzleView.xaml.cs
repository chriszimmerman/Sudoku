using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku {
    /// <summary>
    /// Interaction logic for SudokuPuzzleView.xaml
    /// </summary>
    public partial class SudokuPuzzleView : UserControl {
        private const int squareSize = 30;
        private SudokuGridPresenter presenter;

        public SudokuPuzzleView(int size, int?[,] gridNumbers) {
            InitializeComponent();
            Height = size * squareSize;
            Width = size * squareSize;

            CreateRowAndColumnDefinitions(size);

            for (var row = 0; row < size; row++) {
                for (var column = 0; column < size; column++) {
                    var panel = new SudokuPanel(row, column, gridNumbers[column, row], squareSize, squareSize);
                    panel.MouseDown += PanelClick;

                    panel.PutBackGroundOnPanel();

                    Grid.SetRow(panel, row);
                    Grid.SetColumn(panel, column);
                    SudokuGrid.Children.Add(panel);
                }
            }
            presenter = new SudokuGridPresenter {SudokuGrid = gridNumbers};
        }

        private void CreateRowAndColumnDefinitions(int blockWidth) {
            for (int rowAndColumnNumbers = 0; rowAndColumnNumbers < blockWidth; rowAndColumnNumbers++) {
                SudokuGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(squareSize)});
                SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(squareSize)});
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
