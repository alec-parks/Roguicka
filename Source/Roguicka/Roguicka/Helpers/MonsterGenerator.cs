using Roguicka.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Helpers {

    public enum EMonsterType {
        Goblin = 'G',
        Elf = 'E',
        Troll = 'T' ,
        Wizard = 'W',
        Rat = 'R'
    }

    public static class MonsterGenerator {

        public static Monster MakeMonsterOfLevel(int level, EMonsterType e) {
            return new Monster();
        }

    }
}
