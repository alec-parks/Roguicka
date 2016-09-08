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
            Actor.LevelUp();
            //Actor.Stats.Attack += random.Next(1,6);
            //Actor.Stats.Defense += random.Next(6);
            //Actor.Stats.NeededEnergy += random.Next(6);

            //Prevent showing when enemies level up
            if (Actor.Type == ActorType.Player) {
                //Actor.Stats.Exp = 0;
                Messages[0] = Special;
                Messages[1] = "Level up to lvl: " + Actor.Stats.Level;
                Messages[2] = "Attack: " + Actor.Stats.Attack;
                Messages[3] = "Defense: " + Actor.Stats.Defense;
                Messages[4] = Special;
                //Messages[3] = "Speed: " + Actor.Stats.Speed + "\n";
            }
        }

    }
}
