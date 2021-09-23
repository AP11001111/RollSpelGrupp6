using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    public class Monster : Figure
    {
        public bool IsBoss { get; set; }

        public Monster(int level, int row, int col, bool isBoss = false)
        {
            Location = new Coordinate(row, col);
            IsBoss = isBoss;
            Level = level;
            if (isBoss)
            {
                Name = "Anti-Vaccer: Karen";
            }
            else
            {
                Name = "Plattjordare";
            }
            HP = 10 * level;
            Dodge = 5;
            //SetLevel(level);
            //Initiate();
        }

        //Preparations
        public void PrepareMonster()
        {
            Defence = Helmet.Defence + Armor.Defence;
            HP = Helmet.HP + Armor.HP;
        }
    }
}