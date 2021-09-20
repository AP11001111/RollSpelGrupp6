using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    class Monster : Figure
    {
        public Monster(int level)
        {
            Level = level;
            Initiate();
        }

        public override void Attack(Figure figure)
        {
            figure.TakeDamage(AttackPower);
            Console.WriteLine($"Monster gör {AttackPower} skada på Dig. Du har {figure.HealthPoints} left!");
        }

        public override void TakeDamage(int damage)
        {
            HealthPoints -= damage - Armor;
            if (HealthPoints <= 0)
            {
                Console.WriteLine("Monster died!");
                //Ta bort från spelplan();
            }
        }

        private void Initiate()
        {
            HealthPoints = 2 * Level;
            AttackPower = 1 * Level;
        }
    }
}
