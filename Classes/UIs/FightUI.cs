using RollSpelGrupp6.Classes.UIs;
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

        public FightUI(Generator generator)
        {
            Generator = generator;
        }

        public bool Combat(Player player, Monster monster)
        {
            bool winner = true;
            Player = player;
            Monster = monster;
            int rond = 0;
            bool combat = true;

            //CreateMonster(); // Skapa en ny fiende och beväpnar den
            //Player.Preparations(); // Adderar attackskada samt defence
            Monster.DressUp();
            //Monster.Preparations();

            while (combat)
            {
                if (monster.IsBoss)
                {
                    Printer.PrintInColor(ConsoleColor.Yellow, "BOSS FIGHT");
                }
                if (combat)
                {
                    rond++;
                    Console.WriteLine($"< G R O T T A N >");
                    Console.WriteLine($"<<<[ ROND {rond} ]>>>\n");
                }

                int damage = Player.DoDamage();

                Monster.TakeDamage(damage);
                
                Console.WriteLine($"{Player.Name} inflicted {damage} damage to {Monster.Name}");
                Console.WriteLine($"{Monster.Name} has {Monster.HP} HP left.\n");

                if (Monster.HP < 1)
                {
                    Player.Score++;
                    Console.WriteLine($"{Monster.Name} is defeated.");
                    Player.Experience = monster.IsBoss ? Player.Experience + 3 : Player.Experience + 1;
                    if (Player.Experience >= Player.ExperienceBreakpoint)
                    {
                        Player.IncreaseLevel();
                    }
                    combat = false;
                }
                else
                {
                    damage = Monster.DoDamage();
                    Player.TakeDamage(Monster.DoDamage());

                    Console.WriteLine($"{Monster.Name} åsamkade {Player.Name} {damage} skada");
                    Console.WriteLine($"{Player.Name} har {Player.HP} HP left.\n");
                    //Console.WriteLine($"\nEfter Combat-Metoden har {Player.Name} {Player.HP} HP kvar.\n");
                }

                if (Player.HP < 1)
                {
                    Console.WriteLine($"{Player.Name} as been slayed.");
                    Player.Lives.LivesLeft--;

                    Player.HP = Player.MaxHP;
                    winner = false;
                    combat = false;
                }

                // Console.WriteLine($"\nEfter Combat-Metoden har {enemy.Name} {enemy.HP} HP kvar.\n");
                Console.WriteLine("===================================");
                Console.WriteLine("Tryck för att fortsätta \n");
                Console.ReadLine();
                Console.Clear();
            }
            return winner;
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
    }
}