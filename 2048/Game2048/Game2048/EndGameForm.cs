using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    public partial class EndGameForm : Form
    {
        public EndGameForm()
        {
            InitializeComponent();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm();
            mainForm.ShowDialog(this);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
