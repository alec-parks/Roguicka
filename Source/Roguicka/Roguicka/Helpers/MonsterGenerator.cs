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
        Rat = 'R',
        Druid = 'D'
    }

    public class Bestiary {
        public List<Monster> MonsterList = new List<Monster>();
    }

    public static class MonsterGenerator {

        public static DotNetRandom Random = new DotNetRandom();

        public static Bestiary Bestiary = new Bestiary();

        //Here is where we create a monster from the prefabs
        public static Monster MakeMonster(int level, EMonsterType e) {
            //Make sure that they can spawn in a valid place
            var walkableCell = Game.Instance.Map.GetRandomCell();
            //Fill up monster with stats
            Stats Stats = Bestiary.MonsterList.Single(x => x.MonsterType == e).Stats;
            int MaxHp = Bestiary.MonsterList.Single(x => x.MonsterType == e).CurrentHp;
            Monster monster = new Monster(e, MaxHp, Stats, walkableCell.X, walkableCell.Y, (char)e, RLColor.Red);

            //This is where we bring the monster up to the required level
            for (var i = 0; i < level - 1; i++) {
                //Level up events are shared with the hero, except I modified what happens a little bit
                LevelUpEvent l = new LevelUpEvent(monster);
                //Now THAT'S what I call a hack
                InteractStack.Push(l);
            }

            return monster;
        }
        //This just picks a EMonsterType at random then calls the normal make monster function
        public static Monster MakeRandomMonster(int level) {
            
            int place = Random.Next(Bestiary.MonsterList.Count() - 1);
            return MakeMonster(level, Bestiary.MonsterList[place].MonsterType);
        }

        //Just deserialize the JSON right into the Bestiary
        public static void LoadMonstersFromJSON(string fileName) {
            using (StreamReader r = new StreamReader(fileName + ".json")) {
                string json = r.ReadToEnd();
                Bestiary = JsonConvert.DeserializeObject<Bestiary>(json);
            }
        }


    }
}
