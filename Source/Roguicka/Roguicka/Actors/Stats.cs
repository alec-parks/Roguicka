using System.Collections.Generic;

namespace Roguicka.Actors {
    public class Stats {

        public Dictionary<string, int> Stat { get; }

        public Stats(int attack, int defense, int energyGain, int neededEnergy, int gold, int exp, int level) {
            Stat = new Dictionary<string, int>
            {
                ["Attack"] = attack,
                ["Defense"] = defense,
                ["Energy"] = 0,
                ["EnergyGain"] = energyGain,
                ["NeededEnergy"] = neededEnergy,
                ["Gold"] = gold,
                ["Exp"] = exp,
                ["Level"] = level
            };
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