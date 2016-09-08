using System.Collections.Generic;

namespace Roguicka.Actors {
    public class Stats {

        public Dictionary<string, int> Stat { get; private set; }

        public Stats(int attack, int defense, int needed, int enGain, int gold, int exp, int level) {
            Stat = new Dictionary<string, int>();
            Stat["Attack"] = attack;
            Stat["Defense"] = defense;
            Stat["Energy"] = 0;
            Stat["EnergyGain"] = needed;
            Stat["NeededEnergy"] = enGain;
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
            Stat["Energy"] = 0;
        }

    }
}