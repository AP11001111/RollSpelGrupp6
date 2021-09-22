using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    internal class Monster : Figure
    {
        public Monster(int level, int row, int col)
        {
            Location = new Coordinate(row, col);

            Name = "Plattjordare";
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
        //private void SetLevel(int level)
        //{
        //    Level = Level;
        //}

        //public override void Attack(Figure figure)
        //{
        //    figure.TakeDamage(AttackPower);
        //    Console.WriteLine($"Monster gör {AttackPower} skada på Dig. Du har {figure.HealthPoints} left!");
        //}

        //public override void TakeDamage(int damage)
        //{
        //    HealthPoints -= damage - Armor;
        //    if (HealthPoints <= 0)
        //    {
        //        Console.WriteLine("Monster died!");
        //        //Gain experience();
        //        //Ta bort från spelplan();
        //    }
        //}

        //private void Initiate()
        //{
        //    HealthPoints = 2 * Level;
        //    AttackPower = 1 * Level;
        //}
    }
}