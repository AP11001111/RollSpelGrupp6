using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    public class Figure
    {
        public string Name { get; set; }
        public Coordinate Location { get; set; }

        //public int HealthPoints { get; set; }
        //public int AttackPower { get; set; }
        public int HP { get; set; }

        public int Damage { get; set; }
        public int Defence { get; set; }
        public int Dodge { get; set; }
        public int Level { get; set; }

        //public int Weapon { get; set; }
        //public int Armor { get; set; }
        public Weapon Weapon { get; set; }

        public Armor Armor { get; set; }
        public Helmet Helmet { get; set; }

        //public abstract void TakeDamage(int damage);

        //public abstract void Attack(Figure character);

        public void Preparations()
        {
            Defence += Helmet.Defence + Armor.Defence;
            HP += Helmet.HP + Armor.HP;
        }

        public void DressUp()
        {
            Weapon myWeapon = new Weapon();
            CheckStuff(myWeapon);
            Helmet myHelmet = new Helmet();
            CheckStuff(myHelmet);
            Armor myArmor = new Armor();
            CheckStuff(myArmor);
            Console.WriteLine("Tryck för att fortsätta.\n");
            Console.ReadLine();
            Console.Clear();
            this.Weapon = myWeapon;
            this.Helmet = myHelmet;
            this.Armor = myArmor;
        }

        public static void CheckStuff(Equipment equipment)
        {
            Console.WriteLine($"Type: {equipment.Name}");

            if (equipment is Weapon)
            {
                Console.WriteLine($"Damage: {equipment.LowDamage} - {equipment.HighDamage}");
                Console.WriteLine($"Crit Chance: {equipment.CritChance} %");
            }
            else
            {
                Console.WriteLine($"Defence: {equipment.Defence}");
                Console.WriteLine($"Extra stuff: {equipment.HP} HP");
            }

            Console.WriteLine();
        }
    }
}