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
    public partial class MainForm : Form
    {
        const int size = 4;
        const int blockSize = 70;
        const int blockMargin = 7;
        Label[,] map = new Label[size, size];
        int userScore = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartGame(size);
            ShowScore();
            GenerateNumber();
            GenerateNumber();
            GetColor();
        }

        private void StartGame(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var label = CreateLabel(i, j);
                    Controls.Add(label);
                    map[i, j] = label;
                }
            }
        }

        private void ShowScore()
        {
            scoreLabel.Text = "Ваш счет: " + userScore;
        }

        private void GenerateNumber()
        {
            var random = new Random();

            while (true)
            {
                var randomNumber = random.Next(100);
                var randomRow = random.Next(size);
                var randomColumn = random.Next(size);
                if (map[randomRow, randomColumn].Text == "")
                {
                    if (randomNumber < 75)
                    {
                        map[randomRow, randomColumn].Text = "2";
                        break;
                    }
                    else
                    {
                        map[randomRow, randomColumn].Text = "4";
                        break;
                    }
                }
            }
        }

        private Label CreateLabel(int rowIndex, int columnIndex)
        {
            var label = new Label();

            label.BackColor = SystemColors.ButtonShadow;

            label.Font = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label.Size = new Size(blockSize, blockSize);
            label.TextAlign = ContentAlignment.MiddleCenter;

            int x = 10 + columnIndex * (blockSize + blockMargin);
            int y = 100 + rowIndex * (blockSize + blockMargin);

            label.Location = new Point(x, y);
            return label;
        }

        private void GetColor()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j].Text == "")
                    {
                        map[i, j].BackColor = Color.Gray;
                        map[i, j].ForeColor = Color.Black;
                    }
                    else
                    {
                        var color = int.Parse(map[i, j].Text);
                        map[i, j].BackColor = Color.FromArgb(255 / color, 255 / ((color % 10) * 10), 255 / (color % 10));
                        map[i, j].ForeColor = Color.White;
                    }
                }
            }
        }

        private bool EndGame()
        {
            var endGame = false;
            var countEmptyLabel = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j].Text == "")
                    {
                        countEmptyLabel++;
                    }
                }
            }
            if (countEmptyLabel == 0)
            {
                for (int i = 0; i < size - 1; i++)
                {
                    for (int j = 0; j < size - 1; j++)
                    {
                        if (map[i, j].Text != map[i, j + 1].Text || map[i, j].Text != map[i + 1, j].Text)
                        {
                            endGame = true;
                            break;
                        }
                    }
                }
            }
            return endGame;
        }

        private void MainForm_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (EndGame())
            {
                EndGameForm endGame = new EndGameForm();
                endGame.ShowDialog(this);
                Close();
            }
            else
            {
                if (e.KeyCode == Keys.Right)
                {
                    //слияние
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = size - 1; j >= 0; j--)
                        {
                            if (map[i, j].Text != "")
                            {
                                for (int k = j - 1; k >= 0; k--)
                                {
                                    if (map[i, k].Text != "")
                                    {
                                        if ((map[i, j].Text == map[i, k].Text))
                                        {
                                            map[i, k].Text = "";
                                            var score = Convert.ToInt32(map[i, j].Text);
                                            map[i, j].Text = (score * 2).ToString();
                                            userScore += score * 2;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //упорядочивание
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = size - 1; j >= 0; j--)
                        {
                            if (map[i, j].Text == "")
                            {
                                for (int k = j - 1; k >= 0; k--)
                                {
                                    if (map[i, k].Text != "")
                                    {
                                        map[i, j].Text = map[i, k].Text;
                                        map[i, k].Text = "";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (e.KeyCode == Keys.Left)
                {
                    //слияние
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (map[i, j].Text != "")
                            {
                                for (int k = j + 1; k < size; k++)
                                {
                                    if (map[i, k].Text != "")
                                    {
                                        if ((map[i, j].Text == map[i, k].Text))
                                        {
                                            map[i, k].Text = "";
                                            var score = Convert.ToInt32(map[i, j].Text);
                                            map[i, j].Text = (score * 2).ToString();
                                            userScore += score * 2;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //упорядочивание
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (map[i, j].Text == "")
                            {
                                for (int k = j + 1; k < size; k++)
                                {
                                    if (map[i, k].Text != "")
                                    {
                                        map[i, j].Text = map[i, k].Text;
                                        map[i, k].Text = "";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (e.KeyCode == Keys.Up)
                {
                    //слияние
                    for (int j = 0; j < size; j++)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (map[i, j].Text != "")
                            {
                                for (int k = i + 1; k < size; k++)
                                {
                                    if (map[k, j].Text != "")
                                    {
                                        if ((map[k, j].Text == map[i, j].Text))
                                        {
                                            map[k, j].Text = "";
                                            var score = Convert.ToInt32(map[i, j].Text);
                                            map[i, j].Text = (score * 2).ToString();
                                            userScore += score * 2;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //упорядочивание
                    for (int j = 0; j < size; j++)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (map[i, j].Text == "")
                            {
                                for (int k = i + 1; k < size; k++)
                                {
                                    if (map[k, j].Text != "")
                                    {
                                        map[i, j].Text = map[k, j].Text;
                                        map[k, j].Text = "";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (e.KeyCode == Keys.Down)
                {
                    //слияние
                    for (int j = 0; j < size; j++)
                    {
                        for (int i = size - 1; i >= 0; i--)
                        {
                            if (map[i, j].Text != "")
                            {
                                for (int k = i - 1; k >= 0; k--)
                                {
                                    if (map[k, j].Text != "")
                                    {
                                        if ((map[k, j].Text == map[i, j].Text))
                                        {
                                            map[k, j].Text = "";
                                            var score = Convert.ToInt32(map[i, j].Text);
                                            map[i, j].Text = (score * 2).ToString();
                                            userScore += score * 2;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //упорядочивание
                    for (int j = 0; j < size; j++)
                    {
                        for (int i = size - 1; i >= 0; i--)
                        {
                            if (map[i, j].Text == "")
                            {
                                for (int k = i - 1; k >= 0; k--)
                                {
                                    if (map[k, j].Text != "")
                                    {
                                        map[i, j].Text = map[k, j].Text;
                                        map[k, j].Text = "";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ShowScore();
            GenerateNumber();
            GetColor();
        }
    }
}
