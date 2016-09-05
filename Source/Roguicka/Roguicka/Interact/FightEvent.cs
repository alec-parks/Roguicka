using Roguicka.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Interact {

    public enum EFightEvent {
        Hit,
        Magic
    }

    public class FightEvent : InteractEvent {

        IActor Sender;
        IDestructible Reciever;

        EFightEvent[] FightEvents;

        public FightEvent(IActor sender, IDestructible reciever, params EFightEvent[] e) {
            Sender = sender;
            Reciever = reciever;
            FightEvents = e;
            Messages[0] = Sender.Type.ToString() + " attacked for " + Sender.Stats.Attack + " dmg";
        }

        public override void Trigger() {
            //Still kinda weird
            
            Reciever?.TakeDamage(Sender.Stats.Attack);
        }

    }
}
