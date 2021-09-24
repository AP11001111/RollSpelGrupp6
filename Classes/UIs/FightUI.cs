using RollSpelGrupp6.Classes.UIs;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            while (combat)
            {
                if (monster.IsBoss)
                {
                    Printer.PrintInColor(ConsoleColor.DarkRed, "BOSS FIGHT");
                }
                if (combat)
                {
                    rond++;
                    Console.Write("Du möter en ");
                    Printer.PrintInColor(ConsoleColor.DarkYellow, $"{Monster.Name}");
                    // Console.WriteLine($"< G R O T T A N >");
                    Console.Write($"\n<<<[ "); Console.ForegroundColor = ConsoleColor.Green; Console.Write($"ROND {rond}"); Console.ResetColor();
                    //Printer.PrintInColor(ConsoleColor.Green, $"ROND{rond}");
                    Console.WriteLine(" ]>>>\n");
                }

                int damage = Player.DoDamage();

                Monster.TakeDamage(damage);

                Console.WriteLine($"{Player.Name} åsamkade {Monster.Name} {damage} skada");
                Console.WriteLine($"{Monster.Name} har {Monster.HP} HP left.\n");

                if (Monster.HP < 1)
                {
                    Player.Score++;
                    if (Player.HighScore < Player.Score)
                    {
                        Player.HighScore = Player.Score;
                    }
                    PlayerDatabase.UpdateListOfTop7Players(Player);
                    Console.WriteLine($"{Monster.Name} har dräpts.");

                    //Drop(player);
                    Drop();
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
                    Player.TakeDamage(damage);

                    Console.WriteLine($"{Monster.Name} åsamkade {Player.Name} {damage} skada");
                    Console.WriteLine($"{Player.Name} har {Player.HP} HP left.\n");
                    //Console.WriteLine($"\nEfter Combat-Metoden har {Player.Name} {Player.HP} HP kvar.\n");
                }

                if (Player.HP < 1)
                {
                    Console.WriteLine($"{Player.Name} har avlidit. Beklagar.");
                    Player.Lives.LivesLeft--;

                    Player.HP = Player.MaxHP;
                    winner = false;
                    combat = false;
                }

                // Console.WriteLine($"\nEfter Combat-Metoden har {enemy.Name} {enemy.HP} HP kvar.\n");
                Console.WriteLine("===================================");
                Console.WriteLine("Tryck för att fortsätta \n");
                Console.ReadKey();
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

        public void Drop()
        {
            var dropChanceList = new int[2];
            CreateDropList(dropChanceList);

            bool drop = DropOrNot(dropChanceList[0]);

            if (drop == true)
            {
                TypeOfDrop();
            }

            DropPotions(dropChanceList[1]);
        }

        public void DropPotions(int mark)
        {
            if (mark >= Monster.PotionDropChance)
            {
                Player.Potions++;
                Console.WriteLine($"\n{Monster.Name} tappade en HP-flaska");
            }
        }

        public void TypeOfDrop()
        {
            int typeOfEquipmentDrop = Generator.RandomNumber(1, 3);
            if (typeOfEquipmentDrop == 1)
            {
                CompareWeapon();
                bool exchangeEquipment = SwitchOrNot();
                if (exchangeEquipment == true)
                {
                    Player.Weapon = Monster.Weapon;
                }
            }
            else if (typeOfEquipmentDrop == 2)
            {
                CompareArmor(Player.Helmet, Monster.Helmet);
                bool exchangeEquipment = SwitchOrNot();
                if (exchangeEquipment == true)
                {
                    Player.Weapon = Monster.Weapon;
                }
            }
            else if (typeOfEquipmentDrop == 3)
            {
                CompareArmor(Player.Armor, Monster.Armor);
                bool exchangeEquipment = SwitchOrNot();
                if (exchangeEquipment == true)
                {
                    Player.Weapon = Monster.Weapon;
                }
            }
        }

        public int[] CreateDropList(int[] dropChanceList)
        {
            for (int i = 0; i < dropChanceList.Length; i++)
            {
                dropChanceList[i] = Generator.OneToHundred();
            }
            return dropChanceList;
        }

        public bool DropOrNot(int mark)
        {
            if (mark >= Monster.EquipmentDropChance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SwitchOrNot()
        {
            while (true)
            {
                Console.Write("\nVill du byta? J/N: ");

                char svar = Console.ReadKey().KeyChar;
                //svar.ToUpper();
                if (Char.ToUpper(svar) == 'J')
                {
                    return true;
                }
                else if (Char.ToUpper(svar) == 'N')
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Felaktig knapptryckning. Svara 'J' eller 'N'");
                }
            }
        }

        public void CompareWeapon()
        {
            Console.WriteLine("\nDenna har du på dig");
            Console.WriteLine("▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼\n");
            Console.Write($"Typ av utrustning: "); Printer.PrintInColor(ConsoleColor.Magenta, Player.Weapon.Name);
            Console.Write($"\nSkada: "); BestEquipment(Player.Weapon.LowDamage, Monster.Weapon.LowDamage, false); Console.Write("-");
            BestEquipment(Player.Weapon.HighDamage, Monster.Weapon.HighDamage);
            Console.Write($"\nCritical hit chance: "); BestEquipment(Player.Weapon.CritChance, Monster.Weapon.CritChance);

            Console.WriteLine("\n\nDenna kan du byta till");
            Console.WriteLine("▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼\n");
            Console.Write($"Typ av utrustning: "); Printer.PrintInColor(ConsoleColor.Magenta, Player.Weapon.Name);
            Console.Write($"\nSkada: "); BestEquipment(Monster.Weapon.LowDamage, Player.Weapon.LowDamage, false); Console.Write("-");
            BestEquipment(Monster.Weapon.HighDamage, Player.Weapon.HighDamage);
            Console.Write($"\nCritical hit chance: "); BestEquipment(Monster.Weapon.CritChance, Player.Weapon.CritChance);
        }

        public void CompareArmor(Equipment playerEquipment, Equipment monsterEquipment)
        {
            Console.WriteLine("\nDenna har du på dig");
            Console.WriteLine("▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼\n");
            Console.Write($"Typ av utrustning: "); Printer.PrintInColor(ConsoleColor.Magenta, playerEquipment.Name);
            Console.Write($"\nSkydd: "); BestEquipment(playerEquipment.Defence, monsterEquipment.Defence);
            Console.WriteLine();
            Console.Write($"Extra HP: "); BestEquipment(playerEquipment.HP, monsterEquipment.HP);

            Console.WriteLine("\n\nDenna kan du byta till");
            Console.WriteLine("▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼ ▼\n");
            Console.Write($"Typ av utrustning: "); Printer.PrintInColor(ConsoleColor.Magenta, monsterEquipment.Name);
            Console.Write($"\nSkydd: "); BestEquipment(monsterEquipment.Defence, playerEquipment.Defence);
            Console.WriteLine();
            Console.Write($"Extra HP: "); BestEquipment(monsterEquipment.HP, playerEquipment.HP);
        }

        public static void BestEquipment(int equipment1, int equipment2, bool newLine = true)
        {
            if (equipment1 > equipment2)
            {
                Printer.PrintInColor(ConsoleColor.DarkGreen, $"{equipment1}", newLine);
            }
            else if (equipment1 < equipment2)
            {
                Printer.PrintInColor(ConsoleColor.DarkRed, $"{equipment1}", newLine);
            }
            else
            {
                Printer.PrintInColor(ConsoleColor.Yellow, $"{equipment1}", newLine);
            }
        }
    }
}