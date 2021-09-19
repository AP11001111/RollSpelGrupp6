using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    internal class Player
    {
        public Coordinate Location { get; set; }

        public Player()
        {
            Location = new Coordinate(1, 1);
        }
    }
}