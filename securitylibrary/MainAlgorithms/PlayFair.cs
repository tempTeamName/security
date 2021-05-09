using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        private const int SIZE = 5;
        string[,] matrix = new string[SIZE, SIZE];
        private void ConstructMatrix(string key)
        {
            HashSet<char> found = new HashSet<char>();
            int indx = 0;
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < SIZE; i++){
                for(int j = 0; j < SIZE; ){
                    if(indx == key.Length)
                    {
                        indx = 0;
                        key = alpha;
                    }
                    if (!found.Contains(key[indx])){
                        if(key[indx] == 'I' || key[indx] == 'J' )
                        {
                            matrix[i, j] = "I";
                            found.Add('I');
                            found.Add('J');
                        }
                        else
                        {
                            matrix[i, j] = key[indx].ToString();
                            found.Add(key[indx]);
                        
                        }
                        j++;
                    }
                    indx++;
                }
            }
        }
       

        private Tuple<int,int> getRowCol(string ch)
        {
            if (ch == "I" || ch == "J")
                ch = "I";
            int r, c;
            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    if(matrix[i,j] == ch)
                    {
                        r = i;
                        c = j;
                        return Tuple.Create(r, c);
                    }

                }
            }
            return null;
        }


       
        
        public string Decrypt(string cipherText, string key)
        {
            List<string> divides = new List<string>();
            ConstructMatrix(key.ToUpper());
            divides = PlayFairHelpers.Decrypt.Divide(cipherText.ToUpper());
            string ans = "";
            for(int i = 0; i < divides.Count; i++)
            {
                    string str = divides[i];
                    var firstCoor = getRowCol(str[0].ToString());
                    var secCoor = getRowCol(str[1].ToString());
                    string r1, r2;
                    //same row
                    if (firstCoor.Item1 == secCoor.Item1)
                    {
                    //DE
                         r1 = PlayFairHelpers.Decrypt.sameRow(matrix , firstCoor.Item1, firstCoor.Item2);
                         r2 = PlayFairHelpers.Decrypt.sameRow(matrix , secCoor.Item1, secCoor.Item2);
                    }
                    //same col
                    else if (firstCoor.Item2 == secCoor.Item2)
                    {
                         r1 = PlayFairHelpers.Decrypt.sameCol(matrix, firstCoor.Item1, firstCoor.Item2);
                         r2 = PlayFairHelpers.Decrypt.sameCol(matrix, secCoor.Item1, secCoor.Item2);
                    }
                    //diagonal
                    else
                    {
                        int minCol = Math.Min(firstCoor.Item2, secCoor.Item2);
                        int maxCol = Math.Max(firstCoor.Item2, secCoor.Item2);
                         r1 = PlayFairHelpers.Decrypt.diagonal(matrix ,  minCol, maxCol, firstCoor.Item2, firstCoor.Item1);
                         r2 = PlayFairHelpers.Decrypt.diagonal(matrix, minCol, maxCol, secCoor.Item2, secCoor.Item1);
                       
                    }
                ans += r1 + "" + r2;
            }
            string ciperTextAfterRemovingX = PlayFairHelpers.Decrypt.removeX(ans.ToUpper());
            string x = "theplayfaircipherusesafivebyfivetablecontainingakeywordorphrasememorizationofthekeywordandfoursimpleruleswasallthatwasrequiredtocreatethefivebyfivetableandusetheciphexlrckhtbrvmbrkhqcrxlrckhtbavheleeatgteenetnwembpqewovtdfheufiknylinthespacesinthetablewiththelettersofthekeyworddroppinganyduplicatelettersthenfilltheremainingspaceswiththerestofthelettersofthealphabetinorderusuallyiandhzittfcsoncapsegteeniohwqdpueityitintfexceruwsoftfdnpelbeoslldhtyvtorightorinsomeotherpatternsuchasaspiralbeginningintheupperlefthandcornerandendinginthecenterthekeywordtogetherwiththeconventionsforfillinginthefivebyfivetableconstitutethecipherkeyxlrckhtbrvmbrkhqcroencryptamessageonewouldbreakthemessageintodigramsgroupsoxlrckhtbemblyvterssuchthatforexamplexlrckhtbrenzloworlxlrckhtbrbecoqrvmbrkhqcrhelloworlxlrckhtbrvmbrkhqcrndmapthemoutonthekeytablxlrckhtbegkmdederxmbrkhqcrppendanuncommonmonogramtocompletethefinaldigraxlrckhtbbmhzetwolettersofthedigramareconsideredastheoppositecornersofarectangleinthekeytablexlrckhtbrctetedrdlwletavosinholohtferooksnrsofthisrectanglxlrckhtbbmhenopdzytiehslzlwrnlgisuurrulexlrckhtbbglwcdplmbrkhqcrtoeachpairoflettersintheplaintextmslxmbrkhqcrfbothlettersarethesamexlrckhtbrcwltvoqenblyvterislefxlrckhtbrvmbrkhqcrddaxlrckhtbrvmbrkhqcrafterthefirstlettexlrckhtbrdkorvsqxtheqewpphbwndboqnftvzmbrkhqcrxlrckhtbrvmbrkhqcrfthelettersappearonthesamerowofyourtablxlrckhtbbvreplacethemwiththeletterstotheirimmediaterightrespectivelyxlrckhtbbvrappingaroundtotheleftsideoftherowifaletterintheoriginalpairwasontherightsideoftheroxlrckhtbbmsmifthelettersappearonthesamecolumnofyourtablexlrckhtbreatorblgeqenmhtfekeyvtersimmediatelybelowrespectivelyxlrckhtbbvrappingaroundtothetopsideofthecolumnifaletterintheoriginalpairwasonthebottomsideofthecolumnmslxmbrkhqcrfthelettersarenotonthesameroworcolumnxlrckhtbreatorblgeqenmhtfekeyvtersonthesamerowrespectivelybutattheotherpairofcornersoftherectangledefinedbytheoriginalpaixlrckhtbbmhzeorderisimportanxlrckhtbbmfeikewmqblyvteroftheencryptedpairistheonethatliesonthesamerowasthefirstletteroftheplaintextpaixlrckhtbrvmbrkhqcrodecryptxlrckhtbeashiegtubearxmbrkhqcrppositexlrckhtbegtfdnowlxmbrkhqcrulesxlrckhtbagshfzmbrkhqcrstasxlrckhtbrvmbrkhqcrdroppinganyextraxlrckhtbrvmbrkhqcrxlrckhtbrvmbrkhqcrxlrckhtbeamhanbokoyuemezsndbittfdhgtanhswsohbahcmkitbslbshsmxlrckhtbbv";
           string cipherLower = ciperTextAfterRemovingX.ToLower();

            //for (int i = 0; i < cipherLower.Length; i++)
            //{
            //    if (x[i] == cipherLower[i])
            //        System.Diagnostics.Debug.WriteLine("yes " + cipherLower[i] + " " + x[i]);
            //    else
            //        System.Diagnostics.Debug.WriteLine(cipherLower[i] + " " + x[i]+" "+i);
            //}
                return cipherLower;
        }

        public string Encrypt(string plainText, string key)
        {
            List<string> divitions_of_plain = new List<string>();
            string crypt = "";
            string upperkey = key.ToUpper();
            ConstructMatrix(upperkey);
            string upperPlain = plainText.ToUpper();
            divitions_of_plain = PlayFairHelpers.Encrypt.divide(upperPlain);
            for(int i = 0; i < divitions_of_plain.Count; i++)
            {
                string str = divitions_of_plain[i];
                var firstCoor = getRowCol(str[0].ToString());
                var secCoor = getRowCol(str[1].ToString());
                //same row
                if (firstCoor.Item1 == secCoor.Item1)
                {
                    //DE
                    string r1 = PlayFairHelpers.Encrypt.sameRow(matrix, firstCoor.Item1, firstCoor.Item2);
                    string r2 = PlayFairHelpers.Encrypt.sameRow(matrix , secCoor.Item1, secCoor.Item2);
                    //if (r1 == "I/J" && r2 == "I/J")
                    //{
                    //    crypt += "IJ";
                    //}
                    //else if (r1 == "I/J")
                    //{
                    //    crypt += "I" + r2;
                    //}
                    //else if (r2 == "I/J")
                    //{
                    //    crypt += r1 + "I";
                    //}
                    //else
                        crypt += r1 + r2;
                }
                //same col
                else if(firstCoor.Item2 == secCoor.Item2)
                {
                    string r1 = PlayFairHelpers.Encrypt.sameCol(matrix , firstCoor.Item1, firstCoor.Item2);
                    string r2 = PlayFairHelpers.Encrypt.sameCol(matrix , secCoor.Item1, secCoor.Item2);
                    //if (r1 == "I/J" && r2 == "I/J")
                    //{
                    //    crypt += "IJ";
                    //}
                    //else if (r1 == "I/J")
                    //{
                    //    crypt += "I" + r2;
                    //}
                    //else if (r2 == "I/J")
                    //{
                    //    crypt += r1 + "I";
                    //}
                    //else
                        crypt += r1 + r2;
                }
                //diagonal
                else
                {
                    int minCol = Math.Min(firstCoor.Item2, secCoor.Item2);
                    int maxCol = Math.Max(firstCoor.Item2, secCoor.Item2);
                    string r1 = PlayFairHelpers.Encrypt.diagonal(matrix , minCol, maxCol, firstCoor.Item2 , firstCoor.Item1);
                    string r2 = PlayFairHelpers.Encrypt.diagonal(matrix ,minCol, maxCol, secCoor.Item2 , secCoor.Item1);
                    //if (r1 == "I/J" && r2 == "I/J")
                    //{
                    //    crypt += "IJ";
                    //}
                    //else if (r1 == "I/J")
                    //{
                    //    crypt += "I" + r2;
                    //}
                    //else if (r2 == "I/J")
                    //{
                    //    crypt += r1 + "I";
                    //}
                    //else
                        crypt += r1 + r2;
                }
            }

            return crypt;
        }

    }
}
