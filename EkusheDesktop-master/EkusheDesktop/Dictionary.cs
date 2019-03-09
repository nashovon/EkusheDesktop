using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EkusheDesktop
{
    [Serializable]
    public class Dictionary
    {

        public void Initialize()
        {
            Trie trie = new Trie();
            StreamReader reader = File.OpenText("test_corpus_eng.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(' ');

                foreach (string ss in items)
                {
                    trie.AddWord(ss);
                }

                //Console.WriteLine(trie.Count());

                IFormatter formatter = new BinaryFormatter();

                //Stream stream = new FileStream(@"data.txt", FileMode.Create, FileAccess.Write);
                //using (FileStream st = File.Create("temp.txt"))
                //{
                //    formatter.Serialize(st, trie);
                //    st.Close();
                //}

                using (FileStream fs = File.OpenRead("temp.txt"))
                {


                    Trie trienew = (Trie)formatter.Deserialize(fs);

                    IEnumerable<string> PrefixWords = trienew.GetWords("b");

                   

                   
                    foreach (string ss in PrefixWords)
                    {
                     
                        
                        Console.WriteLine(ss);
                     
                    }
                }




                //Stream stream = new FileStream(@"temp.txt", FileMode.Open, FileAccess.Read);
                //ITrie trienew = (ITrie)formatter.Deserialize(stream);

                //Console.WriteLine(trienew.Count());



            }


        }
    }
}
   


