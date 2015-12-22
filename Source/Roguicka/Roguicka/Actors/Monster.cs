using RLNET;

namespace Roguicka.Actors
{
    public class Monster: IActor
    {
        public ActorType Type { get; set; } = ActorType.Monster;
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; set; } = true;
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int Chase { get; set; } = 3;

        public Monster(int currentHp, int maxHp, int xPos, int yPos, RLColor color, char symbol)
        {
            CurrentHP = currentHp;
            MaxHP = maxHp;
            X = xPos;
            Y = yPos;
            Color = color;
            Symbol = symbol;
        }

        public bool IsDead => CurrentHP <= 0;

        public void TakeDamage(int amount)
        {
            CurrentHP -= amount;
            if (IsDead)
            {
                SetDead();
            }
        }

        public void Heal(int amount)
        {
            if (amount > MaxHP)
            {
                CurrentHP = MaxHP;
            }
            else
            {
                CurrentHP += amount;
            }
        }

        private void SetDead()
        {
            Symbol = '%';
            Blocks = false;
        }
    }
}
