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
    public partial class Menu : Form
    {

        BNBox bnbox ;
        english.BNBox enbox;

        public Menu()
        {
            InitializeComponent();
        }
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override void WndProc(ref Message message)
        {

            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
            {
                message.Result = (IntPtr)HTCAPTION;
                //fix = true;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(bnbox == null)
            {
                if (enbox != null)
                {
                    enbox.Close();
                    enbox = null;
                }
                bnbox = new BNBox();
            }
            else if (bnbox != null)
            {
                bnbox.Close();
                bnbox = null;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
           //notifyIcon1.Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (enbox == null)
            {
                if (bnbox != null)
                {
                    bnbox.Close();
                    bnbox = null;
                }

                enbox = new english.BNBox();
            }
            else if(enbox!=null)
            {
                enbox.Close();
                enbox = null;
            }
        }
    }
}
