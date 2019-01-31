using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkusheDesktop
{
    public class Suggestion
    {



        ITrie trie = new Trie();
        //Refine refine = new refine();

        public Suggestion()
        {
            train();
        }


        public void train()
        {
            StreamReader reader = File.OpenText("vocabulary.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(' ');

                foreach (string ss in items)
                {
                    trie.AddWord(ss);
                }
            }
        }

        public void Getlist(string source)
        {


            IEnumerable<string> temp = trie.GetWords(source);
            int i = 0;
            foreach (string ss in temp)
            {
                if (i >= 5) break; 
                Refine.central[i++] = ss;
            }

        }



    }
}
