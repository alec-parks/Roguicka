using RLNET;
using RogueSharp;

namespace Roguicka.Actors
{
    public class Monster: IActor, IDestructible
    {
        public ActorType Type { get; } = ActorType.Monster;
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

        public Monster()
        {
            CurrentHp = 1;
            MaxHp = 1;
            X = 1;
            Y = 1;
            Color = RLColor.Green;
            Symbol = 'M';
        }

        public Monster(int currentHp, int maxHp, int xPos, int yPos, RLColor color, char symbol)
        {
            CurrentHp = currentHp;
            MaxHp = maxHp;
            X = xPos;
            Y = yPos;
            Color = color;
            Symbol = symbol;
        }

        public void TakeDamage(int amount)
        {
            CurrentHp -= amount;
            if (IsDead)
            {
                SetDead();
            }
        }

        public void Heal(int amount)
        {
            if (amount+CurrentHp > MaxHp)
            {
                CurrentHp = MaxHp;
            }
            else if(amount >=0)
            {
                CurrentHp += amount;
            }
        }

        private void SetDead()
        {
            Symbol = '%';
            Blocks = false;
        }
    }
}
