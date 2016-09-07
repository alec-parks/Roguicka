using RogueSharp.Random;
using Roguicka.Actors;
using Roguicka.Helpers;
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
            
        }
        
        public override void Trigger() {
            //Still kinda weird
            int damage = Sender.Stats.Attack + MonsterGenerator.random.Next(-2 - (Sender.Stats.Level), 2 + (Sender.Stats.Level));
            Reciever?.TakeDamage(damage);
            Messages[0] = Sender.Type.ToString() + " attacked for " + damage + " dmg";
        }

    }
}
