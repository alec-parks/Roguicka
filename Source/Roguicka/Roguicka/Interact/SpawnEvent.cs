using RLNET;
using RogueSharp.Random;
using Roguicka.Actors;
using Roguicka.Engines;
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
        }

        public override void Trigger() {
            for (int i = 0; i < SpawnCount; i++) {
                
                int x = random.Next(Game.Instance.Map.Width - 1);
                int y = random.Next(Game.Instance.Map.Height - 1);
                while (!Game.Instance.Map.IsWalkable(x,y)) {
                    x = random.Next(Game.Instance.Map.Width - 1);
                    y = random.Next(Game.Instance.Map.Height - 1);
                }
                Monster m = new Monster(50, 50, x, y, RLColor.Green, 'T');
                Engine.AddActor(m);
            }
        }

    }
}
