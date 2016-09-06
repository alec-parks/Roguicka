using System;
using RLNET;
using RogueSharp;
using Roguicka.Engines;
using Roguicka.Interact;

namespace Roguicka.Actors {
    public class Monster : IActor, IDestructible {
        public ActorType Type { get; } = ActorType.Monster;
        public Stats Stats { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; private set; } = true;
        public RLColor Color { get; }
        public char Symbol { get; private set; }
        public int Chase { get; set; } = 0;
        public bool IsDead => CurrentHp <= 0;
        public Cell Target { get; set; }
        public string Description { get; set; }

        public Monster() {
            CurrentHp = 100;
            MaxHp = 100;
            X = 1;
            Y = 1;
            Color = RLColor.Green;
            Symbol = 'M';
            Stats = new Stats(1, 1, 1, 1, 1, 1, 1);
            
        }

        public Monster(int currentHp, int maxHp, int xPos, int yPos, RLColor color, char symbol) {
            CurrentHp = currentHp;
            MaxHp = maxHp;
            X = xPos;
            Y = yPos;
            Color = color;
            Symbol = symbol;
            Stats = new Stats(5, 5, 10, 3, 10, 10, 1);
            Description = "A scary monster, with " + CurrentHp + "/" + MaxHp + "hp";
        }

        public void TakeDamage(int amount) {
            CurrentHp -= amount;
            Description = "A scary monster, with " + CurrentHp + "/" + MaxHp + "hp";
            if (IsDead) {
                SetDead();
            }
        }

        public void Heal(int amount) {
            if (amount + CurrentHp > MaxHp) {
                CurrentHp = MaxHp;
            }
            else if (amount >= 0) {
                CurrentHp += amount;
            }
        }

        private void SetDead() {

            Blocks = false;
            InteractStack.Push(new DeathEvent(Engine.GetHero(), this));
            Symbol = '%';
        }
    }
}
