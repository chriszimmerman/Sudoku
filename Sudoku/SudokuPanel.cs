using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sudoku {
    public class SudokuPanel : StackPanel {
        public bool IsLocked { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int? Number { get; set; }

        public SudokuPanel(int row, int column, int? number, int height, int width) {
            Height = height;
            Width = width;
            Children.Add(MakeBorder(height, width));
            Row = row;
            Column = column;
            Number = number;
            IsLocked = Number != null;
        }

        private Border MakeBorder(int height, int width) {
            return new Border { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black, Height = height, Width = width };
        }

        public void PutBackGroundOnPanel() {
            string imagePath = System.IO.Directory.GetCurrentDirectory() + "..\\..\\..\\Images\\";
            var filename = imagePath + (IsLocked ? "sudoku_hard" : "sudoku_soft")
                         + (Number != null ? Number.Value.ToString() : "_") + ".png";
            var image = new BitmapImage(new Uri(filename, UriKind.Relative));
            var imageBrush = new ImageBrush { ImageSource = image };
            Background = imageBrush;
        }
    }
}
