using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RollSpelGrupp6.Classes
{
    internal class UI
    {
        public Grid GameGrid { get; set; }
        public Player Player { get; set; }
        public Monster Monster { get; set; }
        public bool StopGame { get; set; }
        private Coordinate NewPlayerLocation;
        private Coordinate NewMonsterLocation;

        public UI()
        {
            GameGrid = new Grid();
            Player = new Player();
            Monster = new Monster();
            StopGame = false;
            NewMonsterLocation = new Coordinate();
            NewPlayerLocation = new Coordinate();
        }

        private void MonsterLocationSetup()
        {
            NewMonsterLocation.SetCoordinate(5, 5);
            Console.SetCursorPosition(Monster.Location.Col, Monster.Location.Row);
            Console.Write('X');
        }

        public void StartUI()
        {
            GameGrid.GenerateGrid();
            GameGrid.PrintGrid();
            MonsterLocationSetup();
            while (!StopGame)
            {
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            var keyPressed = Console.ReadKey(true).Key;
            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row - 1, Player.Location.Col);
                    Move();
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row + 1, Player.Location.Col);
                    Move();
                    break;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row, Player.Location.Col - 1);
                    Move();
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    NewPlayerLocation.SetCoordinate(Player.Location.Row, Player.Location.Col + 1);
                    Move();
                    break;

                case ConsoleKey.Escape:
                    Console.SetCursorPosition(0, 19);
                    StopGame = true;
                    break;
            }
        }

        private void Move()
        {
            if ((NewPlayerLocation.Row >= 0
                && NewPlayerLocation.Row < GameGrid.GameGrid.Length))
            {
                if (!(GameGrid.GameGrid[NewPlayerLocation.Row][NewPlayerLocation.Col] == '_' ||
                GameGrid.GameGrid[NewPlayerLocation.Row][NewPlayerLocation.Col] == '|'))
                {
                    Console.SetCursorPosition(Player.Location.Col, Player.Location.Row);
                    Console.Write('*');
                    Console.SetCursorPosition(NewPlayerLocation.Col, NewPlayerLocation.Row);
                    Console.Write('@');
                    Player.Location.SetCoordinate(NewPlayerLocation.Row, NewPlayerLocation.Col);
                }
            }
        }
    }
}