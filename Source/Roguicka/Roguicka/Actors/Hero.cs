using RLNET;

namespace Roguicka.Actors
{
    public class Hero : IActor
    {
        public ActorType Type { get; set; } = ActorType.Player;
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; set; } = true;
        public RLColor Color { get; set; } = RLColor.White;
        public char Symbol { get; set; }
        public int LightRadius { get; set; } = 10;
        public bool IsDead => CurrentHP <= 0;

        public Hero(int xPos, int yPos, int currentHp, int maxHp, char symbol)
        {
            X = xPos;
            Y = yPos;
            CurrentHP = currentHp;
            MaxHP = maxHp;
            Symbol = symbol;
        }

        public void TakeDamage(int amount)
        {
            CurrentHP -= amount;
        }

        public void Heal(int amount)
        {
            if (amount + CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
            else
            {
                CurrentHP += amount;
            }
        }
    }
}
