using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RollSpelGrupp6.Classes
{
    internal class Grid
    {
        public Player Player { get; set; }
        public char[][] GameGrid { get; set; }
        public List<Monster> Monsters { get; set; }
        public int MaxMonstersOnBoard { get; set; }
        public bool IsMonsterSpawning { get; set; }
        public bool IsFightUICurrentUI { get; set; }
        public readonly object ListOfMonstersLock = new object();

        public Grid(Player player)
        {
            Player = player;
            MaxMonstersOnBoard = 4;
            Monsters = new List<Monster>();
            GameGrid = new char[18][];
            for (int i = 0; i < GameGrid.Length; i++)
            {
                GameGrid[i] = new char[64];
            }

            for (int i = 0; i < MaxMonstersOnBoard; i++)
            {
                SpawnMonster();
            }

            IsMonsterSpawning = false;
            IsFightUICurrentUI = false;
        }

        public void GenerateGrid()
        {
            Console.CursorVisible = false;
            GameGrid[0] = GameGrid[0].Select(c => '_').ToArray();
            GameGrid[^1] = GameGrid[^1].Select(c => '_').ToArray();
            for (int i = 1; i < GameGrid.Length - 1; i++)
            {
                for (int j = 0; j < GameGrid[i].Length; j++)
                {
                    switch (j)
                    {
                        case 0:
                            GameGrid[i][j] = '|';
                            break;

                        case 63:
                            GameGrid[i][j] = '|';
                            break;

                        default:
                            GameGrid[i][j] = ' ';
                            break;
                    }
                }
            }
            for (int i = 13; i < GameGrid.Length; i++)
            {
                GameGrid[i][53] = '_';
                GameGrid[i][53] = '|';
            }
            for (int i = 55; i < GameGrid[12].Length - 1; i++)
            {
                GameGrid[12][i] = '_';
            }
            GameGrid[^1][0] = '|';
            GameGrid[^1][^1] = '|';
            //GameGrid[1][1] = '@';
        }

        public void PrintGrid()
        {
            for (int i = 0; i < GameGrid.Length; i++)
            {
                for (int j = 0; j < GameGrid[i].Length; j++)
                {
                    bool isMonster = false;
                    foreach (Monster monster in Monsters)
                    {
                        if (monster.Location.Equals(new Coordinate(i, j)))
                        {
                            isMonster = true;
                            Console.Write("X");
                        }
                    }
                    if (!isMonster)
                    {
                        Console.Write(GameGrid[i][j]);
                    }
                }
                Console.Write("\n");
            }
        }

        public void RespawnMonster()
        {
            lock (ListOfMonstersLock)
            {
                Thread.Sleep(5000);
                Monster spawnedMonster = SpawnMonster();
                if (!IsFightUICurrentUI)
                {
                    Console.SetCursorPosition(spawnedMonster.Location.Col, spawnedMonster.Location.Row);
                    Console.Write("X");
                }
                IsMonsterSpawning = false;
            }
        }

        public Monster SpawnMonster()
        {
            bool monsterAdded = false;
            Monster monsterToReturn = new Monster(Player.Level + 2, Generator.RandomNumber(1, 16), Generator.RandomNumber(1, 62));
            if (Monsters.Count == 0 &&
               !monsterToReturn.Location.Equals(Player.Location) &&
               !(GameGrid[monsterToReturn.Location.Row][monsterToReturn.Location.Col] is '_') &&
               !(GameGrid[monsterToReturn.Location.Row][monsterToReturn.Location.Col] is '|'))
            {
                Monsters.Add(monsterToReturn);
                return monsterToReturn;
            }
            while (!monsterAdded)
            {
                Monster monster = new Monster(Player.Level + 2, Generator.RandomNumber(1, 16), Generator.RandomNumber(1, 62));
                foreach (Monster monsterInList in Monsters)
                {
                    if (!monster.Location.Equals(monsterInList.Location) &&
                        !monster.Location.Equals(Player.Location) &&
                        !(GameGrid[monster.Location.Row][monster.Location.Col] is '_') &&
                        !(GameGrid[monster.Location.Row][monster.Location.Col] is '|'))
                    {
                        Monsters.Add(monster);
                        monsterToReturn = monster;
                        monsterAdded = true;
                        break;
                    }
                }
            }
            return monsterToReturn;
        }
    }
}