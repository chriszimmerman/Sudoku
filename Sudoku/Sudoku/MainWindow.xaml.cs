using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku {
  public partial class MainWindow : Window {
    Grid grid;
    const int squareSize = 30;
    string imagePath = System.IO.Directory.GetCurrentDirectory() + "..\\..\\..\\Images\\";
    string puzzlePath = System.IO.Directory.GetCurrentDirectory() + "..\\..\\..\\Puzzles\\";

    public MainWindow() {
      AddChild(grid);
      InitializeComponent();
    }

    private Grid GenerateGrid(int size, int?[,] gridNumbers) {

      grid = new Grid();

      grid.Height = size * squareSize;
      grid.Width = size * squareSize;

      for (int cell = 0; cell < size; cell++) {
        grid.RowDefinitions.Add(new RowDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
      }

      for (int row = 0; row < size; row++) {
        for (int column = 0; column < size; column++) {
          var currentPosition = row * size + column;

          var panel = new SudokuPanel();
          panel.Height = squareSize;
          panel.Width = squareSize;
          panel.Children.Add(new Border { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black, Height = squareSize, Width = squareSize });
          panel.MouseDown += PanelClick;
          panel.Row = row;
          panel.Column = column;
          panel.Number = gridNumbers[column, row];
          panel.IsLocked = panel.Number != null;


          var filename = imagePath + "sudoku_hard" + (panel.Number != null ? panel.Number.Value.ToString() : "_") + ".png";
          var image = new BitmapImage(new Uri(filename, UriKind.Relative));
          var imageBrush = new ImageBrush();
          imageBrush.ImageSource = image;
          panel.Background = imageBrush;

          Grid.SetRow(panel, row);
          Grid.SetColumn(panel, column);
          grid.Children.Add(panel);
        }
      }
      return grid;
    }

    private void StartButtonClick(object sender, RoutedEventArgs e) {
      DisableButtons(); 
      gamePanel.Children.Clear();
      int boardSize = 9;

      PuzzleGenerator puzzle = new PuzzleGenerator((int)Math.Sqrt(boardSize));
      puzzle.MakePartialSolution(Difficulty.Medium);

      gamePanel.Children.Add(GenerateGrid(boardSize, puzzle.ConvertToArray()));
    }

    private void SolveButtonClick(object sender, RoutedEventArgs e) {
      DisableButtons();
      gamePanel.Children.Clear();

      string filename = Microsoft.VisualBasic.Interaction.InputBox("Please enter the name of a file to solve:", "Puzzle", "test.txt", 0, 0);
      PuzzleSolver solution = new PuzzleSolver();
      solution.SolvePartialSolution(puzzlePath + filename);

      int boardSize = 9;
      gamePanel.Children.Add(GenerateGrid(boardSize, solution.ConvertToArray()));
    }

    private void DisableButtons()
    {
      startButton.Visibility = Visibility.Hidden;
      startButton.IsEnabled = false;
      solveButton.Visibility = Visibility.Hidden;
      solveButton.IsEnabled = false;
    }

    public Popup MakeChoicesPopup() {
      Grid choicesGrid = new Grid();
      int blockHeight = 3;
      int blockWidth = 3;

      choicesGrid.Height = blockHeight * squareSize;
      choicesGrid.Width = blockWidth * squareSize;

      for (int cell = 0; cell < blockWidth; cell++)
      {
        choicesGrid.RowDefinitions.Add(new RowDefinition());
        choicesGrid.ColumnDefinitions.Add(new ColumnDefinition());
      }

      choicesGrid.Height = 3 * squareSize;
      choicesGrid.Width = 3 * squareSize;

      for (int row = 0; row < blockHeight; row++)
      {
        for (int column = 0; column < blockWidth; column++)
        {
          var currentPosition = row * blockHeight + column;

          var panel = new SudokuPanel();
          panel.Height = squareSize;
          panel.Width = squareSize;
          panel.Children.Add(new Border { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black, Height = blockHeight, Width = blockWidth });
          panel.Row = row;
          panel.Column = column;
          panel.Number = currentPosition + 1;
          panel.IsLocked = panel.Number != null;


          var filename = imagePath + "sudoku_hard" + panel.Number.Value.ToString() + ".png";
          var image = new BitmapImage(new Uri(filename, UriKind.Relative));
          var imageBrush = new ImageBrush();
          imageBrush.ImageSource = image;
          panel.Background = imageBrush;

          Grid.SetRow(panel, row);
          Grid.SetColumn(panel, column);
          choicesGrid.Children.Add(panel);
        }
      }

      Popup popup = new Popup();
      popup.Child = choicesGrid;
      return popup;
    }

    public void PanelClick(object sender, RoutedEventArgs e) {
      //TODO: make popup with child panel that has n * n child panels
      var newPanel = (SudokuPanel)sender;

      if (!newPanel.IsLocked) {
        Popup popup = MakeChoicesPopup();
        popup.Placement = PlacementMode.MousePoint;
        popup.IsOpen = true;  
      }
    }

    public void Close(object sender, RoutedEventArgs e) {
      Environment.Exit(0);
    }
  }
}
