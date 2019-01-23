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

        public IEnumerable<string> Getlist(string source)
        {
            return trie.GetWords(source);
        }



    }
}
