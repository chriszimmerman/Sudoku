using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku {
  public class SudokuPanel : StackPanel {
    public bool IsLocked { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public int? Number { get; set; }
  }
}
