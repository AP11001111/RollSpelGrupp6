using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6
{
    internal class Generator
    {
        public static Random Rnd { get; set; }

        public Generator()
        {
            Rnd = new Random();
        }

        public static int RandomNumber(int lowNum, int highNum)
        {
            return Rnd.Next(lowNum, highNum + 1);
        }

        public static int OneToHundred()
        {
            return Rnd.Next(1, 101);
        }
    }
}