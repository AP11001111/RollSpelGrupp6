using RollSpelGrupp6.Classes;
using RollSpelGrupp6.Classes.UIs;
using System;
using System.Linq;
using System.Threading;

namespace RollSpelGrupp6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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