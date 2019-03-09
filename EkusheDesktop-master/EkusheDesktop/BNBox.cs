using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkusheDesktop
{
    public partial class BNBox : Form
    {


        Suggestion suggestion = new Suggestion("test1.txt");
        Prediction prediction = new Prediction("test_corpus.txt");
        BNRefine refine = new BNRefine();
        Parser parser = new Parser();
        BNGlobalKeyboardHook ghook = new BNGlobalKeyboardHook();
        BanglaUnicode mapps = new BanglaUnicode();
        bool hooked;

        bool fix;


        public BNBox()
        {
            InitializeComponent();
            timer1.Start();
            hooked = false;
            
        }





        #region Data Members & Structures 



        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct RECT
        {
            public uint Left;
            public uint Top;
            public uint Right;
            public uint Bottom;
        };

        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct GUITHREADINFO
        {
            public uint cbSize;
            public uint flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public RECT rcCaret;
        };

        string[] str_suggestion = new string[5]; //////////data
        /// </summary>
        /// // Point required for ToolTip movement by Mouse
        GUITHREADINFO guiInfo;                     // To store GUI Thread Information
        Point caretPosition;                     // To store Caret Position  

        #endregion




        #region DllImports 

        /*- Retrieves Title Information of the specified window -*/
        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        /*- Retrieves Id of the thread that created the specified window -*/
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, out uint lpdwProcessId);

        /*- Retrieves information about active window or any specific GUI thread -*/
        [DllImport("user32.dll", EntryPoint = "GetGUIThreadInfo")]
        public static extern bool GetGUIThreadInfo(uint tId, out GUITHREADINFO threadInfo);

        /*- Retrieves Handle to the ForeGroundWindow -*/
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /*- Converts window specific point to screen specific -*/
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, out Point position);



        #endregion



        #region Event Handlers 


        string temp = null;
        string main = null;


        private void timer1_Tick(object sender, EventArgs e)
        {


            if (GetForegroundWindow() == this.Handle)
            {
                // then do no processing


                return;
            }

            // Get Current active Process
            //string activeProcess = GetActiveProcess();
            main = BNRefine.output;



            if (main != temp && main != null)
            {
                suggestion.Getlist(main);
                show_sugg();
                temp = main;
            }

            if (main == null && temp != null)
            {
                //Console.WriteLine(temp);
                prediction.Getlist(parser.toBangla(BNRefine.prev_output));
                show_pred();
            }



    
            EvaluateCaretPosition();


            if (fix == false)
            {
                AdjustUI();
            }

        }

        #endregion




        #region Methods 




        private void AdjustUI()
        {
            // Get Current Screen Resolution
            Rectangle workingArea = SystemInformation.WorkingArea;

            // If current caret position throws Tooltip outside of screen area
            // then do some UI adjustment.
            if (caretPosition.X + this.Width > workingArea.Width)
            {
                caretPosition.X = caretPosition.X - this.Width - 50;
            }

            if (caretPosition.Y + this.Height > workingArea.Height)
            {
                caretPosition.Y = caretPosition.Y - this.Height - 50;
            }

            this.Left = caretPosition.X;
            this.Top = caretPosition.Y;
        }

        /// <summary>
        /// Evaluates Cursor Position with respect to client screen.
        /// </summary>
        private void EvaluateCaretPosition()
        {


            caretPosition = new Point();

            // Fetch GUITHREADINFO
            GetCaretPosition();

            caretPosition.X = (int)guiInfo.rcCaret.Left;
            caretPosition.Y = (int)guiInfo.rcCaret.Bottom;

            if ((caretPosition.X > 0 && caretPosition.Y > 0) || GetForegroundProcessName() == "chrome")
            {

                caretPosition.X += 25;
                caretPosition.Y += 25;
                //    Debug.WriteLine("True");
                ClientToScreen(guiInfo.hwndCaret, out caretPosition);

                if (hooked == false)
                {
                    ghook.hook();
                    this.Visible = true;
                    hooked = true;
                }

            }
            else if (hooked == true)
            {
                ghook.unhook();
                this.Visible = false;
                hooked = false;
            }



        }

        /// <summary>
        /// Get the caret position
        /// </summary>
        public void GetCaretPosition()
        {
            guiInfo = new GUITHREADINFO();
            guiInfo.cbSize = (uint)Marshal.SizeOf(guiInfo);

            // Get GuiThreadInfo into guiInfo
            GetGUIThreadInfo(0, out guiInfo);

        }



        private string GetForegroundProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();

            // The foreground window can be NULL in certain circumstances, 
            // such as when a window is losing activation.
            if (hwnd == null)
                return "Unknown";

            uint pid;
            GetWindowThreadProcessId((int)hwnd, out pid);

            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.Id == pid)
                    return p.ProcessName;
            }

            return "Unknown";
        }
        /// <summary>
        /// Retrieves name of active Process.
        /// </summary>
        /// <returns>Active Process Name</returns>
        //private string GetActiveProcess()
        //{
        //    const int nChars = 256;
        //    int handle = 0;
        //    StringBuilder Buff = new StringBuilder(nChars);
        //    handle = (int)GetForegroundWindow();

        //    // If Active window has some title info
        //    if (GetWindowText(handle, Buff, nChars) > 0)
        //    {
        //        uint lpdwProcessId;
        //        uint dwCaretID = GetWindowThreadProcessId(handle, out lpdwProcessId);
        //        uint dwCurrentID = (uint)Thread.CurrentThread.ManagedThreadId;
        //        return Process.GetProcessById((int)lpdwProcessId).ProcessName;
        //    }
        //    // Otherwise either error or non client region
        //    return String.Empty;
        //}



        #endregion



        //This method loads listbox with item 
        private void Form1_Load(object sender, EventArgs e) //loads first
        {

            //////

            ///
            // Just access the string array using class name where it resides
            //string[] str_suggestion = { "A", "A", "A", "A", "A" };

            //foreach (string ss in str_suggestion)
            //{
            //    listBox1.Items.Add(ss);
            //}
            // Adds words.





            //listBox1.Items.Add("Hello");

        }



        public void show_sugg()
        {
            //Console.WriteLine("update");
            listBox1.Items.Clear();

            listBox1.Items.Add(BNRefine.output);

            listBox1.Items.Add("\n");


            if (mapps.getDirect(BNRefine.output) != null) listBox1.Items.Add(parser.toBangla(mapps.getDirect(BNRefine.output)));

            else listBox1.Items.Add(parser.toBangla(BNRefine.output));

            listBox1.Items.Add("\n");

            int i = 1;
            foreach (string ss in BNRefine.central)
            {
                if (i > 5) break;
                listBox1.Items.Add(" " + i + " " + parser.toBangla(ss));
                listBox1.Items.Add("\n");
                i++;
            }

        }

        public void show_pred()
        {

            listBox1.Items.Clear();

            listBox1.Items.Add("\n");
            listBox1.Items.Add("\n");
            listBox1.Items.Add("\n");


            int i = 1;
            foreach (string ss in BNRefine.central)
            {
                if (i > 5) break;
                listBox1.Items.Add(" " + i + " " + parser.toBangla(ss));
                listBox1.Items.Add("\n");
                i++;
            }

            // }
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
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }

        }

        private void onFocusChange(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        //This method helps to select item by mouse clicking
        private void listBox1_itemSelected(object sender, EventArgs e)
        {
            //MessageBox.Show("Selectd by Mouse click -->" + listBox1.SelectedItem.ToString());
            //refine.print(listBox1.SelectedItem.ToString());
            //SendKeys.Send(listBox1.SelectedItem.ToString());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           // Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (fix) fix = false;
            else fix = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ebox ebox = new Ebox();
            //if (!emoji)
            //{
                ebox.Show();
                //emoji = true;
            //}
            //else
            //{
            //    ebox=null;
            //    emoji = false;
            //}

        }


        //This method helps to selet item by Enter/space key pressing


        //private void listBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        MessageBox.Show("Selected by Enter key -->" + listBox1.SelectedItem.ToString());
        //    }
        //    else if (e.KeyCode == Keys.Space)
        //    {
        //        MessageBox.Show("Selected by space Key -->" + listBox1.SelectedItem.ToString());
        //    }
        //    else if (e.KeyCode == Keys.Escape)
        //    {
        //        listBox1.Items.Clear();
        //    }
        //    else if (e.KeyCode == Keys.Back)
        //    {
        //        listBox1.Items.Clear();

        //        //UpdateListBox(Refine.out);
        //    }


        //}

    }
}
