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
            int damage = Sender.Stats.Stat["Attack"] + MonsterGenerator.Random.Next(-2 - (Sender.Stats.Stat["Level"]), 2 + (Sender.Stats.Stat["Level"]));
            damage -= Reciever.Stats.Stat["Defense"] / 4;
            //Make sure you can't heal the opponent when attacking
            damage = Math.Max(damage, 0);
            Reciever?.TakeDamage(damage);
            if (Sender.Type == ActorType.Hero) {
                var enemy = ((Monster)Reciever).MonsterType.ToString();
                Messages[0] = "Hero attacked " + enemy + " for " + damage + " damage";
            }
            else {
                var enemy = ((Monster)Sender).MonsterType.ToString();
                Messages[0] = enemy + " attacked you for " + damage + " damage";
            }
        }

    }
}
