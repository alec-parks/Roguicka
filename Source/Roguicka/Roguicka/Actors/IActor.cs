using RLNET;

namespace Roguicka.Actors
{

    public class Stats {
        public int Attack { get; }
        public int Defense { get; }
        public int Energy { get; private set; }
        public int EnergyGain { get; }
        public int NeededEnergy { get; }
        public int Gold { get; }
        public int Exp { get; }
        public int Level { get; }

        public void AddEnergy()
        {
            Energy += EnergyGain;
        }

        public void UseEnergy() {
            Energy = 0;
        }

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
        //Stats Stats { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool Blocks { get; }
        RLColor Color { get; }
        char Symbol { get; }
        string Description { get; }
    }
}
