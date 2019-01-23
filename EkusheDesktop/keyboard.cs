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
    public partial class keyboard : Form
    {
        bool state = false;
        GlobalKeyboardHook gHook = new GlobalKeyboardHook();


        public keyboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (state == false)
            {
                state = true;
                gHook.hook();
                Box b = new Box();
                b.Show();
            }
            else
            {
                state = false;
                gHook.unhook();
               
            }
        }
    }
}
