using System;
using System.Collections.Generic;

namespace codeinter
{
    class yuvaldorhahomo
    {
        public static void Main(string[] args)
        {
/*            Console.WriteLine(secrueFun("i!Hsksj1d"));
            Console.WriteLine(secrueFun("PASSWORDASD"));
*/
            //Console.WriteLine(SaloshGey(2, "1A 2F 1C"));
            Console.WriteLine(Ex3(1, ""));

            Console.ReadLine();
        }

        public static int Ex2 ( int[][] A)
        {
            int[] countAppearens = new int[A.GetLength(0) * A.GetLength(1)];
            //          ID, firstRowOfApearens
            Dictionary<int, int> uniqueRows = new Dictionary<int, int>();
            int sum = 0;

            for (int k = 1; k <= countAppearens.Length; k++)
            {
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    for (int j = 0; j < A.GetLength(1); j++)
                    {
                        if (k == A[i][j])
                        {
                            if (!uniqueRows.ContainsKey(k))
                            {
                                uniqueRows.Add(k, i);
                            }
                            else
                            {
                                int baseRow = uniqueRows[k];
                                if (baseRow != i)
                                {
                                    countAppearens[k] = 1;
                                    break;
                                }
                            }
                        }
                    }
                    if(countAppearens[k] == 1)
                    {
                        break;
                    }
                }
            }

            foreach(int num in countAppearens)
            {
                sum += num;
            }

            return sum;
        }


        public static bool Ex1(string S)
        {
            bool atLeastOneDigit = false, atLeastOneUpper = false, atLeastOneLower = false, atLeastOneSpecial = false;

            //HashSet<char> specialSymbols = new HashSet<char>() { '!', '@', '#', '$' ,'%', '^','&','*','(',')','_' };

            HashSet<char> specialSymbols = new HashSet<char>();

            specialSymbols.Add('!');
            specialSymbols.Add('@');
            specialSymbols.Add('#');
            specialSymbols.Add('$');
            specialSymbols.Add('%');
            specialSymbols.Add('^');
            specialSymbols.Add('&');
            specialSymbols.Add('*');
            specialSymbols.Add('(');
            specialSymbols.Add(')');
            specialSymbols.Add('_');

            if(S.Length < 6) //password length is shorter then 6
            {
                return false;
            }


            foreach (char ch in S)
            {
                if (ch == ' ')
                {
                    return false;
                }
                if(char.IsDigit(ch))
                {
                    atLeastOneDigit = true;
                    continue;
                }
                if(char.IsUpper(ch))
                {
                    atLeastOneUpper = true;
                    continue;
                }
                if (char.IsLower(ch))
                {
                    atLeastOneLower = true;
                    continue;
                }
                if(specialSymbols.Contains(ch))
                {
                    atLeastOneSpecial = true;
                    continue;
                }
            }

            return atLeastOneDigit && atLeastOneLower && atLeastOneUpper && atLeastOneSpecial;

        }

        public static int Ex3(int N, string S)
        {
            bool[,] airPlane = new bool[N,10];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    airPlane[i, j] = true;
                }
            }

            bool[] currRowFromPlane = new bool[10];
            Dictionary<char, int> seatTranslator= new Dictionary<char, int>();
            int numOf4Families = 0;

            seatTranslator.Add('A', 0);
            seatTranslator.Add('B', 1);
            seatTranslator.Add('C', 2);
            seatTranslator.Add('D', 3);
            seatTranslator.Add('E', 4);
            seatTranslator.Add('F', 5);
            seatTranslator.Add('G', 6);
            seatTranslator.Add('H', 7);
            seatTranslator.Add('J', 8);
            seatTranslator.Add('K', 9);

            string seat = "";

            foreach(char ch in S)
            {

                if(ch != ' ')
                {
                    seat += ch;
                }
                else
                {
                    airPlane[int.Parse(seat.Substring(0, seat.Length - 1)) - 1, seatTranslator[seat[seat.Length - 1]]] = false;
                    seat = "";
                }
            }

            if(S.Length != 0 )
            {
                airPlane[int.Parse(seat.Substring(0, seat.Length - 1)) - 1, seatTranslator[seat[seat.Length - 1]]] = false;
            }

            for (int i = 0; i < N; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    currRowFromPlane[j] = airPlane[i, j];
                }
                numOf4Families += numOfFamiliesInARow(currRowFromPlane);
            }

            return numOf4Families;

        }

        private static int numOfFamiliesInARow(bool[] currRow)
        {
            int count = 0;
            if (currRow[1] && currRow[2] && currRow[3] && currRow[4])
                count++;

            if (currRow[5] && currRow[6] && currRow[7] && currRow[8])
                count++;

            if (count == 0 && currRow[3] && currRow[4] && currRow[5] && currRow[6])
                count++;

            return count;
        }
    }
}

