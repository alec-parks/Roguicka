using Roguicka.Actors;
using Roguicka.Helpers;

namespace Roguicka.Interact {
    public class DeathEvent : InteractEvent {

        Player DeadActor;
        Player LiveActor;

        public DeathEvent(Player liveActor, Player deadActor) {
            LiveActor = liveActor;
            DeadActor = deadActor;
        }

        public override void Trigger() {
            if (DeadActor.Type == ActorType.Monster && DeadActor.Stats.Exp != -1) {
                //LiveActor.Stats.Gold += DeadActor.Stats.Gold;
                //LiveActor.Stats.Exp += DeadActor.Stats.Exp;
                Messages[0] = Spacer;
                Messages[1] = "You gained " + DeadActor.Stats.Gold + " gold and " + DeadActor.Stats.Exp + " xp";
                Messages[2] = "Total gold: " + LiveActor.Stats.Gold + " Total xp: " + LiveActor.Stats.Exp;
                Messages[3] = Spacer;
                DeadActor = null;
                LevelUpHelper.CheckLeveledUp(LiveActor);

                
            }
        }

    }
}
