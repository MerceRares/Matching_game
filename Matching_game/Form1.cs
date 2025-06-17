using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_game
{

    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "p","p","d","d","S","S","j","j",
            "l","l","t","t","b","b","z","z"
        };

        Label firstClicked, secondClicked;
        Timer gameTimer = new Timer();
        int timeLeft = 60;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

            timeLabel.Text = "Time Left: 60s";

            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            timeLabel.Text = $"Time Left: {timeLeft}s";

            if (timeLeft <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("Time's up! YOU LOOSE!");
                this.Close();
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label;
            if (clickedLabel == null || clickedLabel.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
                return;
            }

            timer1.Start();
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label label = control as Label;
                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            gameTimer.Stop();
            MessageBox.Show("You matched all the icons!!!");
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
            }
        }
    }
}