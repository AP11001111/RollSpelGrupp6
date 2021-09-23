using System;
using RollSpelGrupp6.Classes;

namespace RollSpelGrupp6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;
            UI gameUI = new UI();
           
            gameUI.StartUI();
        }
    }
}