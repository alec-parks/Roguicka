using RogueSharp.Random;
using Roguicka.Actors;

namespace Roguicka.Interact {
    class LevelUpEvent : InteractEvent {

        private readonly Player _actor;
        private static DotNetRandom _random;

        public LevelUpEvent(Player actor) {
            _actor = actor;
        }


        public static DotNetRandom Random
        {
            get { return _random ?? (_random = new DotNetRandom()); }
        }

        public override void Trigger() {
            _actor.Stats.LevelUpStat("Attack", Random.Next(1, 6));
            _actor.Stats.LevelUpStat("Defense", Random.Next(1, 6));
            
            _actor.Stats.LevelUpStat("Level", 1);
            //Prevent showing when enemies level up
            if (_actor.Type == ActorType.Hero) {
                _actor.Stats.Stat["Exp"] = 0;
                Messages[0] = Special;
                Messages[1] = "Level up to lvl: " + _actor.Stats.Stat["Level"];
                Messages[2] = "Attack: " + _actor.Stats.Stat["Attack"];
                Messages[3] = "Defense: " + _actor.Stats.Stat["Defense"];
                Messages[4] = Special;
                //Messages[3] = "Speed: " + Actor.Stats.Speed + "\n";
            }
        }

    }
}
