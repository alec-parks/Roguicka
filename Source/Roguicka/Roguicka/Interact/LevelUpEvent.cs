using RogueSharp.Random;
using Roguicka.Actors;

namespace Roguicka.Interact {
    class LevelUpEvent : InteractEvent {

        Player Actor;
        public DotNetRandom random;

        public LevelUpEvent(Player actor) {
            Actor = actor;
            random = new DotNetRandom();

        }

        public override void Trigger() {
            Actor.Stats.LevelUpStat("Attack", random.Next(1, 6));
            Actor.Stats.LevelUpStat("Defense", random.Next(1, 6));
            
            //Actor.Stats.Level++;
            //Prevent showing when enemies level up
            if (Actor.Type == ActorType.Hero) {
                //Actor.Stats.Exp = 0;
                Messages[0] = Special;
                Messages[1] = "Level up to lvl: " + Actor.Stats.Stat["Level"];
                Messages[2] = "Attack: " + Actor.Stats.Stat["Attack"];
                Messages[3] = "Defense: " + Actor.Stats.Stat["Defense"];
                Messages[4] = Special;
                //Messages[3] = "Speed: " + Actor.Stats.Speed + "\n";
            }

        }
    }
}
