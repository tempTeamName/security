using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        private const int SIZE = 26;
        char[,] matrix = new char[SIZE,SIZE];
        char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public void constructMatrix()
        {
            for(int i = 0; i < SIZE; i++)
            {
                int ind = i;
                for(int j = 0; j < SIZE; j++)
                {
                    matrix[i,j] = alpha[ind];
                    ind += 1;
                    ind %= SIZE;
                }
            }

        }
        public string generateKeyStream(string plain , string key)
        {
            string keystream = key;

            int diff = Math.Abs(plain.Length - key.Length);

            for(int i = 0; i < diff ; i++)
            {
                keystream += plain[i];
            }
            return keystream;
        }
        public string Analyse(string plainText, string cipherText)
        {
            constructMatrix();
            string plain = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    int ind = char.ToUpper(plainText[i]) - 64;
                    if (matrix[ind - 1, j] == cipherText[i])
                    {
                        plain += alpha[j];
                        break;
                    }
                }
            }
            string key = "";
            for(int i = cipherText.Length - 1 ; i>= 0 ; i--)
            {
                if (plain[i] == cipherText[i])
                    continue;
               
                key += plain[i];
            }
            return key;
        }

        public string decrypt_helper(string cipherText, string key , int st)
        {
            string plain = "";
            for (int i = st; i < key.Length && i < cipherText.Length; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    int ind = char.ToUpper(key[i]) - 64;
                    if (matrix[ind - 1 , j] == cipherText[i])
                    {
                        plain += alpha[j];
                        break;
                    }
                }
            }
            return plain;
        }
        public string Decrypt(string cipherText, string key)
        {
            constructMatrix();
            // string keystream = generateKeyStream();
            string plain1 = decrypt_helper(cipherText.ToUpper(), key.ToUpper() , 0);
            string key2 = "";
            int d = cipherText.Length - plain1.Length;

            string plain = plain1;
            for (int i = 0; i < d; i++)
            {
               key2 += plain1[i];
            }
            plain1 = decrypt_helper(cipherText, key2 , key.Length);
            plain += plain1;

            return plain.ToLower();
        }

        public string Encrypt(string plainText, string key)
        {
            constructMatrix();
            string keystream = generateKeyStream(plainText.ToUpper() , key.ToUpper());
            string cipher = "";
            for(int i = 0; i < keystream.Length; i++)
            {
                int ind1 = char.ToUpper(plainText[i])  - 64;

                int ind2 = char.ToUpper(keystream[i]) - 64;
                cipher += matrix[ind1 - 1, ind2 - 1];
            }
            //system.diagonstics.debug.write()
            return cipher;
        }
    }
}
