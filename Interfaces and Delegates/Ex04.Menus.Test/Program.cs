using System;

namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            /*Console.WriteLine(Sqrt(25).ToString());
            Console.WriteLine(Sqrt(2147395600).ToString());*/
            Console.WriteLine(req(40).ToString());
            Console.ReadLine();
            /*InterfaceMenu theInterfaceMenu = new InterfaceMenu();
            theInterfaceMenu.InitMenu();
            
            DelegatesMenu theDelegatesMenu = new DelegatesMenu();
            theDelegatesMenu.InitMenu();*/
        }

        public static int Sqrt(int x)
        {
            int count = 0;
            long fuck1, fuck2;

            if (x == 1)

            {
                return 1;
            }

            while (true)
            {
                fuck1 = (long)count * (long)count;
                fuck2 = (long)(count + 1) * (long)(count + 1);
                if(fuck1 <= (long)x && fuck2 > (long)x)
                {
                    break;
                }
                count++;
            }

            return count;
        }
        
        public static int req(int n)
        {
            if(n == 0)
            {
                return 1;
            }

            if(n == -1)
            {
                return 0;
            }

            return req(n - 2) + req(n - 1);
        }
    }
}
