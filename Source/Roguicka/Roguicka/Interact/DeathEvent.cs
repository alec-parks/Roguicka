using Roguicka.Actors;
using Roguicka.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Interact {
    public class DeathEvent : InteractEvent {

        IActor DeadActor;
        IActor LiveActor;

        public DeathEvent(IActor liveActor, IActor deadActor) {
            LiveActor = liveActor;
            DeadActor = deadActor;
        }

        public override void Trigger() {
            if (DeadActor.Type == ActorType.Monster && DeadActor.Stats.Exp != 0) {
                LiveActor.Stats.Gold += DeadActor.Stats.Gold;
                LiveActor.Stats.Exp += DeadActor.Stats.Exp;
                
                Messages[0] = "You gained " + DeadActor.Stats.Gold + " gold and " + DeadActor.Stats.Exp + " xp";
                Messages[1] = "Total gold: " + LiveActor.Stats.Gold + " Total xp: " + LiveActor.Stats.Exp;
                DeadActor.Stats.Gold = DeadActor.Stats.Exp = 0;
                DeadActor = null;
                LevelUpHelper.CheckLeveledUp(LiveActor);

                
            }
        }

    }
}
