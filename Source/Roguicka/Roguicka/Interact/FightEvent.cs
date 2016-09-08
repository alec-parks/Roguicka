using Roguicka.Actors;
using Roguicka.Helpers;
using System;

namespace Roguicka.Interact {

    public enum EFightEvent {
        Hit,
        Magic
    }

    public class FightEvent : InteractEvent {

        Player Sender;
        Player Reciever;

        EFightEvent[] FightEvents;


        public FightEvent(Player sender, Player reciever, params EFightEvent[] e) {
            Sender = sender;
            Reciever = reciever;
            FightEvents = e;
            
        }
        
        public override void Trigger() {
            //Still kinda weird
            int damage = Sender.Stats.Attack + MonsterGenerator.Random.Next(-2 - (Sender.Stats.Level), 2 + (Sender.Stats.Level));
            damage -= Reciever.Stats.Defense / 4;
            //Make sure you can't heal the opponent when attacking
            damage = Math.Max(damage, 0);
            Reciever?.TakeDamage(damage);
            Messages[0] = Sender.Type.ToString() + " attacked for " + damage + " dmg";
        }

    }
}
