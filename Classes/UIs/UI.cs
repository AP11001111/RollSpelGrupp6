﻿using RollSpelGrupp6.Structures;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RollSpelGrupp6.Classes
{
    internal class UI
    {
        public FightUI FightUI { get; set; }
        public Grid GameGrid { get; set; }
        public Player Player { get; set; }
        public Generator Generator { get; set; }
        public Monster Monster { get; set; }
        public bool StopGame { get; set; }
        public bool IsConsoleCleared { get; set; }

        private Coordinate NewPlayerLocation;

        public UI()
        {
            Player = new Player();
            Generator = new Generator();
            GameGrid = new Grid(Player);
            FightUI = new FightUI(Generator);
            StopGame = false;
            IsConsoleCleared = false;

            NewPlayerLocation = new Coordinate();
        }

        public void StartUI()
        {
            GameGrid.GenerateGrid();
            GameGrid.PrintGrid();
            Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
            Console.WriteLine("@");
            Console.SetCursorPosition(0, 19);
            Player.PlayerInventory.PrintInventory();
            while (!StopGame)
            {
                if (!GameGrid.IsMonsterSpawning && GameGrid.Monsters.Count < GameGrid.MaxMonstersOnBoard)
                {
                    GameGrid.IsMonsterSpawning = true;
                    Thread addMonster = new Thread(GameGrid.RespawnMonster);
                    addMonster.Start();
                }
                if (IsConsoleCleared)
                {
                    Console.Clear();
                    GameGrid.PrintGrid();
                    Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
                    Console.WriteLine("@");
                    Console.SetCursorPosition(0, 19);
                    Player.PlayerInventory.PrintInventory();
                    IsConsoleCleared = false;
                    GameGrid.IsFightUICurrentUI = false;
                }
                TakeInput();
                if (Player.PlayerInventory.IsContentUpdated)
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
                    Console.SetCursorPosition(0, 29);
                    StopGame = true;
                    break;

                case ConsoleKey.I:

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
                        if (NewPlayerLocation.Equals(monster.Location))
                        {
                            GameGrid.IsFightUICurrentUI = true;
                            Console.Clear();
                            FightUI.Combat(Player, monster);
                            Console.Clear();
                            lock (GameGrid.ListOfMonstersLock)
                            {
                                GameGrid.Monsters.Remove(monster);
                            }
                            IsConsoleCleared = true;

                            break;
                        }
                    }
                    Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
                    Console.Write(' ');
                    Console.SetCursorPosition(NewPlayerLocation.Col, NewPlayerLocation.Row);
                    Console.Write('@');
                    Player.Location.SetCoordinate(NewPlayerLocation.Row, NewPlayerLocation.Col);
                }
            }
        }
    }
}