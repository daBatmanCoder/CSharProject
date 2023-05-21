using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    public class ShowDate : IFunctionality
    {
        public void InvokeFunction()
        {
             Console.WriteLine(DateTime.Now.ToShortDateString());
        }
    }
}
