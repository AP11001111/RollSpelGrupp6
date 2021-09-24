using System;
using RollSpelGrupp6.Classes;

namespace RollSpelGrupp6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 28;
            Console.WindowWidth = 125;
            UI gameUI = new UI();
            gameUI.StartUI();
        }
    }
}