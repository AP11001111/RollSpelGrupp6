using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;

namespace RollSpelGrupp6
{
    public class Lives
    {
        public int LivesLeft { get; set; }
        public ConsoleTables.ConsoleTable TableOfLives { get; set; }

        public Lives()
        {
            LivesLeft = 3;
            //TableOfLives = new ConsoleTable();
        }

        public void PrintLives()
        {
            switch (LivesLeft)
            {
                case 0:
                    TableOfLives = new ConsoleTable("Lives", " ", " ", " ");
                    break;

                case 1:
                    TableOfLives = new ConsoleTable("Lives", "*", " ", " ");
                    break;

                case 2:
                    TableOfLives = new ConsoleTable("Lives", "*", "*", " ");
                    break;

                case 3:
                    TableOfLives = new ConsoleTable("Lives", "*", "*", "*");
                    break;
            }
            TableOfLives.Write(Format.Alternative);
        }
    }
}