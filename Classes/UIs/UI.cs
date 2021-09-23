using ConsoleTables;
using RollSpelGrupp6.Classes.UIs;
using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
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
        public string PlayerUsername { get; set; }
        public bool StopGame { get; set; }
        public bool IsConsoleCleared { get; set; }
        public bool IsPlayerFightWinner { get; set; }

        private Coordinate NewPlayerLocation;

        public UI()
        {
            Generator = new Generator();
            SetUpUI();
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
            PrintUserInformation();
            PrintPlayerRankings();
            Player.Lives.PrintLives();
            while (!StopGame)
            {
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
                    PrintUserInformation();
                    PrintPlayerRankings();
                    Player.Lives.PrintLives();
                    IsConsoleCleared = false;
                }
                TakeInput();
                if (Player.PlayerInventory.IsContentUpdated)
                {
                    //update the bag shown on screen
                }
            }
            if (StopGame)
            {
                if (Player.Lives.LivesLeft == 0)
                {
                    Player.ResetPlayer();
                    PlayerDatabase.AddUserToPlayerDatabase(PlayerUsername, Player);
                    PlayerDatabase.WriteToPlayerDatabase();
                }
                else
                {
                    PlayerDatabase.AddUserToPlayerDatabase(PlayerUsername, Player);
                    PlayerDatabase.WriteToPlayerDatabase();
                    Console.SetCursorPosition(0, 29);
                }
            }
        }

        private void SetUpUI()
        {
            bool isUsernameAccepted = false;
            Printer.PrintInColor(ConsoleColor.Yellow, "Välkommen till spelet");
            Printer.PrintInColor(ConsoleColor.Yellow, "\nAnge användarnamn");
            PlayerUsername = Console.ReadLine().ToLower();
            while (!isUsernameAccepted)
            {
                if (PlayerUsername.Length > 1
                    && PlayerUsername.Length < 15
                    && !PlayerUsername.Contains(" ")
                    && !PlayerUsername.Contains("\\"))
                {
                    isUsernameAccepted = true;
                    break;
                }
                Printer.PrintInColor(ConsoleColor.Red, "\nOgiltigt värde.\nAnvändarnamnet måste vara mellan 2 och 15 tecken lång och kan inte innehålla ' ' och '\\'");
                PlayerUsername = Console.ReadLine().ToLower();
            }
            PlayerDatabase.ReadFromPlayerDatabase();
            Player = PlayerDatabase.GetUserFromPlayerDatabase(PlayerUsername);
            Console.Clear();
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
                    StopGame = true;
                    break;

                case ConsoleKey.R:
                    Player.ResetPlayer();
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
            var tableUserInformation = new ConsoleTable("Player", "Level", "Experience", "Level Upgrade At", "Total Health", "Damage", "Boss Health", "Score", "High Score");
            tableUserInformation.AddRow($"{Player.Name}", $"{Player.Level}", $"{Player.Experience} points", $"{Player.ExperienceBreakpoint} points", $"{Player.HP}", $"{Player.Weapon.LowDamage} - {Player.Weapon.HighDamage}", $"{bossDamage}", $"{Player.Score}", $"{Player.HighScore}");
            tableUserInformation.Write(Format.Alternative);
        }

        private void PrintPlayerRankings()
        {
            var tableOfHighScores = new ConsoleTable("Player", "Score");
            foreach (Player player in PlayerDatabase.ListOfTop10Players)
            {
                tableOfHighScores.AddRow($"{player.Name}", $"{player.HighScore}");
            }
            tableOfHighScores.Write(Format.Alternative);
        }
    }
}