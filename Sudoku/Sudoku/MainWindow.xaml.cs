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
  public partial class MainWindow : Window {
    Grid grid;
    string imagePath = System.IO.Directory.GetCurrentDirectory() + "..\\..\\..\\Images\\";

    public MainWindow() {
      grid = GenerateGrid(9);
      AddChild(grid);
      InitializeComponent();
    }

    private Grid GenerateGrid(int size) {
      PuzzleGenerator p = new PuzzleGenerator((int)Math.Sqrt(size));
      p.MakePartialSolution(Difficulty.Medium);
      var gridNumbers = p.ConvertToArray();

      grid = new Grid();

      grid.Height = size * 30;
      grid.Width = size * 30;

      for (int cell = 0; cell < size; cell++) {
        grid.RowDefinitions.Add(new RowDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
      }

      for (int row = 0; row < size; row++) {
        for (int column = 0; column < size; column++) {
          var currentPosition = row * size + column;

          var panel = new SudokuPanel();
          panel.Height = 30;
          panel.Width = 30;
          panel.Children.Add(new Border { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black, Height = 30, Width = 30 });
          panel.MouseDown += PanelClick;
          panel.Row = row;
          panel.Column = column;
          panel.Number = gridNumbers[row, column];
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
      startButton.Visibility = Visibility.Hidden;
      startButton.IsEnabled = false;
      gamePanel.Children.Add(GenerateGrid(9));
    }

    public void PanelClick(object sender, RoutedEventArgs e) {
      var newPanel = (SudokuPanel)sender;

      if (!newPanel.IsLocked) {
        var filename2 = imagePath + "sudoku_soft9.png";
        var image2 = new BitmapImage(new Uri(filename2, UriKind.Relative));
        var imageBrush2 = new ImageBrush();
        imageBrush2.ImageSource = image2;
        newPanel.Background = imageBrush2;
      }
    }
  }
}
