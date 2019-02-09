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
            form2.Left = 1200;
            form2.Top = 50;
            form2.Show();
            Application.Run();

            
            
        }
    }
}
