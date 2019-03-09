using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace english
{
    class Parser
    {

        /*****************************/


        private BanglaUnicode unicode = new BanglaUnicode();

     

        public String toBangla(String engWord)
        {

            StringBuilder st = new StringBuilder();
            char carry = (char)0;
            char secondCarry = (char)0;
            char thirdCarry = (char)0;
            bool tempNoCarry = false;
            bool jukta = false;
            char[] charArray;
            bool prevJukta = false;


            charArray = engWord.ToCharArray();

            // ======================= The great for loop starts =======================
            for (int i=0; i<charArray.Length; i++)
            {
                char now = charArray[i];

                // we won't parse anything other than english letters & digits
                if (!((now >= 'a' && now <= 'z') || (now >= 'A' && now <= 'Z') || (now >= '0' && now <= '9')))
                {
                    st.Append(now);
                    carry = (char)0;
                    //secondCarry = '\0';
                    //thirdCarry = '\0';
                    // if a bug shows up first thing to do is reset secondCarry, thirdCarry etc here
                    continue;
                }

                if (now == 'A' || now == 'B' || now == 'C' || now == 'E' || now == 'F' || now == 'P' || now == 'X')
                    now = char.ToLower(now);
                if (now == 'K' || now == 'L' || now == 'M' || now == 'V' || now == 'Y' || now == 'W' || now == 'Q')
                    now = char.ToLower(now);

                if (now == 'H' && carry != 'T') // khondiyo to -> TH
                    now = 'h';

                // 'w' should be 'O' when it's the first one or comes after a vowel
                if ((carry == (char)0 || isVowel(carry)) && now == 'w')
                    now = 'O';


                if (isVowel(now))
                {

                    // special for wri kar & wri
                    if (carry == 'r' && secondCarry == 'r' && now == 'i')
                    {

                        if (thirdCarry == (char)0)
                        {
                            if (st.Length != 0) st.Remove(st.Length- 2, 2);
                            st.Append("\u098B"); // wri						
                        }
                        else
                        {
                            if (st.Length != 0) st.Remove(st.Length- 3, 3);
                            st.Append("\u09C3"); // wri kar
                        }
                        carry = 'i';
                        continue;

                    }

                    String dual ;
                    

                    if (secondCarry != (char)0)
                        dual = unicode.getDualKar(now, carry);
                    else dual = unicode.getDual(now, carry); // dual as the first character of st

                    if (dual != null)
                    {
                        if (carry != 'o')
                        {
                            if(st.Length!=0) st.Remove(st.Length - 1, 1);
                        }
                           
                        if (isVowel(secondCarry))
                        { // a dual kar does not applied on vowel
                            st.Append(unicode.get(carry)).Append(unicode.get(now));
                        }
                        else
                            st.Append(dual);
                    }
                    else if (now == 'o' && carry != (char)0)
                    {
                        if (isVowel(carry))
                            st.Append(unicode.get('O'));
                        else
                        {
                            thirdCarry = secondCarry;
                            secondCarry = carry;
                            carry = now; // carry = 0 
                            continue;
                        }
                    }
                    else if (isVowel(carry) || carry == (char)0)
                    {
                        if (now == 'a' && carry != (char)0) // not first a
                            st.Append(unicode.get('y')).Append(unicode.getKar('a'));
                        else
                        {
                            st.Append(unicode.get(now));
                           // Console.WriteLine("now = " + now);
                        }
                    }
                    else
                    {
                        st.Append(unicode.getKar(now));
                    }

                }


                if (now == 'y' || now == 'Z' || now == 'r')
                    jukta = false;

                // when previous was a jukta and dual of the later two is not available
                // go to the else part of the next if block i.e now is independent
                tempNoCarry = jukta && unicode.getDual(now, carry) == null;

                if (isConsonant(now) && isConsonant(carry) && !tempNoCarry)
                {

                    // handle jo fola

                    if (now == 'y' || now == 'Z')
                    {
                        if (now == 'y' && carry == 'q' && secondCarry == 'q') ;
                        else
                            now = 'z';
                    }

                    //handle gg as in gyan,
                    //second carry not n, to skip onushar/unga

                    if (carry == 'g' && now == 'g' && secondCarry != 'N' && secondCarry != 'n')
                    {
                        if (st.Length != 0) st.Remove(st.Length- 1, 1);
                        st.Append("\u099C\u09CD\u099E");
                        prevJukta = jukta;
                        jukta = true;
                        secondCarry = 'g';
                        continue;
                    }

                    // handle kkh = kSh
                    if (secondCarry == 'k' && carry == 'k' && now == 'h')
                        carry = 'S';

                    // check if dual
                    String dual = unicode.getDual(now, carry);

                    if (dual != null)
                    {

                        // handle kaNgkShito
                        if (thirdCarry == 'g' && secondCarry == 'k' && carry == 'S' && now == 'h')
                            prevJukta = jukta = false;

                        bool firstOrAfterVowelOrJukta = isVowel(secondCarry) || secondCarry == (char)0 || prevJukta;

                        if (dualSitsUnder(thirdCarry, secondCarry, carry, now) && !firstOrAfterVowelOrJukta)
                        {
                            if (st.Length != 0) st.Remove(st.Length- 1, 1);
                            if (secondCarry == 'r' && thirdCarry == 'r')
                            {
                                if (st.Length != 0)  st.Remove(st.Length - 1, 1);
                            }
                            if (jukta) ;
                            else if (secondCarry != (char)0 && !isVowel(secondCarry))
                                st.Append("\u09CD");

                            st.Append(dual);
                            prevJukta = jukta;
                            // Jukta should be false in case we want to have three jukta letters
                            jukta = true;

                        }
                        else
                        {
                            if (jukta)
                            {
                                if (st.Length != 0) st.Remove(st.Length - 2, st.Length);
                            }
                            else
                            {
                                if (st.Length != 0) st.Remove(st.Length - 1, 1);
                            }

                            if (secondCarry == 'g' && carry == 'g' && now == 'h')
                            { // handled gg previously, now more pain
                                if (st.Length != 0) st.Remove(st.Length- 1, 1);
                                st.Append(unicode.get('g'));
                            }

                            st.Append(dual);
                            prevJukta = jukta;
                            jukta = false;
                        }


                    }
                    else
                    {

                        prevJukta = jukta;
                        jukta = false;

                        if (secondCarry != 'r' && carry == 'r' && now == 'z')
                        {
                            st.Append("\u200D\u09CD"); // handle rya as in ransom/rapid
                        }
                        else if (carry == 'r' && secondCarry != 'r') ;
                        // no ref when (c) rr (c)
                        else if (carry == 'r' && secondCarry == 'r' && isConsonant(thirdCarry)) ;
                        // ref when (v) rr (c)
                        else if (carry == 'r' && secondCarry == 'r' && (isVowel(thirdCarry) || thirdCarry == (char)0))
                        {
                            if (st.Length != 0) st.Remove(st.Length- 1, 1);
                            st.Append("\u09CD"); // jukta added for ref, but jukta not true
                        }

                        else if (notJukta(thirdCarry, secondCarry, carry, now)) ;
                        else
                        {
                            st.Append("\u09CD");
                            //prevJukta = jukta;
                            jukta = true;
                        }

                        st.Append(unicode.get(now));

                    }

                }
                else if (isConsonant(now))
                {

                    if (isVowel(carry) && now == 'Z')
                        st.Append("\u09CD");

                    if (carry == 0 && now == 'x')
                        st.Append(unicode.get('e'));

                    prevJukta = jukta;
                    jukta = false;

                    // to write b-fola 
                    if (now == 'w' && isConsonant(carry) && isConsonant(secondCarry))
                    {
                        st.Append("\u09CD");
                        prevJukta = jukta;
                        jukta = true;
                    }
                    // handle lakshmi/ lokhnou
                    if (thirdCarry == 'k' && secondCarry == 'S' && carry == 'h' && (now == 'N' || now == 'm'))
                    {
                        st.Append("\u09CD");
                        prevJukta = false;
                        jukta = true;
                    }
                    st.Append(unicode.get(now));

                }

                thirdCarry = secondCarry;
                secondCarry = carry;
                carry = now;
            } // end of for loop

            return st.ToString();
        }

        bool isVowel(char now)
        {
            if ("AEIOUaeiou".IndexOf(now) == -1)
                return false;
            return true;
        }

        bool isConsonant(char now)
        {
            return !isVowel(now) && char.IsLetter(now);
        }

        bool isCharInString(char now, String foo)
        {
            if (foo.IndexOf(now) == -1)
                return false;
            return true;
        }

        bool dualSitsUnder(char thirdCarry, char secondCarry, char carry, char now)
        {

            if (secondCarry == 'r' && thirdCarry == 'r')
                return true;

            if (secondCarry == 'r')
                return false;

            String djkt = unicode.getDjkt(carry, now);
            if (djkt != null)
                if (isCharInString(secondCarry, djkt))
                    return true;

            String djktt = unicode.getDjktt(carry, now);
            if (djktt != null)
                return djktt.Contains(char.ToString(thirdCarry) + char.ToString(secondCarry)); // ? true : false;

            // if we didn't cover it here, let's assume it sits under a consonant so we return true
            // but making it false has some advantages, e.g. the blocks that has only two lines
            // can be removed.. So when we're finished this function should return false
            return false;
        }

        bool notJukta(char thirdCarry, char secondCarry, char carry, char now)
        {

            if (now == 'r' || now == 'z' || now == 'w')
                return false;

            String foo = unicode.getDualJkt(secondCarry, carry);

            if (foo != null)
                return !isCharInString(now, foo); //? false : true;

            foo = unicode.getJkt(carry);
            if (foo != null)
                return !isCharInString(now, foo); // ? false : true;


            // if we didn't cover it here let's assume a consonant sits under it so we return false
            // but making it true has some advantages, e.g. the blocks that has only two lines
            // can be removed.. So when we're finished, this function should return true
            return true;
        }


        /*************************/
    }
}
