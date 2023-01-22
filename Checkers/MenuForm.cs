using System;
using System.Windows.Forms;

namespace Checkers
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
