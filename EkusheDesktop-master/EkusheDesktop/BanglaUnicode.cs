using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkusheDesktop
{
    public class BanglaUnicode
    {



        private IDictionary<string, string> map = new Dictionary<string, string>();
        private IDictionary<string, string> kars = new Dictionary<string, string>();
        private IDictionary<string, string> jkt = new Dictionary<string, string>();
        private IDictionary<string, string> djkt = new Dictionary<string, string>();
        private IDictionary<string, string> djktt = new Dictionary<string, string>();
        private IDictionary<string, string> num = new Dictionary<string, string>();
        private IDictionary<string, string> direct = new Dictionary<string, string>();


        public BanglaUnicode(){

            // Same : a, k, g, c, f, e, b, v, l, m, p
            map.Add("o", "\u0985"); // shore o
            map.Add("O", "\u0993"); // rossho o
            map.Add("a", "\u0986"); // aa
            map.Add("A", "\u0986"); // aa
            map.Add("S", "\u09B6"); // talobbo sho
            map.Add("sh", "\u09B6"); // talobbo sho
            map.Add("s", "\u09B8");  // donto sho
            map.Add("Sh", "\u09B7"); // murdonno sho
            map.Add("h", "\u09B9"); // ho
            map.Add("H", "\u09B9"); // ho
            map.Add("r", "\u09B0"); // ro
            map.Add("R", "\u09DC"); // dhoye shunne ro
            map.Add("Rh", "\u09DD"); // dhoye shunne ro
            map.Add("k", "\u0995"); // ko
            map.Add("K", "\u0995"); // ko
            map.Add("q", "\u0995");
            map.Add("qq", "\u0981"); // chondro bindu
            map.Add("kh", "\u0996"); // kho
            map.Add("g", "\u0997"); // go
            map.Add("G", "\u0997"); //go
            map.Add("gh", "\u0998"); // gho
            map.Add("Ng", "\u0999"); // unga		
            map.Add("c", "\u099A"); // cho
            map.Add("C", "\u099A"); // cho
            map.Add("ch", "\u099B"); // ccho
            map.Add("j", "\u099C"); // jo
            map.Add("jh", "\u099D"); // jho
            map.Add("J", "\u099C"); // jho
            map.Add("NG", "\u099E"); // niyo
            map.Add("T", "\u099F"); // To
            map.Add("Th", "\u09A0"); // Tho
            map.Add("TH", "\u09CE"); // khondiyo to
            map.Add("f", "\u09AB"); // fo
            map.Add("F", "\u09AB"); // fo
            map.Add("ph", "\u09AB"); // fo
            map.Add("i", "\u0987"); // rossho i
            map.Add("I", "\u0988"); // dhirgo i
            map.Add("e", "\u098F"); // e
            map.Add("E", "\u098F"); // e
            map.Add("u", "\u0989"); // rossho u
            map.Add("U", "\u098A"); // dhirgo u
            map.Add("b", "\u09AC"); // bo
            map.Add("B", "\u09AC"); // bo
            map.Add("w", "\u09AC"); // bo
            map.Add("bh", "\u09AD"); // bho
            map.Add("V", "\u09AD"); // bho
            map.Add("v", "\u09AD"); // bho
            map.Add("t", "\u09A4"); // to
            map.Add("th", "\u09A5"); // tho
            map.Add("d", "\u09A6"); // do
            map.Add("dh", "\u09A7"); // dho
            map.Add("D", "\u09A1"); // do
            map.Add("Dh", "\u09A2"); // dho
            map.Add("n", "\u09A8"); // donto no
            map.Add("N", "\u09A3"); // murdo no
            map.Add("z", "\u09AF"); // zho
            map.Add("Z", "\u09AF"); // zho fola
            map.Add("y", "\u09DF"); // ontosto yo
            map.Add("l", "\u09B2"); // lo
            map.Add("L", "\u09B2"); // lo
            map.Add("m", "\u09AE"); // mo
            map.Add("M", "\u09AE"); // mo
            map.Add("P", "\u09AA"); // po
            map.Add("p", "\u09AA"); // po
            map.Add("ng", "\u0982"); // onushar
            map.Add("cb", "\u0981"); // chondro point
            map.Add("x", "\u0995\u09CD\u09B8");
            map.Add("OU", "\u0994");
            map.Add("OI", "\u0990");
            map.Add("hs", "\u09CD");
            map.Add("nj", "\u099E\u09CD\u099C"); //
            map.Add("nc", "\u099E\u09CD\u099A"); //


           


            kars.Add("o", ""); // o kar
            kars.Add("a", "\u09BE"); // aa kar
            kars.Add("A", "\u09BE"); // aa kar
            kars.Add("e", "\u09C7"); // e kar
            kars.Add("E", "\u09C7"); // e kar
            kars.Add("O", "\u09CB"); // O kar
            kars.Add("OI", "\u09C8"); // OI kar
            kars.Add("OU", "\u09CC");
            kars.Add("i", "\u09BF"); // rossho i kar
            kars.Add("I", "\u09C0"); //dhirgo i karu
            kars.Add("u", "\u09C1"); // rossho u kar
            kars.Add("U", "\u09C2"); // dhirgo u kar
            kars.Add("oo", "\u09C1"); // rossho u kar

            // each of 2nd sits under 1st
            jkt.Add("k", "kTtnNslw");
            jkt.Add("g", "gnNmlw");
            jkt.Add("ch", "w");
            jkt.Add("Ng", "gkm");
            jkt.Add("NG", "cj");
            //jkt.Add("g", "gnNmlw");
            jkt.Add("G", "gnNmlw");
            jkt.Add("th", "w");
            jkt.Add("gh", "Nn");
            jkt.Add("c", "c");
            jkt.Add("j", "jw");
            jkt.Add("T", "T");
            jkt.Add("D", "D");
            jkt.Add("R", "g");
            jkt.Add("N", "DNmw");
            jkt.Add("t", "tnmwN");
            jkt.Add("d", "wdm");
            jkt.Add("dh", "wn");
            jkt.Add("n", "ndwmtsDT");
            jkt.Add("p", "plTtns");
            jkt.Add("f", "l");
            jkt.Add("ph", "l");
            jkt.Add("b", "jdbwl");
            jkt.Add("v", "l");
            jkt.Add("bh", "l");
            jkt.Add("m", "npfwvmlb");
            jkt.Add("l", "lwmpkgTDf");
            jkt.Add("Sh", "kTNpmf");
            jkt.Add("S", "clwnm");
            jkt.Add("sh", "clwnm");
            jkt.Add("s", "kTtnpfmlw");
            jkt.Add("h", "Nnmlw");
            jkt.Add("cb", "");
            jkt.Add("jh", "");
            jkt.Add("TH", "");
            jkt.Add("qq", "");
            jkt.Add("ng", "");
            jkt.Add("kh", "");
            jkt.Add("gg", "");
            //jkt.Add("dh", "");
            jkt.Add("Th", "");

            // first sits under each of 2nd
            djkt.Add("kh", "Ngs");
            djkt.Add("ch", "c");
            djkt.Add("Dh", "N");
            djkt.Add("ph", "mls");
            djkt.Add("dh", "gdnbl");
            djkt.Add("bh", "dm");
            djkt.Add("Sh", "k");
            djkt.Add("th", "tns");
            djkt.Add("Th", "Nn");
            djkt.Add("jh", "j");
            djkt.Add("NG", "cj");

            // first sits under 2nd(dual)
            djktt.Add("ch", "NG");
            djktt.Add("gh", "Ng");
            djktt.Add("Th", "Sh");
            djktt.Add("jh", "NG");
            djktt.Add("sh", "ch");

            //numbers
            num.Add("1", "১");
            num.Add("2", "২");
            num.Add("3", "৩");
            num.Add("4", "৪");
            num.Add("5", "৫");
            num.Add("6", "৬");
            num.Add("7", "৭");
            num.Add("8", "৮");
            num.Add("9", "৯");
            num.Add("0", "০");

            //direct
            direct.Add("class", "ক্লাস");
            direct.Add("mobile", "মোবাইল");
            direct.Add("bank", "ব্যাঙ্ক");
            direct.Add("bag", "ব্যাগ");
            direct.Add("school", "স্কুল");


        }


        public String getDual(char x, char carry)
        {
            string result = (carry.ToString() + x.ToString()).Trim('\0');
            string value = null;
            //Console.WriteLine(map[carry.ToString() + x.ToString()]);
            if (map.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String get(char x)
        {
            string result = (x.ToString()).Trim('\0');
            string value = null;
            //Console.WriteLine(map);
            if (map.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String getKar(char x)
        {
            string result = (x.ToString()).Trim('\0');
            string value = null;
            //Console.WriteLine("getKar");
            if (kars.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String getDualKar(char x, char carry)
        {
            string result = (carry.ToString() + x.ToString()).Trim('\0');
            string value = null;
            //Console.WriteLine(result);
            if (kars.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String getJkt(char carry)
        {
            string result = (carry.ToString().Trim('\0'));
            string value = null;
            //Console.WriteLine(result);
            if (jkt.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String getDualJkt(char secondCarry, char carry)
        {
            string result = (secondCarry.ToString() + carry.ToString()).Trim('\0');
            string value = null;
            //Console.WriteLine(result);
            if (jkt.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String getDjkt(char secondCarry, char carry)
        {
            string result = (secondCarry.ToString() + carry.ToString()).Trim('\0');
            string value = null;
            //Console.WriteLine(result);
            if (djkt.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String getDjktt(char secondCarry, char carry)
        {
            string result = (secondCarry.ToString() + carry.ToString()).Trim('\0');
            string value = null;
            //Console.WriteLine(result);
            if (djkt.TryGetValue(result, out value)) return value;
            else return null;
        }
        public String getDirect(string result)
        {
            string value = null;
            if (result != null)
            {
                direct.TryGetValue(result, out value);
                
            }
            
            
            return value;
        }
      


    }
}
