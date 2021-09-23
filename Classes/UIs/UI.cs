﻿using ConsoleTables;
using RollSpelGrupp6.Classes.UIs;
using RollSpelGrupp6.Structures;
using System;
using System.Threading;

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
        public bool IsPlayerFightWinner { get; set; }

        private Coordinate NewPlayerLocation;

        public UI()
        {
            Generator = new Generator();
            Player = new Player();
            Player.DressUp();
            GameGrid = new Grid(Player);
            FightUI = new FightUI(Generator);
            StopGame = false;
            IsConsoleCleared = false;
            IsPlayerFightWinner = true;

            NewPlayerLocation = new Coordinate();
        }

        public void StartUI()
        {
            GameGrid.GenerateGrid();
            GameGrid.PrintGrid();
            Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
            Console.WriteLine("@");
            Console.SetCursorPosition(0, 19);
            //Console.WriteLine($"{Player.Name} HP: {Player.HP}\tLevel: {Player.Level}\tWeapon damage range: {Player.Weapon.HighDamage} to {Player.Weapon.HighDamage}");
            PrintUserInformation();
            Player.Lives.PrintLives();
            //Player.PlayerInventory.PrintInventory();
            while (!StopGame)
            {
                //if (!GameGrid.IsMonsterSpawning)
                //{
                //    if (GameGrid.Monsters.Count < GameGrid.MaxMonstersOnBoard)
                //    {
                //        GameGrid.IsMonsterSpawning = true;
                //        Thread addMonster = new Thread(GameGrid.RespawnMonster);
                //        addMonster.Start();
                //        //GameGrid.RespawnMonster();
                //    }
                //    else if (GameGrid.Boss.Count is 0)
                //    {
                //        GameGrid.IsMonsterSpawning = true;
                //        Thread addBoss = new Thread(GameGrid.RespawnBoss);
                //        addBoss.Start();
                //        //GameGrid.RespawnBoss();
                //    }
                //}
                if (!GameGrid.IsMonsterSpawning && GameGrid.Monsters.Count < GameGrid.MaxMonstersOnBoard)
                {
                    if (!GameGrid.IsRespawnedMonsterPrinted)
                    {
                        Console.SetCursorPosition(GameGrid.LastAddedMonster.Location.Col, GameGrid.LastAddedMonster.Location.Row);
                        Printer.PrintInColor(ConsoleColor.DarkYellow, 'x', false);
                    }
                    GameGrid.IsMonsterSpawning = true;
                    Thread addMonster = new Thread(GameGrid.RespawnMonster);
                    addMonster.Start();
                }
                if (!GameGrid.IsBossSpawning && GameGrid.Boss.Count == 0)
                {
                    GameGrid.IsBossSpawning = true;
                    Thread addBoss = new Thread(GameGrid.RespawnBoss);
                    addBoss.Start();
                }
                if (IsConsoleCleared)
                {
                    Console.Clear();
                    GameGrid.PrintGrid();
                    Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
                    Console.WriteLine("@");
                    Console.SetCursorPosition(0, 19);
                    //Console.WriteLine($"{Player.Name} HP: {Player.HP}\tLevel: {Player.Level}\tWeapon damage range: {Player.Weapon.HighDamage} to {Player.Weapon.HighDamage}");
                    PrintUserInformation();
                    Player.Lives.PrintLives();
                    //Player.PlayerInventory.PrintInventory();
                    IsConsoleCleared = false;
                    //GameGrid.IsFightUICurrentUI = false;
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
                            FightUiTransition(monster);
                            if (IsPlayerFightWinner)
                            {
                                lock (GameGrid.MonsterLock)
                                {
                                    GameGrid.Monsters.Remove(monster);
                                }
                            }
                            break;
                        }
                    }
                    if (GameGrid.Boss.Count > 0)
                    {
                        if (NewPlayerLocation.Equals(GameGrid.Boss[0].Location))
                        {
                            FightUiTransition(GameGrid.Boss[0]);
                            if (IsPlayerFightWinner)
                            {
                                lock (GameGrid.MonsterLock)
                                {
                                    GameGrid.Boss.RemoveAt(0);
                                }
                            }
                        }
                    }
                    if (IsPlayerFightWinner)
                    {
                        Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
                        Printer.PrintInColor(ConsoleColor.Black, ' ');
                        Console.SetCursorPosition(NewPlayerLocation.Col, NewPlayerLocation.Row);
                        Console.Write('@');
                        Player.Location.SetCoordinate(NewPlayerLocation.Row, NewPlayerLocation.Col);
                    }
                    IsPlayerFightWinner = true;
                }
            }
        }

        private void FightUiTransition(Monster monster)
        {
            GameGrid.IsFightUICurrentUI = true;
            Console.Clear();
            //if (FightUI.Combat(Player, monster))
            //{
            //}
            IsPlayerFightWinner = FightUI.Combat(Player, monster);
            Console.Clear();
            if (Player.Lives.LivesLeft == 0)
            {
                Printer.PrintInColor(ConsoleColor.Red, ($"YOU ARE OUT OF LIVES. YOUR SCORE IS {Player.Score}"));
                StopGame = true;
            }

            GameGrid.IsFightUICurrentUI = false;
            IsConsoleCleared = true;
        }

        private void PrintUserInformation()
        {
            string bossDamage = GameGrid.Boss.Count is 0 ? "Respawning" : GameGrid.Boss[0].HP.ToString();
            var tableUserInformation = new ConsoleTable("Player", "Level", "Experience", "Level Upgrade At", "Total Health", "Damage", "Boss Health");
            tableUserInformation.AddRow($"{Player.Name}", $"{Player.Level}", $"{Player.Experience} points", $"{Player.ExperienceBreakpoint} points", $"{Player.HP}", $"{Player.Weapon.LowDamage} - {Player.Weapon.HighDamage}", $"{bossDamage}");
            tableUserInformation.Write(Format.Alternative);
        }
    }
}