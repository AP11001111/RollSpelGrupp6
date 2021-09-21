using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    internal class Grid
    {
        public char[][] GameGrid { get; set; }
        public List<Monster> Monsters { get; set; }

        public Grid()
        {
            Monsters = new List<Monster>()
            {
                new Monster(1,3,3),
                new Monster(3,13,30),
                new Monster(10,9,23),
                new Monster(20,3,37)
            };
            GameGrid = new char[18][];
            for (int i = 0; i < GameGrid.Length; i++)
            {
                GameGrid[i] = new char[64];
            }
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
            //foreach (char[] array in GameGrid)
            //{
            //    foreach (char c in array)
            //    {
            //        Console.Write(c);
            //    }
            //    Console.Write("\n");
            //}
        }
    }
}