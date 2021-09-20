using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RollSpelGrupp6.Classes
{
    internal class Bag
    {
        public Equipment[][] BagContents { get; set; }
        public bool IsContentUpdated { get; set; }

        public Bag()
        {
            BagContents = new Equipment[3][]
            {
                new Weapon[3],
                new DefenseEquipment[3],
                new Potion[3]
            };
            IsContentUpdated = false;
        }

        public void DropItem()
        {
        }

        public void PutItem(Equipment equipment)
        {
            int equipmentType = GetEquipmentType(equipment);
            (bool, int) temp = HasSpace(equipmentType);
            if (temp.Item1)
            {
                BagContents[equipmentType][temp.Item2] = equipment;
            }
            else
            {
                //Ask user which item to replace
            }
        }

        public (bool, int) HasSpace(int equipmentType)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BagContents[equipmentType][i] is null)
                {
                    return (true, i);
                }
            }
            return (false, -1);
        }

        public int GetEquipmentType(Equipment equipment)
        {
            if (equipment is Weapon)
            {
                return 0;
            }
            else if (equipment is DefenseEquipment)
            {
                return 1;
            }
            else //if (equipment is Potion)
            {
                return 2;
            }
        }

        public void PrintBag()
        {
            foreach (Equipment[] equipmentArray in BagContents)
            {
                foreach (Equipment equipment in equipmentArray)
                {
                    if (equipment is null)
                    {
                        Console.WriteLine("0");
                    }
                    else
                    {
                        Console.WriteLine(equipment);
                    }
                }
            }
        }
    }
}