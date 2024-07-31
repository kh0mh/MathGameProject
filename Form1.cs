using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace MathGame
{
    public partial class Form1 : Form
    {
        private ErrorProvider ErrorProvider;
        private SoundPlayer SoundPlayer;
        public Form1()
        {
            InitializeComponent();
            InitializeSoundsError();
           
        }
        void InitializeSoundsError()
        {
            ErrorProvider = new ErrorProvider();
            SoundPlayer = new SoundPlayer();

            ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

        }
        private void AnswerTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                ErrorProvider.SetError(AnswerTextBox, "Invalid value");
                e.Handled = true;
            }
            else
            {
                ErrorProvider.Clear();
            }

        }
       
        private void RadiosLevel_CheckedChanged_1(object sender, EventArgs e)
        {
           
            Guna.UI2.WinForms.Guna2CustomRadioButton radioG = (Guna.UI2.WinForms.Guna2CustomRadioButton)sender;
            user.Level = radioG.Text;
            user.IsAnsweredL = true;
            AllDone();
        }

        private void RadiosType_CheckedChanged(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2CustomRadioButton radioG = (Guna.UI2.WinForms.Guna2CustomRadioButton)sender;
            user.Type = radioG.Text;
            user.IsAnswesrdT = true;
            AllDone();
           
        }

        
        Random Random = new Random();
       
        struct User
        {
            public int ManyRounds;

            public string Level, Type;

            public float Fnumber, Lnumber, Enumber, UsersAnswer;

            public int MixL, MixT , CountIfWinOrn ;

            public bool IsLmix, IsTmix, IsAnsweredL, IsAnswesrdT, IsAnswersedR, AllAnswersed;

            public short CorrectAnswers , WrongAnswers ;

        };
         User user;

       
        private void UpDownRounds_ValueChanged_1(object sender, EventArgs e)
        {
            user.IsAnswersedR = true;
            user.ManyRounds = int.Parse(UpDownRounds.Value.ToString());
            user.CountIfWinOrn = user.ManyRounds;
            AllDone();
        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            AnswerTextBox.Clear();
            AnswerTextBox.FillColor = Color.White;
            ErrorProvider.Clear();
        }

        void AllDone()
        {
            if(user.IsAnsweredL && user.IsAnswersedR && user.IsAnswesrdT)
            {
                user.AllAnswersed = true;
                ErrorProvider.Clear();
               
            }
        }

     
      
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            label1.Text = MS + ":" + S;

            if (++MS == 99)
            {
                S++;
                MS = 0;
               // label1.Text += MS + ":" + S;
               
            }
            if (S == 1)
            {
                GameOver();
                timer1.Stop();

            }


        }

        void GameOver()
        {
            EndScreenLabl.Visible = true;
            EndScreenLabl.Dock = DockStyle.Fill;
            CorrectALabel.Text = user.CorrectAnswers.ToString();
            WrongALabel.Text = user.WrongAnswers.ToString();
            ManyRLabel.Text = user.CountIfWinOrn.ToString();
            
            if(user.CountIfWinOrn == user.CorrectAnswers || user.CorrectAnswers > user.WrongAnswers)
            {
                FinalResult.ForeColor = Color.LightGreen;
                FinalResult.Text = "You Win";
              
            }
            else if(user.WrongAnswers == user.CorrectAnswers)
            {
                FinalResult.Text = "";
            }
            else
            {
               FinalResult.ForeColor = Color.Red;
               FinalResult.Text = "You Lost";
               System.Media.SystemSounds.Hand.Play();
            }


            ErrorProvider.Clear();
            UpDownRounds.Visible = false;

            listBox1.Visible = false;
        }

        void IsTheCorrectAnswer()
        {

            user.UsersAnswer = float.Parse(AnswerTextBox.Text);
           // listBox1.Items.Add(SecondL + user.UsersAnswer +  " Correct Answer is " + user.Enumber);

            if (user.UsersAnswer != user.Enumber)
            {
                listBox1.Items.Add(SecondL + user.UsersAnswer + " Correct Answer is " + user.Enumber);
                
                AnswerTextBox.FillColor = Color.MistyRose;
                ErrorProvider.SetError(AnswerTextBox, "Wrong Answer");
                user.WrongAnswers++;
               
            }
            else
            {
                listBox1.Items.Add(SecondL + user.UsersAnswer);
                user.CorrectAnswers++;
                AnswerTextBox.FillColor = Color.LightGreen;
            
                ErrorProvider.Clear();
             
            }
            //BtnCheckAnswer.Enabled = false;
        }
       int S = 0, MS = 0;
        private void BtnCheckAnswer_Click(object sender, EventArgs e)
        {
           
            if (!CanCheck || AnswerTextBox.Text == "")
            {
                ErrorProvider.SetError(BtnCheckAnswer,"Start First!");
                return;
            }
            BtnStart.Enabled = true;
            IsTheCorrectAnswer();
          
            if (--user.ManyRounds <= 0)
            {
                timer1.Enabled = true;
               timer1.Start();
                //timer1_Tick(sender, e);
                
                
            }

                    
        }

        short Count = 1;
        bool CanCheck = false , Checing = false;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (Checing)
                Checing = false;
            if (!user.AllAnswersed)
            {
                
                ErrorProvider.SetError(BtnStart, "Answer First !");
                return;
            }
            CanCheck = true;
            
            Count++;
            BtnStart.Text = "Complete Round " + Count;
            //BtnStart.Enabled = false;
            if (user.ManyRounds == 1)
            {
                ErrorProvider.Clear();
            }

            if (AnswerTextBox.FillColor == Color.MistyRose || AnswerTextBox.FillColor == Color.LightGreen)
            {
                AnswerTextBox.Clear();
                AnswerTextBox.FillColor = Color.White;
                ErrorProvider.Clear();
            }

            GetNumbersLevels();
            Mathen();

        }


        string MixLevelfouction()
        {
            user.MixL = Random.Next(1, 4);

            switch (user.MixL)
            {
                case 1:
                    return "Hard";

                case 2:
                    return "Med";

                default:
                    return "Easy";
            }
            
            
        }
        string MixTypefouction()
        {
            user.MixT = Random.Next(1, 5);

            switch (user.MixT)
            {
                case 1:
                    return "Add";

                case 2:
                    return "Sub";

                case 3:
                    return "Mult";

                default:
                    return "Div";


            }
            
        }

        
        void GetNumbersLevels()
        {

            if (user.Level == "Mix")
            {
                user.IsLmix = true;
                user.Level = MixLevelfouction();
            }
                

            switch (user.Level)
            {

                case "Easy":
                    user.Fnumber = Random.Next(1, 10);
                    user.Lnumber = Random.Next(1, 10);
                    break;

                case "Med":
                    user.Fnumber = Random.Next(10, 20);
                    user.Lnumber = Random.Next(10, 20);
                    break;

                case "Hard":
                    user.Fnumber = Random.Next(20, 50);
                    user.Lnumber = Random.Next(20, 50);
                    break;

            }
            AllDone();
            if (user.IsLmix)
            {
                user.Level = "Mix";
                user.IsLmix = false;
            }

        }
        string SecondL = "";
        void Mathen()
        {
            
            if(user.Type == "Mix")
            {
                user.IsTmix = true;
                user.Type = MixTypefouction();
            }
            switch (user.Type)
            {
                case "Add":
                    user.Enumber = user.Fnumber + user.Lnumber;
                    ScreenLable.Text = user.Fnumber + " + " + user.Lnumber + " = " + " ?";
                    SecondL = user.Fnumber + " + " + user.Lnumber + " = ";
                    break;

                case "Sub":
                    user.Enumber = user.Fnumber - user.Lnumber;
                    ScreenLable.Text = user.Fnumber + " - " + user.Lnumber + " = " + " ?";
                    SecondL = user.Fnumber + " - " + user.Lnumber + " = ";

                    break;

                case "Mult":
                    user.Enumber = user.Fnumber * user.Lnumber;
                    ScreenLable.Text = user.Fnumber + " * " + user.Lnumber + " = " + " ?";
                    SecondL = user.Fnumber + " * " + user.Lnumber + " = ";

                    break;

                case "Div":
                    user.Enumber = user.Fnumber / user.Lnumber;
                    ScreenLable.Text = user.Fnumber + " / " + user.Lnumber + " = " + " ?";
                    SecondL = user.Fnumber + " / " + user.Lnumber + " = ";

                    break;
   
            }
            AllDone();
            if (user.IsTmix)
            {
                user.Type = "Mix";
                user.IsTmix = false;
            }
        }

        void UnCheckRadio()
        {
            List<Guna.UI2.WinForms.Guna2CustomRadioButton> button = new Guna.UI2.WinForms.Guna2CustomRadioButton[] { RadioAdd, RadioDiv, RadioMixType, RadioMult, RadioSub, RadioMixLevel, RadioMed, RadioHard, RadioEasy }.ToList();
           
            foreach(Guna.UI2.WinForms.Guna2CustomRadioButton Radios in button)
            {
                Radios.Checked = false;
            }

        }

        //Refresh Varibals
        private void BtnPlayAgain_Click(object sender, EventArgs e) 
        {

            this.Refresh();

            MS = 0; 
            S = 0;

            BtnStart.Checked = false;
            BtnCheckAnswer.Checked = false;
            AnswerTextBox.Clear();
            ScreenLable.Text = "";
            BtnStart.Text = "Start Game";

            UnCheckRadio();

            CanCheck = false;
            Count = 0;
            SecondL = "";

            listBox1.Items.Clear();

            UpDownRounds.Value = 0;

            user.ManyRounds = 0;

            user.WrongAnswers = 0;
            user.UsersAnswer = 0;
            user.CorrectAnswers = 0;
            user.CountIfWinOrn = 0;

            user.Type = "";
            user.Level = "";

            user.MixT = 0;
            user.MixL = 0;
            
            user.Lnumber = 0;
            user.Fnumber = 0;
            user.Enumber = 0;

            user.AllAnswersed = false;
            user.IsTmix = false;
            user.IsLmix = false;
            user.IsAnswesrdT = false;
            user.IsAnswersedR = false;
            user.IsAnsweredL = false;
            
            
            AnswerTextBox.FillColor = SystemColors.Control;
            
            EndScreenLabl.Dock = DockStyle.None;
            UpDownRounds.Visible = true;
            listBox1.Visible = true;
            EndScreenLabl.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
