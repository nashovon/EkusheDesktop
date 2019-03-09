using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace EkusheDesktop
{
    public class BNRefine
    {

        int capital = 1;
        int pressed = 0;
        Parser parser = new Parser();
        Suggestion suggestion = new Suggestion("test1.txt");
        Prediction prediction = new Prediction("test_corpus.txt");
        BanglaUnicode mapps = new BanglaUnicode();
        //box b = new box();


        public static string output = null;
        public static string prev_output = null;

        public static string[] central = { "1", "2", "3", "4", "5" };



        public void press_space(string temp)
        {
            if (!string.IsNullOrEmpty(temp)) output = temp;
            pressed = 1;
            if (mapps.getDirect(output) != null)
            {
                SendKeys.Send(mapps.getDirect(output));
                prev_output = output;
                output = null;
            }
            else if (!string.IsNullOrEmpty(output))
            {
                SendKeys.Send(parser.toBangla(output));
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

            if (temp == 48) output += "০";
            else if (temp == 49) output += "১";
            else if (temp == 50) output += "২";
            else if (temp == 51) output += "৩";
            else if (temp == 52) output += "৪";
            else if (temp == 53) output += "৫";
            else if (temp == 54) output += "৬";
            else if (temp == 55) output += "৭";
            else if (temp == 56) output += "৮";
            else if (temp == 57) output += "৯";


        }

        public void compose(int temp)
        {
            temp += 32 * capital;
            char letter = Convert.ToChar(temp);
            output = output + letter.ToString();
            parser.toBangla(output);
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

