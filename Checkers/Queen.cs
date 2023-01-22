using System.Windows.Forms;

namespace Checkers
{
    internal class Queen : Checker
    {
        public Queen(int x, int y, Side side)
        {
            Column = x;
            Row = y;
            Side = side;
            View = side == Side.dark ? Properties.Resources.dark_queen : Properties.Resources.light_queen;
        }

        public override bool IsPossibleMove(Cell endPoint, Panel[,] board)
        {
            int x = endPoint.Column;
            int y = endPoint.Row;

            if (board[x, y].BackgroundImage == null)
            {
                if ((x == Column + 1 || x == Column - 1) && (y == Row - 1 || y == Row + 1) ||
                    (x == Column + 2) && (y == Row - 2) && !IsAlly(x - 1, y + 1, board) ||
                    (x == Column - 2) && (y == Row - 2) && !IsAlly(x + 1, y + 1, board) ||
                    (x == Column + 2) && (y == Row + 2) && !IsAlly(x - 1, y - 1, board) ||
                    (x == Column - 2) && (y == Row + 2) && !IsAlly(x + 1, y - 1, board))
                    return true;
            }
            return false;
        }

        public override void MoveTo(ref Cell endPoint, ref Panel[,] board)
        {
            int x = endPoint.Column;
            int y = endPoint.Row;

            if (x == Column + 2 && y == Row - 2)
            {
                board[x - 1, y + 1].Tag = new Cell(x - 1, y + 1);
                board[x - 1, y + 1].BackgroundImage = null;
            }
            else if (x == Column - 2 && y == Row - 2)
            {
                board[x + 1, y + 1].Tag = new Cell(x + 1, y + 1);
                board[x + 1, y + 1].BackgroundImage = null;
            }
            else if (x == Column + 2 && y == Row + 2)
            {
                board[x - 1, y - 1].Tag = new Cell(x - 1, y - 1);
                board[x - 1, y - 1].BackgroundImage = null;
            }
            else if (x == Column - 2 && y == Row + 2)
            {
                board[x + 1, y - 1].Tag = new Cell(x + 1, y - 1);
                board[x + 1, y - 1].BackgroundImage = null;
            }

            board[Column, Row].Tag = new Cell(Column, Row);
            board[Column, Row].BackgroundImage = null;

            Column = x;
            Row = y;
            board[x, y].Tag = this;
            board[x, y].BackgroundImage = View;
        }
    }
}
