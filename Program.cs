using RollSpelGrupp6.Classes;
using System;
using System.Linq;
using System.Threading;

namespace RollSpelGrupp6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Player player = new Player();
            //player.PlayerInventory.PutItem(new Weapon());
            //player.PlayerInventory.PrintInventory();
            //Thread.Sleep(2000);
            UI gameUI = new UI();
            gameUI.StartUI();
        }
    }
}