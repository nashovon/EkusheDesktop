using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace english
{
    public class Suggestion
    {



        ITrie trie = new Trie();
        //Refine refine = new refine();

        public Suggestion(string filename)
        {
            train(filename);
        }


        public void train(string filename)
        {
            StreamReader reader = File.OpenText(filename);
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
                BNRefine.central[i++] = ss;
            }

        }



    }
}
