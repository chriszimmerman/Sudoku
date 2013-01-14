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
            DisableButtons();
            gamePanel.Children.Clear();
            const int boardSize = 9;

            var puzzle = new PuzzleGenerator((int)Math.Sqrt(boardSize));
            puzzle.MakePartialSolution(Difficulty.Medium);

            gamePanel.Children.Add(new SudokuGridView(boardSize, puzzle.ConvertToArray()));
        }

        private void SolveButtonClick(object sender, RoutedEventArgs e) {
            DisableButtons();
            gamePanel.Children.Clear();

            string filename = Microsoft.VisualBasic.Interaction.InputBox("Please enter the name of a file to solve:", "Puzzle", "test.txt", 0, 0);
            var solution = new PuzzleSolver();
            solution.SolvePartialSolution(puzzlePath + filename);

            const int boardSize = 9;
            gamePanel.Children.Add(new SudokuGridView(boardSize, solution.ConvertToArray()));
        }

        private void DisableButtons() {
            startButton.Visibility = Visibility.Hidden;
            startButton.IsEnabled = false;
            solveButton.Visibility = Visibility.Hidden;
            solveButton.IsEnabled = false;
        }

        public void Close(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }
    }
}
