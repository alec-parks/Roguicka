using Roguicka.Actors;
using System.Collections.Generic;
using System.Linq;
using RLNET;
using Roguicka.Interact;
using RogueSharp.Random;
using System.IO;
using Newtonsoft.Json;

namespace Roguicka.Helpers {

    public enum EMonsterType {
        Goblin = 'G',
        Elf = 'E',
        Troll = 'T',
        Wizard = 'W',
        Rat = 'R'
    }

    public class Beastiary {
        public List<Monster> MonsterList = new List<Monster>();
    }

    public static class MonsterGenerator {

        public static DotNetRandom Random = new DotNetRandom();

        public static Beastiary Beastiary = new Beastiary();

        public static Monster MakeMonster(int level, EMonsterType e) {
            var coord = Game.Instance.Map.GetFreeRandomCoord();
            Monster monster = new Monster(50, coord.Item1, coord.Item2, RLColor.Red, (char)e, true) {
                MonsterType = e,
                Stats = Beastiary.MonsterList.Single(x => x.MonsterType == e).Stats,
                CurrentHp = Beastiary.MonsterList.Single(x => x.MonsterType == e).CurrentHp,
                
                
            };

            for (var i = 0; i < level - 1; i++) {
                LevelUpEvent l = new LevelUpEvent(monster);
                //Now THAT'S what I call a hack
                l.random = Random;
                InteractStack.Push(l);
            }

            return monster;
        }

        public static Monster MakeRandomMonster(int level) {
            int place = Random.Next(Beastiary.MonsterList.Count() - 1);
            return MakeMonster(level, Beastiary.MonsterList[place].MonsterType);
        }

        public static void LoadMonstersFromJSON(string fileName) {
            using (StreamReader r = new StreamReader(fileName + ".json")) {
                string json = r.ReadToEnd();
                Beastiary = JsonConvert.DeserializeObject<Beastiary>(json);
            }
        }


    }
}
