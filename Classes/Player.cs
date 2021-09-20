﻿using RollSpelGrupp6.Structures;
using System;

namespace RollSpelGrupp6.Classes
{
    internal class Player : Figure
    {
        public Bag PlayerBag { get; set; }
        public int Experience { get; set; }
        public int ExperienceBreakpoint { get; set; }

        //Constructor
        public Player()
        {
            Location = new Coordinate(1, 1);

            Weapon = sword;
            Armor = helm;
            PlayerBag = new Bag();
            Initiate();
            Setup();
        }

        //Sets a character to beginning, level 1
        private void Initiate()
        {
            Level = 1;
            Experience = 0;
            ExperienceBreakpoint = 4;
        }

        //Recalculates the stats
        private void Setup()
        {
            HealthPoints = 2 * Level + Weapon;
            AttackPower = (1 * Level) + Armor;
        }

        //
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

        //Player attacks
        public override void Attack(Figure figure)
        {
            figure.TakeDamage(AttackPower);
            Console.WriteLine($"Du gör {AttackPower} skada på Monster. Den har {figure.HealthPoints} left!");
        }

        //Player takes damage
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

        //Respawns the player back to level 1
        public void Respawn()
        {
            Initiate();
            Setup();
        }
    }
}