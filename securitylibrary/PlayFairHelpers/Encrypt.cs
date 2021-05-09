using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.PlayFairHelpers
{
    public static class Encrypt
    {
        private const int SIZE = 5;
        public static List<string>  divide(string plainText)
        {
            List<string> d = new List<string>();
            for (int i = 0; i < plainText.Length;)
            {
                string s = "";
                if (i == (plainText.Length) - 1)
                {
                    s += plainText[i] + "X";
                    d.Add(s);
                    break;
                }
                if (plainText[i] == plainText[i + 1])
                {
                    s += plainText[i];
                    s += "X";
                    d.Add(s);
                    i++;
                }
                else
                {
                    s += plainText[i];
                    s += plainText[i + 1];
                    d.Add(s);
                    i += 2;
                }
            }
            return d;
        }
        public static string diagonal(string [,] matrix,int min, int max, int c, int r)
        {
            //if corner
            if (c == max)
                return matrix[r, min];
            else
                return matrix[r, max];
        }
        public static string sameRow(string[,]matrix , int r, int c)
        {
            if (c == SIZE - 1)
                return matrix[r, 0];
            else
                return matrix[r, c + 1];
        }
        public static string sameCol(string[,]matrix , int r, int c)
        {
            if (r == SIZE - 1)
                return matrix[0, c];
            else
                return matrix[r + 1, c];

        }
    }
}
