using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    public class CountSpaces : IFunctionality
    {
        public void InvokeFunction()
        {
            Console.Write(string.Format("Please enter your sentence:{0}", Environment.NewLine));
            string userString = Console.ReadLine();
            int countSpaces = 0;
            string grammerValidate;

            foreach (char oneChar in userString)
            {
                if (oneChar == ' ')
                {
                    countSpaces++;
                }
            }

            if (countSpaces == 1)
            {
                grammerValidate = string.Format("is 1 space");
            }
            else
            {
                grammerValidate = string.Format("are {0} spaces", countSpaces);
            }

            Console.Write(string.Format("There {0} in your sentence.{1}", grammerValidate, Environment.NewLine));
        }
    }
}
