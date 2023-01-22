using System;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    public partial class GameForm : Form
    {
        private static Panel[,] Board;
        public GameForm()
        {
            InitializeComponent();
        }
        private static int GetCountCheckers(Side side)
        {
            int count = 0;
            foreach (var panel in Board)
            {
                try
                {
                    Checker checker = panel.Tag as Checker;
                    if (checker.Side == side)
                        count++;
                }
                catch { }
            }
            return count;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            const int tileSize = 88;
            const int gridSize = 8;
            var light = Color.FromArgb(252, 216, 162);
            var dark = Color.FromArgb(140, 85, 35);

            Board = new Panel[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)  //i = column
            {
                for (int j = 0; j < gridSize; j++)  //j = row
                {
                    // create new Panel which will be one board tile
                    var newPanel = new Panel();
                    newPanel.Width = newPanel.Height = tileSize;
                    newPanel.Location = new Point(tileSize * i, tileSize * j);
                    Board[i, j] = newPanel;

                    // backgrounds image
                    Cell cell = new Cell(i, j);

                    if (i % 2 == 0)
                        newPanel.BackColor = j % 2 == 0 ? light : dark;
                    else
                        newPanel.BackColor = j % 2 == 0 ? dark : light;

                    if (j % 2 == 0 && i % 2 != 0 || j % 2 != 0 && i % 2 == 0)
                    {
                        if (j < 3)
                            cell = new LightChecker(i, j);
                        if (j > 4)
                            cell = new DarkChecker(i, j);
                    }

                    newPanel.BackgroundImage = cell.View;
                    newPanel.BackgroundImageLayout = ImageLayout.Stretch;
                    newPanel.Tag = cell;

                    newPanel.AllowDrop = true;
                    newPanel.MouseDown += Panel_MouseDown;
                    newPanel.DragEnter += Panel_DragEnter;
                    newPanel.DragDrop += Panel_DragDrop;

                    panel1.Controls.Add(Board[i, j]);
                }
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel.BackgroundImage != null)
                DoDragDrop(panel, DragDropEffects.Copy);
        }

        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            Panel panel = sender as Panel;

            if (e.Data.GetDataPresent(panel.GetType()))
                e.Effect = DragDropEffects.Copy;
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            Panel panel = sender as Panel;
            Panel panel2 = (Panel)e.Data.GetData(panel.GetType());

            try
            {
                Cell endPoint = panel.Tag as Cell;
                Checker startPoint = panel2.Tag as Checker;

                if (startPoint.IsPossibleMove(endPoint, Board))
                    startPoint.MoveTo(ref endPoint, ref Board);

                if (GetCountCheckers(Side.dark) == 0)
                    MessageBox.Show("Светлая сторона победила!");
                else if (GetCountCheckers(Side.light) == 0)
                    MessageBox.Show("Тёмная сторона победила!");
            }
            catch
            { }
        }
    }
}
