using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Structures
{
    internal class Coordinate
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Coordinate()
        {
        }

        public Coordinate(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void SetCoordinate(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}