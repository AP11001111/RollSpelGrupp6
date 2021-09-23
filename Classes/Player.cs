﻿using RollSpelGrupp6.Structures;
using System;

namespace RollSpelGrupp6.Classes
{
    public class Player : Figure
    {
        public int Score { get; set; }
        public Lives Lives { get; set; }

        public int baseHP { get; set; }
        public Inventory PlayerInventory { get; set; }
        public int Experience { get; set; }
        public int ExperienceBreakpoint { get; set; }
        public Coordinate NewPlayerLocation { get; set; }

        public Player()
        {
            Score = 0;
            baseHP = 100;
            Lives = new Lives();
            PlayerInventory = new Inventory();
            Location = new Coordinate(1, 1);
            Name = "Sir Kurt";
            Level = 1;
            HP = baseHP;
            Dodge = 5;
            Experience = 0;
            ExperienceBreakpoint = 2;
        }

        public void IncreaseLevel()
        {
            Level++;
            Experience -= ExperienceBreakpoint;
            ExperienceBreakpoint = ExperienceBreakpoint * 3 / 2;
            MaxHP = MaxHP * 11 / 10;
            HP = MaxHP;
        }
    }
}