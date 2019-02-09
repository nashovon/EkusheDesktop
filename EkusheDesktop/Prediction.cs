using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace EkusheDesktop
{

    public class Prediction
    {




        //Next Word Prediction
        public static List<string> al_comments = new List<string>();
        public static IList<Dictionary<string, int>> list_of_words_mp = new List<Dictionary<string, int>>();
        public static IList<Dictionary<string, int>> list_of_words_mp_trigram = new List<Dictionary<string, int>>();
        public static Dictionary<string, int> mp1_word_id = new Dictionary<string, int>();
        public static Dictionary<string, int> mp1_word_id_trigram = new Dictionary<string, int>();
        private IList<int> delCharCorIcLet = new List<int>();

        public Prediction()
        {
            read_comments();
        }




        //JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:



        //JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
        public void read_comments()
        {
            //try
            //{
            StringBuilder sBuffer = new StringBuilder();
            StreamReader br_ = File.OpenText("test_corpus.txt");

            //int i = 0;
            string strLine;
            while ((strLine = br_.ReadLine()) != null)
            {
                strLine = strLine + "\n";
                al_comments.Add(strLine);
                string[] tokens = strLine.Split(' ');
                //

                // foreach (string ss in tokens) Console.WriteLine(++i);
                //
                int len_tok = tokens.Length;
                //Debug.WriteLine(len_tok);
                for (int tk_idx = 0; tk_idx < len_tok; tk_idx++)
                {
                    string ss = tokens[tk_idx];
                    if (tk_idx + 1 < len_tok)
                    {
                        int temp;
                        //mp1_word_id.TryGetValue(ss, out temp);
                        if (mp1_word_id.TryGetValue(ss, out temp) == false)
                        {
                            mp1_word_id[ss] = mp1_word_id.Count;
                        }
                        int id;
                        mp1_word_id.TryGetValue(ss, out id);

                        if (list_of_words_mp.Count <= id)
                        {
                            list_of_words_mp.Add(new Dictionary<string, int>());
                        }


                        Dictionary<string, int> tt_cur_mp = list_of_words_mp[id];

                        string ss_next = tokens[tk_idx + 1];



                        if (mp1_word_id.TryGetValue(ss_next, out temp) == false)
                        {
                            tt_cur_mp[ss_next] = 1;
                        }
                        else
                        {
                            int cur_freq;
                            //tt_cur_mp.TryGetValue(ss_next, out cur_freq);

                            if (tt_cur_mp.TryGetValue(ss_next, out cur_freq) == false) tt_cur_mp[ss_next] = cur_freq + 1;
                        }
                    }
                    //FOR TRIGRAM
                    if (tk_idx + 2 < len_tok)
                    {

                        string ss_next1 = tokens[tk_idx + 1];
                        string ss_2gram = ss + " " + ss_next1;
                        int temp = -1;
                        //mp1_word_id_trigram.TryGetValue(ss_2gram, out temp);
                        if (mp1_word_id_trigram.TryGetValue(ss_2gram, out temp) == false)
                        {
                            mp1_word_id_trigram[ss_2gram] = mp1_word_id_trigram.Count;
                        }
                        int id;

                        mp1_word_id_trigram.TryGetValue(ss_2gram, out id);


                        if (list_of_words_mp_trigram.Count <= id)
                        {
                            list_of_words_mp_trigram.Add(new Dictionary<string, int>());
                        }
                        Dictionary<string, int> tt_cur_mp_trigram = list_of_words_mp_trigram[id];
                        string ss_next2 = tokens[tk_idx + 2];
                        //temp = -1;
                        //tt_cur_mp_trigram.TryGetValue(ss_next2, out temp);
                        if (tt_cur_mp_trigram.TryGetValue(ss_next2, out temp) == false)
                        {
                            tt_cur_mp_trigram[ss_next2] = 1;
                        }
                        else
                        {

                            int cur_freq = -1;
                            //tt_cur_mp_trigram.TryGetValue(ss_next2, out cur_freq);

                            if (tt_cur_mp_trigram.TryGetValue(ss_next2, out cur_freq) == true) tt_cur_mp_trigram[ss_next2] = cur_freq + 1;
                        }
                    }
                    // br_.Close();
                }
            }
        }


        public void Getlist(string cur_word)
        {


            bool trigram_flag = false;
            int temp = -1;
            mp1_word_id.TryGetValue(cur_word, out temp);
            if (temp == -1)
            {
                temp = -1;
                mp1_word_id_trigram.TryGetValue(cur_word, out temp);
                if (temp != -1)
                {
                    trigram_flag = true;
                }
                else
                {
                    //sugg_flag = true;
                    //return;
                }
            }
            IList<Dictionary<string, int>> list_of_words_mp_in_function;
            int word_idx = 0;
            if (trigram_flag)
            {
                mp1_word_id.TryGetValue(cur_word, out word_idx);
                list_of_words_mp_in_function = list_of_words_mp_trigram;
            }
            else
            {
                mp1_word_id.TryGetValue(cur_word, out word_idx);
                list_of_words_mp_in_function = list_of_words_mp;
            }
            Dictionary<string, int> tt_cur_mp = list_of_words_mp_in_function[word_idx];

            string[] keys = tt_cur_mp.Keys.OrderBy(v => v).ToArray();

            //foreach (string ss in keys)
            //{
            //    Debug.WriteLine(ss);
            //}
            //int mx = 0;

            //ISet<Entry<string, int>> set = tt_cur_mp.entrySet();
            //IList<Entry<string, int>> list = new List<Entry<string, int>>(set);
            //Collections.sort(list, new ComparatorAnonymousInnerClass(this));

            //int cnt_next_word = 0;

            //foreach (Entry<string, int> entry in list)
            //{
            //    possibleNextWOrdsInBng.add(entry.Key);
            //}

            //isNexWord = true;
            //CandidatesViewShown = true;
            //mCandidateView.setSuggestions(possibleNextWOrdsInBng, true, true);

            //string[] final = null;
            //int j = 0;

            int i = 0;
            foreach (string ss in keys)
            {
                if (i >= 5) break;
                Refine.central[i++] = ss;
            }


        }





    }
}
