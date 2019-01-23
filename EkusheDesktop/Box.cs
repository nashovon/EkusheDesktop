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
    public partial class Box : Form
    {
        public Box()
        {
            InitializeComponent();
            timer1.Start();
            //train();
            
        }
        Suggestion suggestion = new Suggestion();
        Prediction prediction = new Prediction();
        Refine refine = new Refine();
        //public void train()
        //{
        //    StreamReader reader = File.OpenText("vocabulary.txt");
        //    string line;
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        string[] items = line.Split(' ');

        //        foreach (string ss in items)
        //        {
        //            trie.AddWord(ss);
        //        }



        //    }
        //}



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
        Point startPosition = new Point();       // Point required for ToolTip movement by Mouse
        GUITHREADINFO guiInfo;                     // To store GUI Thread Information
        Point caretPosition;                     // To store Caret Position  

       // ITrie trie = new Trie();
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

        //int flag = 0;
        //public void str_fillup()
        //{
        //    if (flag == 0)
        //    {
        //        str_suggestion[0] = "A";// { "A", "B", "C", "D", "E" };


        //        flag = 1;
        //    }
        //    return;
        //}

        string temp = null;
        RidmikParser parser = new RidmikParser();


        private void timer1_Tick(object sender, EventArgs e)
        {

            if (GetForegroundWindow() == this.Handle)
            {
                // then do no processing


                return;
            }

            // Get Current active Process
            string activeProcess = GetActiveProcess();
            string main = Refine.output;



            if (main != temp && main != null)
            {
                UpdateListBox(main);
                temp = main;
            }

            if (main==null && temp!=null)
            {
                //Console.WriteLine(temp);
                show_pred(parser.toBangla(temp));
            }



            //Call this function just for checking the array is filled up or not
            //str_fillup();

            // If window explorer is active window (eg. user has opened any drive)
            // Or for any failure when activeProcess is nothing               
            if ((activeProcess.ToLower().Contains("explorer") | (activeProcess == string.Empty)))
            {
                // Dissappear 
                this.Visible = false;
            }
            // else if (str_suggestion[0] == null)
            //{
            //  this.Visible = false;
            //}
            else
            {
                // Otherwise Calculate Caret position
                EvaluateCaretPosition();

                // Adjust ToolTip according to the Caret
                AdjustUI();

                // Display current active Process on Tooltip
                // lblCurrentApp.Text = " You are Currently inside : " + activeProcess;
                this.Visible = true;
            }
        }


        #endregion




        #region Methods 



        /// <summary>
        /// This function will adjust Tooltip position and
        /// will keep it always inside the screen area.
        /// </summary>
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

            caretPosition.X = (int)guiInfo.rcCaret.Left + 25;
            caretPosition.Y = (int)guiInfo.rcCaret.Bottom + 25;

            ClientToScreen(guiInfo.hwndCaret, out caretPosition);

            //txtCaretX.Text = (caretPosition.X).ToString();
            //txtCaretY.Text = caretPosition.Y.ToString();

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

        /// <summary>
        /// Retrieves name of active Process.
        /// </summary>
        /// <returns>Active Process Name</returns>
        private string GetActiveProcess()
        {
            const int nChars = 256;
            int handle = 0;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = (int)GetForegroundWindow();

            // If Active window has some title info
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                uint lpdwProcessId;
                uint dwCaretID = GetWindowThreadProcessId(handle, out lpdwProcessId);
                uint dwCurrentID = (uint)Thread.CurrentThread.ManagedThreadId;
                return Process.GetProcessById((int)lpdwProcessId).ProcessName;
            }
            // Otherwise either error or non client region
            return String.Empty;
        }


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

        public void UpdateListBox(string main)
        {
            //Console.WriteLine("update");
            listBox1.Items.Clear();
            IEnumerable<string> PrefixWords = suggestion.Getlist(Refine.output);

            listBox1.Items.Add(main);

            listBox1.Items.Add("\n\n\n");

            int i = 1;
            foreach (string ss in PrefixWords)
            {
                if (i > 5) break;
                listBox1.Items.Add(i + " " + parser.toBangla(ss));
                listBox1.Items.Add("\n");
                //Console.WriteLine(ss);
                i++;
            }

    }

        public void show_pred(string ss)
        {
           
            listBox1.Items.Clear();

            
            string[] words = prediction.Getlist(ss);

            listBox1.Items.Add("\n\n\n");

            int j = 1;
                for (int i=words.Length-1; ;i--)
                {
                    if (j > 5 || i<=0) break;
                    listBox1.Items.Add(j+" "+words[i]);
                    listBox1.Items.Add("\n");
                   // Debug.WriteLine(words[i]);
                    j++;
                }
            
            // }
        }

        //public string GetWord(int n)
        //{

        //    IEnumerable<string> PrefixWords = trie.GetWords(Refine.output);

        //    int i = 1;
        //    string value = null;

        //    foreach (string ss in PrefixWords)
        //    {
        //        if (i > n) break;
        //        value = ss;
        //        //Console.WriteLine(ss);
        //        i++;
        //    }

        //    return value;
        //}


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


        //This method helps to selet item by Enter/space key pressing


        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Selected by Enter key -->" + listBox1.SelectedItem.ToString());
            }
            else if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Selected by space Key -->" + listBox1.SelectedItem.ToString());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                listBox1.Items.Clear();
            }
            else if (e.KeyCode == Keys.Back)
            {
                listBox1.Items.Clear();

                //UpdateListBox(Refine.out);
            }


        }

    }
}
