using RLNET;

namespace Roguicka.Actors
{
    public class Player:IActor,IDestructible
    {
        public ActorType Type { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; private set; } = true;
        public RLColor Color { get; private set; } = RLColor.Black;
        public char Symbol { get; private set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; private set; }
        public bool IsDead => CurrentHp <= 0;

        public Player()
        {
            Type = ActorType.Monster;
            X = 1;
            Y = 1;
            Symbol = 'P';
            MaxHp = 1;
            CurrentHp = 1;
        }

        public Player(ActorType actorType, int x, int y, char symbol, int hp, bool blocker, RLColor color)
        {
            Type = actorType;
            X = x;
            Y = y;
            Blocks = blocker;
            Color = color;
            Symbol = symbol;
            MaxHp = hp;
            CurrentHp = hp;
        }

        public void TakeDamage(int amount)
        {
            CurrentHp -= amount;
            if (IsDead)
            {
                SetDead();
            }
        }

        private void SetDead()
        {
            Symbol = '%';
            Blocks = false;
            Color = RLColor.Gray;
        }

        public void Heal(int amount)
        {
            if (amount > 0)
            {
                if (CurrentHp + amount <= MaxHp)
                {
                    CurrentHp += amount;
                }
                else
                {
                    CurrentHp = MaxHp;
                }
            }
        }
    }
}