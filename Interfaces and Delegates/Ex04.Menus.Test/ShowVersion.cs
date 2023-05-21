using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    public class ShowVersion : IFunctionality
    {
        public void InvokeFunction()
        {
             Console.WriteLine("Version: 22.2.4.8950");
        }
    }
}
