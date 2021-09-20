using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RollSpelGrupp6.Classes
{
    internal class UI
    {
        public FightUI FightUI { get; set; }
        public Grid GameGrid { get; set; }
        public Player Player { get; set; }
        public Monster Monster { get; set; }
        public bool StopGame { get; set; }
        private Coordinate NewPlayerLocation;
        private Coordinate NewMonsterLocation;

        public UI()
        {
            GameGrid = new Grid();
            Player = new Player(); //Setting input parameter as (1,1) to avoid the error
            FightUI = new FightUI();
            StopGame = false;
            NewMonsterLocation = new Coordinate();
            NewPlayerLocation = new Coordinate();
        }

        public void StartUI()
        {
            GameGrid.GenerateGrid();
            GameGrid.PrintGrid();
            while (!StopGame)
            {
                TakeInput();
                if (Player.PlayerBag.IsContentUpdated)
                {
                    //update the bag shown on screen
                }
            }
        }

        private void TakeInput()
        {
            var keyPressed = Console.ReadKey(true).Key;
            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row - 1, Player.Location.Col);
                    MovePlayer();
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row + 1, Player.Location.Col);
                    MovePlayer();
                    break;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row, Player.Location.Col - 1);
                    MovePlayer();
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row, Player.Location.Col + 1);
                    MovePlayer();
                    break;

                case ConsoleKey.Escape:
                    Console.SetCursorPosition(0, 19);
                    StopGame = true;
                    break;

                case ConsoleKey.D1:
                    //Implement part for choosing equipment

                    //internal void SetEquipment(int equipmentType)
                    //{
                    //    //Take equipment position from user
                    //    //PlayerBag.BagContents[equipmentType - 1][equipmentPosition] as current equipment
                    //    throw new NotImplementedException();
                    //}
                    //Player.SetEquipment(1);
                    break;
            }
        }

        private void MovePlayer()
        {
            if ((NewPlayerLocation.Row >= 0
                && NewPlayerLocation.Row < GameGrid.GameGrid.Length))
            {
                if (!(GameGrid.GameGrid[NewPlayerLocation.Row][NewPlayerLocation.Col] == '_' ||
                GameGrid.GameGrid[NewPlayerLocation.Row][NewPlayerLocation.Col] == '|'))
                {
                    foreach (Monster monster in GameGrid.Monsters)
                    {
                        if (NewPlayerLocation.Row == monster.Location.Row && NewPlayerLocation.Col == monster.Location.Row)
                        {
                            Console.SetCursorPosition(0, 19);
                            StartFight(monster);
                        }
                    }
                    Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
                    Console.Write('*');
                    Console.SetCursorPosition(NewPlayerLocation.Col, NewPlayerLocation.Row);
                    Console.Write('@');
                    Player.Location.SetCoordinate(NewPlayerLocation.Row, NewPlayerLocation.Col);
                }
            }
        }

        private void StartFight(Monster monster)
        {
            //Player.DecreaseHP(10);
            //Player.IncreaseHP(10);
            Console.WriteLine("Nu slår vi monstern!!!");
            Thread.Sleep(2000);
        }
    }
}