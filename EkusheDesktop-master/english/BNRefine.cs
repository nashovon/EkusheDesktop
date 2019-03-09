using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace english
{
    public class BNRefine
    {


        int capital = 1;
        int pressed = 0;
        Suggestion suggestion = new Suggestion("english_trie_file.txt");
        Prediction prediction = new Prediction("test_corpus_eng.txt");


        public static string output = null;
        public static string prev_output = null;

        public static string[] central = { "1", "2", "3", "4", "5" };





        public void press_space(string temp)
        {
            
            if (!string.IsNullOrEmpty(temp)) output = temp;
            pressed = 1;
   
             if (!string.IsNullOrEmpty(output))
            {
                //SendMessage(GetActiveWindowTitle(), 0x000C, 0, output);
                SendKeys.Send(output);
                prev_output = output;
                output = null;

            }


        }

        public bool press_backspace()
        {


            if (!string.IsNullOrEmpty(output))
            {
                output = output.Remove(output.Length - 1, 1);
                return false;
            }
            else return true;

        }

        public void press_number(int temp)
        {

            if (temp == 48) output += "0";
            else if (temp == 49) output += "1";
            else if (temp == 50) output += "2";
            else if (temp == 51) output += "3";
            else if (temp == 52) output += "4";
            else if (temp == 53) output += "5";
            else if (temp == 54) output += "6";
            else if (temp == 55) output += "7";
            else if (temp == 56) output += "8";
            else if (temp == 57) output += "9";


        }

        public void compose(int temp)
        {
            temp += 32 * capital;
            char letter = Convert.ToChar(temp);
            output = output + letter.ToString();
        }

   
        public void converter(int input)
        {

          


             if (input == 160) capital = 1 - capital;
           
           
            else if (input >= 48 && input <= 57)
            {
                if (pressed == 0)
                {
                    pressed = 1;

                    if (capital == 1)
                    {

                        try
                        {
                            
                            press_space(central[input - 49]);
                            SendKeys.Send(" ");
                        }
                        catch (Exception) { Debug.WriteLine("1-5 only"); }
                    }

                    else if (capital == 0) press_number(input);



                }
                else pressed = 0;
            }


            else if ((input >= 65 && input <= 90))
            {
                if (pressed == 0)
                {

                    pressed = 1;
                    prev_output = null;
                    compose(input);
                }
                else pressed = 0;
            }


        }


    }
}

