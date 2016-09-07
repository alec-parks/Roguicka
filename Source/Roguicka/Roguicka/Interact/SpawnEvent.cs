using RLNET;
using RogueSharp.Random;
using Roguicka.Actors;
using Roguicka.Engines;
using Roguicka.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Interact {
    public class SpawnEvent : InteractEvent {

        int SpawnCount;
        DotNetRandom random = new DotNetRandom();

        //Change so theres multiple ways of spawning
        public SpawnEvent(int spawnCount) {
            SpawnCount = spawnCount;
            Messages[0] = Spacer;
            Messages[1] = "Spawning: " + spawnCount + " enemies";
            Messages[2] = Spacer;
        }

        public override void Trigger() {
            for (int i = 0; i < SpawnCount; i++) {

                var coord = Game.Instance.Map.GetFreeRandomCoord();
                //Monster type doesn't work yet
                Monster m = MonsterGenerator.MakeRandomMonster(1);
                Engine.AddActor(m);
            }
        }

    }
}
