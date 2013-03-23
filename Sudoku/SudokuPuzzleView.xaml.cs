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
        private int?[,] boardAs2DArray;
        private List<Square> board; 

        public SudokuPuzzleView() {
            int size = 9;
            InitializeComponent();

            var puzzle = new PuzzleGenerator((int)Math.Sqrt(size));
            board = puzzle.MakePartialSolution(Difficulty.Hard);
            boardAs2DArray = puzzle.PartialSolutionAs2DArray;
            
            CreateRowAndColumnDefinitions(size);

            for (var row = 0; row < size; row++) {
                for (var column = 0; column < size; column++) {
                    var panel = new SudokuPanel(row, column, boardAs2DArray[column, row], squareSize, squareSize);
                    panel.MouseDown += PanelClick;

                    panel.PutBackGroundOnPanel();

                    this.SudokuGrid.Children.Add(panel);
                    Grid.SetRow(panel, row);
                    Grid.SetColumn(panel, column);
                }
            }
            presenter = new SudokuGridPresenter { SudokuGrid = boardAs2DArray, SudokuGridSquares = board, View = this };
        }

        private void CreateRowAndColumnDefinitions(int blockWidth) {
            for (int rowAndColumnNumbers = 0; rowAndColumnNumbers < blockWidth; rowAndColumnNumbers++) {
                this.SudokuGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(squareSize)});
                this.SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(squareSize)});
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
        }
    }
}
