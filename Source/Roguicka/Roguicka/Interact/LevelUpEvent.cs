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
            int hpChange = Random.Next(5, 10), atkChange = Random.Next(1, 6), defChange = Random.Next(1, 6);
            _actor.LevelUpHP(hpChange);
            _actor.Stats.LevelUpStat("Attack", atkChange);
            _actor.Stats.LevelUpStat("Defense", defChange);

            _actor.Stats.LevelUpStat("Level", 1);
            //Prevent showing when enemies level up
            if (_actor.Type == ActorType.Hero) {
                _actor.Stats.Stat["Exp"] = 0;
                Messages[0] = Special;
                Messages[1] = "Level up to lvl: " + _actor.Stats.Stat["Level"];
                Messages[2] = "HP: " + (_actor.MaxHp - hpChange) + " > " + _actor.MaxHp;
                Messages[3] = "Attack: " + (_actor.Stats.Stat["Attack"] - atkChange) + " > " + _actor.Stats.Stat["Attack"];
                Messages[4] = "Defense: " + (_actor.Stats.Stat["Defense"] - defChange) + " > " + _actor.Stats.Stat["Defense"];
                Messages[5] = Special;
            }
        }

    }
}
