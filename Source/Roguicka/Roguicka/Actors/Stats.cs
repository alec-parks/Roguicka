namespace Roguicka.Actors {
    public class Stats {
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Energy { get; private set; }
        public int EnergyGain { get; private set; }
        public int NeededEnergy { get; private set; }
        public int Gold { get; private set; }
        public int Exp { get; private set; }
        public int Level { get; private set; }

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

        

        public void AddEnergy() {
            Energy += EnergyGain;
        }

        public void UseEnergy() {
            Energy = 0;
        }

    }
}