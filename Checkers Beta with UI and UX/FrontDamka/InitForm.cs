using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrontDamka
{
    public partial class InitForm : Form
    {
        public InitForm()
        {
            InitializeComponent();
        }

        public static bool checkName(string i_NameOfUser)
        {
            bool returnFlag = true;

            for (int i = 0; i < i_NameOfUser.Length; i++)
            {
                if (!char.IsLetter(i_NameOfUser[i]))
                {
                    returnFlag = !returnFlag;
                    break;
                }
            }

            return returnFlag && (i_NameOfUser.Length < 11);
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Text = string.Empty;
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.BackColor = Color.White;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
                textBoxPlayer2.BackColor = SystemColors.Control;
            }
        }

        private void buttonFinished_Click(object sender, EventArgs e)
        {
            bool okPlayer2Name = true;
            bool okPlayer1Name = true;

            if (textBoxPlayer1.Text.Equals(""))
            {
                MessageBox.Show("Please enter player's 1 name in order to play!", "Error");
                okPlayer1Name = !okPlayer1Name;
            }
            else
            {
                if (!checkName(textBoxPlayer1.Text))
                {
                    MessageBox.Show("Please enter a valid player's 1 name in order to play!(maxmium 10 characters only letters..)", "Error");
                    okPlayer1Name = !okPlayer1Name;
                }
            }

            if (checkBoxPlayer2.Checked)
            {
                if (!checkName(textBoxPlayer2.Text))
                {
                    MessageBox.Show("Please enter a valid player's 2 name in order to play!(maxmium 10 characters only letters..)", "Error");
                    okPlayer2Name = !okPlayer2Name;
                }

                if (textBoxPlayer2.Text.Equals(""))
                {
                    MessageBox.Show("Please enter player's 2 name in order to play!", "Error");
                    okPlayer2Name = !okPlayer2Name;
                }
            }

            if (okPlayer1Name && okPlayer2Name)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        public string TextBoxPlayerOne
        {
            get { return textBoxPlayer1.Text; }
        }

        public string TextBoxPlayerTwo
        {
            get
            {
                if (checkBoxPlayer2.Checked)
                {
                    return textBoxPlayer2.Text;
                }
                else
                {
                    return "Computer";
                }
            }
        }

        public bool checkBoxPlayerTwo
        {
            get { return checkBoxPlayer2.Checked; }
        }

        public int NumOfPlayers
        {
            get
            {
                if (checkBoxPlayer2.Checked)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int SizeOfBoard
        {
            get
            {
                if (radioButton6x6.Checked)
                {
                    return 6;
                }
                else
                {
                    if (radioButton8x8.Checked)
                    {
                        return 8;
                    }
                    else
                    {
                        return 10;
                    }
                }
            }
        }
    }
}
