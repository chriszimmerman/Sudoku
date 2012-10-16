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

namespace SudokuGui {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
      //GenerateGrid(9);
    }

    private void GenerateGrid(int size) {
      var grid = new Grid();

      grid.ShowGridLines = true;

      for (int cell = 0; cell < size; cell++) {
        grid.RowDefinitions.Add(new RowDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
      }

      for (int row = 0; row < size; row++) {
        for (int column = 0; column < size; column++) {
          var panel = new StackPanel();
          panel.Height = 30;
          panel.Width = 30;
          Grid.SetRow(panel, row);
          Grid.SetColumn(panel, column);
          grid.Children.Add(panel);

        }
      }
    }
  }
}
