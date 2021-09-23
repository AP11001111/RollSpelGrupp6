using RollSpelGrupp6.Structures;

namespace RollSpelGrupp6.Classes
{
    internal class Monster : Figure
    {
        public bool IsBoss { get; set; }

        public Monster(int level, int row, int col, bool isBoss = false)
        {
            Location = new Coordinate(row, col);
            IsBoss = isBoss;
            Name = "Plattjordare";
            HP = 10 * level;
            Dodge = 5;
        }

        //Preparations
        public void PrepareMonster()
        {
            Defence = Helmet.Defence + Armor.Defence;
            HP = Helmet.HP + Armor.HP;
        }
    }
}