﻿using RollSpelGrupp6.Structures;
using System;

namespace RollSpelGrupp6.Classes
{
    public class Player : Figure
    {
        private int baseHP = 100;
        public Inventory PlayerInventory { get; set; }
        public int Experience { get; set; } = 0;
        public int ExperienceBreakpoint { get; set; } = 4;
        public Coordinate NewPlayerLocation { get; set; }
        //public Coordinate Location { get; set; }

        public Player()
        {
            PlayerInventory = new Inventory();
            Location = new Coordinate(1, 1);
            Name = "Sir Kurt";
            Level = 1;
            HP = baseHP;
            Dodge = 5;
        }

        //Recalculates the stats
        private void PreparePlayer()
        {
            Defence = Helmet.Defence + Armor.Defence;
            HP = Helmet.HP + Armor.HP;
        }
    }

    //public class Player : Figure
    //{
    //    public Inventory PlayerInventory { get; set; }
    //    public int Experience { get; set; }
    //    public int ExperienceBreakpoint { get; set; }

    //    //Constructor
    //    public Player()
    //    {
    //        Location = new Coordinate(1, 1);

    //        PlayerInventory = new Inventory();
    //        Initiate();
    //        Setup();
    //    }

    //    //Sets a character to beginning, level 1
    //    private void Initiate()
    //    {
    //        Level = 1;
    //        Experience = 0;
    //        ExperienceBreakpoint = 4;
    //    }



    //    //
    //    public void GainExperience(int monsterLevel)
    //    {
    //        Experience = 2 * monsterLevel;
    //        if (Experience >= ExperienceBreakpoint)
    //        {
    //            ExperienceBreakpoint += 4;
    //            LevelUp();
    //        }
    //    }

    //    public void LevelUp()
    //    {
    //        Level++;
    //        Setup();
    //    }

    //    //Player attacks
    //    public override void Attack(Figure figure)
    //    {
    //        figure.TakeDamage(AttackPower);
    //        Console.WriteLine($"Du gör {AttackPower} skada på Monster. Den har {figure.HealthPoints} left!");
    //    }

    //    //Player takes damage
    //    public override void TakeDamage(int damage)
    //    {
    //        HealthPoints -= damage - Armor;
    //        if (HealthPoints <= 0)
    //        {
    //            Console.WriteLine("You died!");
    //            Respawn();
    //            //ResetMap();
    //        }
    //    }

    //    //Respawns the player back to level 1
    //    public void Respawn()
    //    {
    //        Initiate();
    //        Setup();
    //    }
    //}
}