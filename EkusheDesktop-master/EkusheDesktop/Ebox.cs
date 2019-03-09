using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkusheDesktop
{
    public partial class Ebox : Form
    {

        //Emoji emoji = new Emoji();
        public Ebox()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("😐");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Smiley);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("😐");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SendKeys.Send("😐");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendKeys.Send("😐");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendKeys.Send("😐");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SendKeys.Send("😐");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Smile);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Joy);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Confused);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Disappointed);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Innocent);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Sleepy);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Angry);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Smile);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Sleepy);
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Neutral_Face);
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Cry);
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Flushed);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Smirk);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Smiley);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Grinning);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Confounded);
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Heart_Eyes);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Kissing_Closed_Eyes);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Confused);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Sunglasses);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Disappointed_Relieved);
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Disappointed);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Expressionless);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Smirk);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Astonished);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Wink);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SendKeys.Send(Emoji.Mask);
        }
    }
}
