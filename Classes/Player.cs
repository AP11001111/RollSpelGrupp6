using RollSpelGrupp6.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    internal class Player : Figure
    {
        public Coordinate Location { get; set; }
        public int Experience { get; set; }
        public int ExperienceBreakpoint { get; set; }

        public Player(int sword, int helm)
        {
            Location = new Coordinate(1, 1);

            Weapon = sword;
            Armor = helm;

            Initiate();
            Setup();
        }

        private void Initiate()
        {
            Level = 1;
            Experience = 0;
            ExperienceBreakpoint = 4;
        }

        private void Setup()
        {
            HealthPoints = 2 * Level + Weapon;
            AttackPower = (1 * Level) + Armor;
        }

        public void GainExperience(int monsterLevel)
        {
            Experience = 2 * monsterLevel;
            if (Experience >= ExperienceBreakpoint)
            {
                ExperienceBreakpoint += 4;
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Level++;
            Setup();
        }

        public override void Attack(Figure figure)
        {
            figure.TakeDamage(AttackPower);
            Console.WriteLine($"Du gör {AttackPower} skada på Monster. Den har {figure.HealthPoints} left!");
        }

        public override void TakeDamage(int damage)
        {
            HealthPoints -= damage - Armor;
            if (HealthPoints <= 0)
            {
                Console.WriteLine("You died!");
                Respawn();
                //ResetMap();
            }
        }

        public void Respawn()
        {
            Initiate();
            Setup();
        }
    }
}