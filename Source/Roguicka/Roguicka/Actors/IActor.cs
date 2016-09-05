using RLNET;

namespace Roguicka.Actors
{

    public class Stats {
        public int Attack;
        public int Defense;
        public int Energy;
        public int EnergyGain;
        public int NeededEnergy;
        public int Gold;
        public int Exp;
        public int Level;

        public Stats(int attack, int defense, int needed, int enGain, int gold, int exp, int level) {
            Attack = attack;
            Defense = defense;
            Energy = 0;
            NeededEnergy = needed;
            EnergyGain = enGain;
            Gold = gold;
            Exp = exp;
            Level = level;
        }
    }

    public interface IActor
    {
        ActorType Type { get; }
        Stats Stats { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool Blocks { get; }
        RLColor Color { get; }
        char Symbol { get; }
    }
}
