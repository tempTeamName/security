using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            Dictionary<char, char> map = new Dictionary<char, char>();
            bool[] vis = new bool[26];
            for (int i = 0; i < plainText.Length; i++)
            {
                if (!map.ContainsKey(plainText[i]))
                {
                    map.Add(plainText[i], cipherText[i]);
                    vis[(char)(cipherText[i] - 'a')] = true;
                }
            }
            char[] key = new char[26];
            int nextCh = 0;
            for (int i = 0; i < 26; i++)
            {
                if (map.ContainsKey((char)(i + 'a')))
                {
                    key[i] = map[(char)(i + 'a')];
                }
                else
                {
                    while (vis[nextCh])
                    {
                        nextCh++;
                    }
                    key[i] = (char)(nextCh + 'a');
                    nextCh++;
                }

            }
            return new string(key);
        }

        public string Decrypt(string cipherText, string key)
        {
            key = key.ToLower();
            cipherText = cipherText.ToLower();
            char[] decryptedText = cipherText.ToCharArray();
            for (int i = 0; i < decryptedText.Length; i++)
            {
                int index = key.IndexOf(decryptedText[i]);
                decryptedText[i] = (char)(index + 'a');
            }
            return new string(decryptedText);
        }

        public string Encrypt(string plainText, string key)
        {
            key = key.ToLower();
            plainText = plainText.ToLower();
            char[] enctyptedText = plainText.ToCharArray();
            for (int i = 0; i < enctyptedText.Length; i++)
            {
                int index = enctyptedText[i] - 'a';
                enctyptedText[i] = key[index];
            }
            return new string(enctyptedText);
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            int[] frequency = new int[26];
            char[] charOrder = { 'e', 't', 'a', 'o', 'i', 'n', 's', 'r', 'h', 'l', 'd', 'c',
                'u', 'm', 'f', 'p', 'g', 'w', 'y', 'b', 'v', 'k', 'x', 'j', 'q', 'z' };
            List<item> list = new List<item>();
            cipher = cipher.ToLower();
            for (int i = 0; i < cipher.Length; i++)
            {
                frequency[(char)(cipher[i] - 'a')]++;
            }
            for (int i = 0; i < 26; i++)
            {
                char cur = (char)(i + 'a');
                list.Add(new item() { ch = cur, freq = frequency[cur - 'a'] });
            }
            list.Sort((x, y) => x.CompareTo(y));
            list.Reverse();

            Dictionary<char, char> map = new Dictionary<char, char>();
            bool[] vis = new bool[26];
            for (int i = 0; i < charOrder.Length; i++)
            {
                if (!map.ContainsKey(charOrder[i]))
                {
                    map.Add(charOrder[i], list[i].ch);
                    vis[(char)(list[i].ch - 'a')] = true;
                }
            }

            char[] key = new char[26];
            int nextCh = 0;
            for (int i = 0; i < 26; i++)
            {
                if (map.ContainsKey((char)(i + 'a')))
                {
                    key[i] = map[(char)(i + 'a')];
                }
                else
                {
                    while (vis[nextCh])
                    {
                        nextCh++;
                    }
                    key[i] = (char)(nextCh + 'a');
                    nextCh++;
                }

            }
            return this.Decrypt(cipher, new string(key));
        }
        class item
        {
            public char ch;
            public int freq;
            public int CompareTo(item other)
            {
                if (other == null)
                    return 1;

                if (freq == other.freq)
                {
                    if (ch < other.ch)
                        return -1;
                    else
                        return 1;
                }
                else if (freq < other.freq)
                    return -1;
                else
                    return 1;
            }
        }
    }
}