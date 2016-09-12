using RLNET;

namespace Roguicka.Actors
{
    public class Player:IActor,IDestructible
    {
        public ActorType Type { get; }
        public Stats Stats { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; private set; } = true;
        public RLColor Color { get; private set; } = RLColor.Black;
        public char Symbol { get; private set; }
        public string Description { get; set; } = "A player";
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
            Stats = new Stats(1,1,1,1,1,1,1);
        }

        public void LevelUpHp(int amount) {
            MaxHp += amount;
            CurrentHp = MaxHp;
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

        public virtual void TakeDamage(int amount)
        {
            if (amount >= 0)
            {
                CurrentHp -= amount;
            }
            if (IsDead)
            {
                SetDead();
            }
        }

        protected virtual void SetDead()
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