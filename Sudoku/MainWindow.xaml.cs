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
            MainGrid.Children.Add(SudokuMenu);
            Grid.SetRow(SudokuMenu,0);
            Grid.SetColumn(SudokuMenu,0);
            var puzzle = new SudokuPuzzleView();
            MainGrid.Children.Add(puzzle);
            Grid.SetRow(puzzle, 1);
            Grid.SetColumn(puzzle,0);
        }

        private void SolveButtonClick(object sender, RoutedEventArgs e) {
            MainGrid.Children.Clear();

            string filename = Microsoft.VisualBasic.Interaction.InputBox("Please enter the name of a file to solve:", "Puzzle", "test.txt", 0, 0);
            var solution = new PuzzleSolver();
            solution.SolvePartialSolution(puzzlePath + filename);

            MainGrid.Children.Add(new SudokuPuzzleView());
        }

        public void Close(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }
    }
}
