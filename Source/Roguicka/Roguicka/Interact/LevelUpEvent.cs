using RogueSharp.Random;
using Roguicka.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Interact {
    class LevelUpEvent : InteractEvent {

        IActor Actor;
        DotNetRandom random;

        public LevelUpEvent(IActor actor) {
            Actor = actor;
            random = new DotNetRandom();
        }

        public override void Trigger() {
            Actor.Stats.Attack += random.Next(6);
            Actor.Stats.Defense += random.Next(6);
            //Actor.Stats.NeededEnergy += random.Next(6);
            Actor.Stats.Exp = 0;
            Actor.Stats.Level++;
            Messages[0] = "\nLevel up to lvl: " + Actor.Stats.Level;
            Messages[1] = "Attack: " + Actor.Stats.Attack;
            Messages[2] = "Defense: " + Actor.Stats.Defense;
            //Messages[3] = "Speed: " + Actor.Stats.Speed + "\n";
        }

    }
}
