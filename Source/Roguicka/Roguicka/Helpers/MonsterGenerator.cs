using Roguicka.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguicka.Interact;
using RogueSharp.Random;

namespace Roguicka.Helpers {

    public enum EMonsterType {
        Goblin = 'G',
        Elf = 'E',
        Troll = 'T',
        Wizard = 'W',
        Rat = 'R'
    }



    public static class MonsterGenerator {

        public static DotNetRandom random = new DotNetRandom();

        public static Dictionary<EMonsterType, Stats> Beastiary = new Dictionary<EMonsterType, Stats>() {
            { EMonsterType.Goblin, new Stats(3,3,10,4,5,5,1) },
            { EMonsterType.Elf, new Stats(3,5,10,6,10,10,1) },
            { EMonsterType.Rat, new Stats(2,2,5,4,7,3,1) },
            { EMonsterType.Troll, new Stats(5,3,10,2,15,10,1) },
            { EMonsterType.Wizard, new Stats(4,4,10,7,0,15,1) }
        };

        //static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static Monster MakeMonster(int level, EMonsterType e) {
            var coord = Game.Instance.Map.GetFreeRandomCoord();
            Monster monster = new Monster(50, 50, coord.Item1, coord.Item2, RLColor.Red, (char)e);

            monster.Stats = Beastiary[e];
            for (int i = 0; i < level - 1; i++) {
                LevelUpEvent l = new LevelUpEvent(monster);
                //Now THAT'S what I call a hack
                l.random = random;
                InteractStack.Push(l);
            }

            return monster;
        }

        public static Monster MakeRandomMonster(int level) {
            var enemies = Beastiary.Values.ToList();
            var enemyType = Beastiary.Keys.ToList();

            int place = random.Next(enemyType.Count() - 1);
            return MakeMonster(level, enemyType[place]);
        }

    }
}
