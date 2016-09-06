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
        Troll = 'T' ,
        Wizard = 'W',
        Rat = 'R'
    }

    

    public static class MonsterGenerator {

        static DotNetRandom random = new DotNetRandom();

        static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static Monster MakeMonsterOfLevel(int level, EMonsterType e) {
            var coord = Game.Instance.Map.GetFreeRandomCoord();
            
            Monster monster = new Monster( 50, 50, coord.Item1,coord.Item2, RLColor.Red, chars[random.Next(26)]);
            for (int i = 0; i < level - 1; i++) {
                LevelUpEvent l = new LevelUpEvent(monster);
                //Now THAT'S what I call a hack
                l.random = random;
                InteractStack.Push(l);
            }
            
            return monster;
        }

    }
}
