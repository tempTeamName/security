using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.PlayFairHelpers
{
    public static class Decrypt
    {
        private static int SIZE = 5;
        public static string diagonal(string[,]matrix , int min, int max, int c, int r)
        {
            //if corner
            if (c == min)
                return matrix[r, max];
            else
                return matrix[r, min];
        }
        public static string sameRow(string[,]matrix , int r, int c)
        {
            if (c == 0)
                return matrix[r, SIZE - 1];
            else
                return matrix[r, c - 1];
        }
        public static string sameCol(string[,]matrix , int r, int c)
        {
            if (r == 0)
                return matrix[SIZE - 1, c];
            else
                return matrix[r - 1, c];

        }
        public static string removeX(string cipherText)
        {
            string ans = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (cipherText.Length - 1 == i && cipherText[i] == 'X')
                    break;
                if (i > 0 && i < cipherText.Length - 1)
                {
                    if (cipherText[i] == 'X' && cipherText[i + 1] == cipherText[i - 1] && i%2 != 0 )
                    {
                        System.Diagnostics.Debug.WriteLine(i + " " + cipherText[i + 1] + " " + cipherText[i - 1] + " " + cipherText[i]);
                        continue;
                    }
                }
                ans += cipherText[i];

            }
            return ans;
        }

        public static List<string> Divide(string cipherText)
        {
            List<string> d = new List<string>();
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                string s = cipherText[i] + "" + cipherText[i + 1];
                d.Add(s);
            }
            return d;
        }
    }
}
