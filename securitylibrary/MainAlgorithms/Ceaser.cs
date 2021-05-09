using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            char[] encryptedString = plainText.ToLower().ToCharArray();
            for (int i = 0; i < encryptedString.Length; i++)
            {
                int charValue = encryptedString[i] - 'a';
                int encryptedValue = (charValue + key) % 26;
                encryptedString[i] = (char)('a' + encryptedValue);
            }
            return new string(encryptedString);
        }

        public string Decrypt(string cipherText, int key)
        {
            char[] decryptedString = cipherText.ToLower().ToCharArray();
            for (int i = 0; i < decryptedString.Length; i++)
            {
                int charValue = decryptedString[i] - 'a';
                int encryptedValue = (charValue - key + 26) % 26;
                decryptedString[i] = (char)('a' + encryptedValue);
            }
            return new string(decryptedString);
        }

        public int Analyse(string plainText, string cipherText)
        {
            return (cipherText.ToLower()[0] - plainText.ToLower()[0] + 26) % 26;
        }
    }
}