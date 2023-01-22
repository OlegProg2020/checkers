using System.Drawing;

namespace Checkers
{
    public enum Side { dark, light }
    public class Cell
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public Bitmap View { get; set; }

        public Cell(int x, int y)
        {
            View = null;
            Column = x;
            Row = y;
        }
        public Cell()
        {
            View = null;
        }
    }
}
