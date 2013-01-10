using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Sudoku
{
    public class SudokuPopup : Popup
    {
        public SudokuPopup(SudokuPanel popupParent = null) {
            this.PopupParent = popupParent;
        }

        public SudokuPanel PopupParent { get; set; }
    }
}
