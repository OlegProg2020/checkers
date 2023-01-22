using System;
using System.Windows.Forms;

namespace Checkers
{
    public class Checker : Cell
    {
        public Side Side { get; set; }
        public virtual bool IsPossibleMove(Cell endPoint, Panel[,] grid)
        {
            throw new NotImplementedException();
        }
        public virtual void MoveTo(ref Cell endPoint, ref Panel[,] grid)
        {
            throw new NotImplementedException();
        }
        public bool IsAlly(int i, int j, Panel[,] board)
        {
            Checker checker = board[i, j].Tag as Checker;
            return checker.Side == Side;
        }
    }
}
