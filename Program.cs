using System;
using RollSpelGrupp6.Classes;

namespace RollSpelGrupp6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;
            UI gameUI = new UI();
            UI.StartScreen();
            //Grid grid = new Grid(new Player());
            //grid.GenerateGrid();
            //grid.PrintGrid();
            //Thread.Sleep(10000);
            gameUI.StartUI();
        }
    }
}