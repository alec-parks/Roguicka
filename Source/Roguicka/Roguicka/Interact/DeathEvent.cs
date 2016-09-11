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
            if (DeadActor.Type == ActorType.Monster && DeadActor.Stats.Stat["Exp"] != -1) {
                LiveActor.Stats.Stat["Gold"] += DeadActor.Stats.Stat["Gold"];
                LiveActor.Stats.Stat["Exp"] += DeadActor.Stats.Stat["Exp"];

                
                
                Messages[0] = Spacer;
                Messages[1] = "You gained " + DeadActor.Stats.Stat["Gold"] + " gold and " + DeadActor.Stats.Stat["Exp"] + " xp";
                Messages[2] = "Total gold: " + LiveActor.Stats.Stat["Gold"] + " Total xp: " + LiveActor.Stats.Stat["Exp"];
                Messages[3] = Spacer;
                DeadActor.Stats.Stat["Exp"] = -1;
                DeadActor = null;
                LevelUpHelper.CheckLeveledUp(LiveActor);

                
            }
        }

    }
}
