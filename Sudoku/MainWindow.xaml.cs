using System;
using System.Windows;
using System.Windows.Controls;

namespace Sudoku {
    public partial class MainWindow : Window {
        readonly string puzzlePath = System.IO.Directory.GetCurrentDirectory() + "..\\..\\..\\Puzzles\\";

        public MainWindow() {
            InitializeComponent();
        }

        private void StartButtonClick(object sender, RoutedEventArgs e) {
            MainGrid.Children.Clear();
            const int boardSize = 9;

            var puzzle = new PuzzleGenerator((int)Math.Sqrt(boardSize));
            puzzle.MakePartialSolution(Difficulty.Medium);

            MainGrid.Children.Add(new SudokuPuzzleView(boardSize, puzzle.ConvertToArray()));
        }

        private void SolveButtonClick(object sender, RoutedEventArgs e) {
            MainGrid.Children.Clear();

            string filename = Microsoft.VisualBasic.Interaction.InputBox("Please enter the name of a file to solve:", "Puzzle", "test.txt", 0, 0);
            var solution = new PuzzleSolver();
            solution.SolvePartialSolution(puzzlePath + filename);

            const int boardSize = 9;
            MainGrid.Children.Add(new SudokuPuzzleView(boardSize, solution.ConvertToArray()));
        }

        public void Close(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }
    }
}
