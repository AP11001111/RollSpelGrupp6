using RollSpelGrupp6.Classes.UIs;
using System;

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
            Monster.DressUp();

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

                Monster.TakeDamage(Player.DoDamage());

                Console.WriteLine($"{Player.Name} inflicted {Player.Damage} damage to {Monster.Name}");
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
                    Player.TakeDamage(Monster.DoDamage());

                    Console.WriteLine($"{Monster.Name} åsamkade {Player.Name} {Monster.Damage} skada");
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
    }
}