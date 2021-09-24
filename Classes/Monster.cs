﻿using RollSpelGrupp6.Structures;

namespace RollSpelGrupp6.Classes
{
    public class Monster : Figure
    {
        public bool IsBoss { get; set; }
        public int EquipmentDropChance = 1;
        public int PotionDropChance = 1;

        public Monster(int level, int row, int col, bool isBoss = false)
        {
            Location = new Coordinate(row, col + 30);//offsetting game grid to right by 30
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
        }

        //Preparations
        public void PrepareMonster()
        {
            Defence = Helmet.Defence + Armor.Defence;
            HP = Helmet.HP + Armor.HP;
        }
    }
}