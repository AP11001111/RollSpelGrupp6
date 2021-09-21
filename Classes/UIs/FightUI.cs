using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    internal class FightUI
    {
        public Player Player { get; set; }
        public Monster Monster { get; set; }

        public Generator Generator { get; set; }

        public FightUI(Player player, Monster monster)
        {
            Player = player;
            Monster = monster;
            Generator = new Generator();
        }

        public void Combat()
        {
            int rond = 0;
            bool combat = true;

            //CreateMonster(); // Skapa en ny fiende och beväpnar den
            Player.DressUp();
            Player.Preparations(); // Adderar attackskada samt defence
            Monster.DressUp();
            Monster.Preparations();

            while (combat)
            {
                if (combat)
                {
                    rond++;
                    Console.WriteLine($"< G R O T T A N >");
                    Console.WriteLine($"<<<[ ROND {rond} ]>>>\n");
                }

                PlayerAttacks(); // Hjälte anfaller

                if (Monster.HP < 1)
                {
                    Console.WriteLine($"{Monster.Name} is defeated.");

                    combat = false;
                }
                else
                {
                    MonsterAttacks();
                    //Console.WriteLine($"\nEfter Combat-Metoden har {Player.Name} {Player.HP} HP kvar.\n");
                }

                if (Player.HP < 1)
                {
                    Console.WriteLine($"{Player.Name} as been slayed.");

                    combat = false;
                }

                // Console.WriteLine($"\nEfter Combat-Metoden har {enemy.Name} {enemy.HP} HP kvar.\n");
                Console.WriteLine("===================================");
                Console.WriteLine("Tryck för att fortsätta \n");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void CombatTemp()
        {
        }

        //public void CreateMonster()
        //{
        //    Monster enemy = new Monster(Player.Level);
        //    enemy = Equipment.DressTheMonster(enemy);
        //    Monster = enemy;
        //}

        public static void MonsterDefeated()
        {
        }

        public void PlayerAttacks()
        {
            bool hit = HitOrMIss(); //Kollar om man träffar eller missar

            if (hit == false)
            {
                Console.WriteLine($"{Player.Name} missed.");
            }

            PlayerDamage();// Bestämmer hur hög skadan blir + crit
            ActualDamageToMonster(); // Ser hur mycket HP som förloras genom att ta värdet på skadan minus värdet på skyddet.
            //Det nya skadevärdet subtraheras från HP

            Console.WriteLine($"{Player.Name} inflicted {Player.Damage} damage to {Monster.Name}");
            Console.WriteLine($"{Monster.Name} has {Monster.HP} HP left.\n");
        }

        public void MonsterAttacks()
        {
            bool hit = HitOrMIss();

            if (hit == false)
            {
                Console.WriteLine($"{Monster.Name} missed.");
            }

            MonsterDamage();
            ActualDamageToPlayer();

            Console.WriteLine($"{Monster.Name} inflicted {Monster.Damage} damage to {Player.Name}");
            Console.WriteLine($"{Player.Name} has {Player.HP} HP left.\n");
        }

        public void PlayerDamage()
        {
            //int critHit = Generator.OneToHundred();
            int critHit = Generator.OneToHundred();
            Player.Damage = WeaponDamage(Player.Weapon.LowDamage, Player.Weapon.HighDamage);

            if (Player.Weapon.CritChance >= critHit)
            {
                Console.WriteLine("Critical Hit!");
                Player.Damage *= 2; // Om Critdamage
            }
        }

        public void MonsterDamage()
        {
            //int critHit = Generator.OneToHundred();
            int critHit = Generator.OneToHundred();
            Monster.Damage = WeaponDamage(Monster.Weapon.LowDamage, Monster.Weapon.HighDamage);
            if (Monster.Weapon.CritChance >= critHit)
            {
                Console.WriteLine("Critical Hit!");
                Monster.Damage *= 2; // Om Critdamage
            }
        }

        public static int WeaponDamage(int lowDamage, int highDamage)
        {
            return Generator.RandomNumber(lowDamage, highDamage + 1);
        }

        public bool HitOrMIss()
        {
            //int hit = Generator.OneToHundred();
            int hit = Generator.OneToHundred();
            if (hit < Monster.Dodge)
            {
                return false;
            }

            return true;
        }

        public void ActualDamageToMonster()
        {
            Player.Damage -= Monster.Defence;

            if (Player.Damage < 0)
            {
                Player.Damage = 0;
            }
            Monster.HP = Monster.HP - Player.Damage;
        }

        public void ActualDamageToPlayer()
        {
            Monster.Damage -= Player.Defence;

            if (Monster.Damage < 0)
            {
                Monster.Damage = 0;
            }
            Player.HP = Player.HP - Monster.Damage;
        }
    }
}