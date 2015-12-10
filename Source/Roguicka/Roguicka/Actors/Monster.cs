using System;
using RLNET;

namespace Roguicka.Actors
{
    class Monster: IActor
    {
        public ActorType Type { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }

        Monster()
        {
            Type = ActorType.Monster;
        }

        public Monster(int currentHp, int maxHp, int xPos, int yPos, RLColor color, char symbol)
        {
            CurrentHP = currentHp;
            MaxHP = maxHp;
            X = xPos;
            Y = yPos;
            Color = color;
            Symbol = symbol;
            Blocks = true;
            Type = ActorType.Monster;
        }

        public bool IsDead()
        {
            bool dead = CurrentHP <= 0;
            if (dead)
            {
                Symbol = '%';
                Blocks = false;
            }
            return dead;
        }

        public void TakeDamage(int amount)
        {
            CurrentHP -= amount;
            IsDead();
        }
    }
}
