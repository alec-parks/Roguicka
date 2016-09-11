using System.Collections.Generic;

namespace Roguicka.Actors {
    public class Stats {

        public Dictionary<string, int> Stat { get; private set; }

        public Stats(int attack, int defense, int energyGain, int neededEnergy, int gold, int exp, int level) {
            Stat = new Dictionary<string, int>();
            Stat["Attack"] = attack;
            Stat["Defense"] = defense;
            Stat["Energy"] = 0;
            Stat["EnergyGain"] = energyGain;
            Stat["NeededEnergy"] = neededEnergy;
            Stat["Gold"] = gold;
            Stat["Exp"] = exp;
            Stat["Level"] = level;
        }

        public void LevelUpStat(string statName, int value) {
            Stat[statName] += value;
        }

        public void AddEnergy() {
            Stat["Energy"] += Stat["EnergyGain"];
        }

        public void UseEnergy() {
            Stat["Energy"] -= Stat["NeededEnergy"];
        }

    }
}