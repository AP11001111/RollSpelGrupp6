using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    internal class Grid
    {
        public char[][] GameGrid { get; set; }

        public Grid()
        {
            GameGrid = new char[18][];
            for (int i = 0; i < GameGrid.Length; i++)
            {
                GameGrid[i] = new char[64];
            }
        }

        public void GenerateGrid()
        {
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
            GameGrid[1][1] = '@';
        }

        public void PrintGrid()
        {
            foreach (char[] array in GameGrid)
            {
                foreach (char c in array)
                {
                    Console.Write(c);
                }
                Console.Write("\n");
            }
        }

        public void ShowUpdatedPlayerLocation()
        {
        }
    }
}