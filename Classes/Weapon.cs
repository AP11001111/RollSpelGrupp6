using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes
{
    public class Weapon : Equipment
    {
        public Weapon()
        {
            init();
            SetLevel(1);
        }

        //Initierar vapnet till en ursprungsversion
        private void init()
        {
            Name = "Sword";
            LowDamage = 25;
            HighDamage = 50;

            LowCrit = 1;
            HighCrit = 10;
            CritChance = Generator.RandomNumber(LowCrit, HighCrit);

            DropChance = 33;
        }

        //Räknar om statsen att överrensstämma med Level
        public void SetLevel(int level)
        {
            Level = level;

            if (level > 1)
            {
                for (int i = 0; i < Level; i++)
                {
                    IncrementDamage();

                    LowCrit++;
                    HighCrit++;
                    CritChance = Generator.RandomNumber(LowCrit, HighCrit);
                    //Nån specielgrej var tredje level kanske?
                    if (Level % 3 == 0)
                    {

                    }
                }
            }
        }
        private void IncrementDamage()
        {
            LowDamage *= 11;
            HighDamage *= 11;
            LowDamage /= 10;
            HighDamage /= 10;
        }
    }
}
