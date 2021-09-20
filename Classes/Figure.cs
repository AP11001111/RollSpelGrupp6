using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    public abstract class Figure
    {
        public int HealthPoints { get; set; }
        public int AttackPower { get; set; }
        public int Level { get; set; }
        public int Weapon { get; set; }
        public int Armor { get; set; }

        public abstract void TakeDamage(int damage);

        public abstract void Attack(Figure character);
    }
}
