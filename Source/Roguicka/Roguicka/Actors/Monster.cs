﻿using RLNET;
using RogueSharp;
using Roguicka.Engines;
using Roguicka.Helpers;
using Roguicka.Interact;

namespace Roguicka.Actors {
    public class Monster : Player {
        public new ActorType Type { get; } = ActorType.Monster;
        public EMonsterType MonsterType;
        public int Chase { get; set; } = 0;
        public Cell Target { get; set; }

        public Monster() : base(ActorType.Monster, 1, 1, 'M', 1, true, RLColor.Green) { }

        public Monster(EMonsterType monsterType, int maxHP, Stats stats, int x, int y, char symbol, RLColor color)
            :base(ActorType.Monster, x, y, symbol, maxHP, true, color){
            MonsterType = monsterType;
            Stats = stats;
            //Symbol = monsterType;
        }

        protected override void SetDead() {
            InteractStack.Push(new DeathEvent(Engine.GetHero(), this));
            base.SetDead();
            //TODO find altnernate way to do this.
            //Requiring an engine is tight coupling and makes testing test multiple things.
        }
    }
}
