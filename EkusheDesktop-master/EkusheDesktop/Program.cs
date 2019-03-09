using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkusheDesktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Box box = new Box();
            Menu form2 = new Menu();
            form2.StartPosition = FormStartPosition.Manual;
            var screenWidth = Screen.PrimaryScreen.Bounds.Width;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height;
            form2.Left = screenWidth-70;
            form2.Top = 0;
            form2.Show();
            Application.Run();

            
            
        }
    }
}
