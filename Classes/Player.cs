using RollSpelGrupp6.Structures;
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
        //public Coordinate Location { get; set; }

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

        //Recalculates the stats
        //private void PreparePlayer()
        //{
        //    Defence = Helmet.Defence + Armor.Defence;
        //    //HP = Helmet.HP + Armor.HP
        //    MaxHP = HP + Helmet.HP + Armor.HP;
        //}
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