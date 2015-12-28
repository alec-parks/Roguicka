using RLNET;

namespace Roguicka.Actors
{
    public class Hero : IActor, IDestructible
    {
        public ActorType Type { get; set; } = ActorType.Player;
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; set; } = true;
        public RLColor Color { get; set; } = RLColor.White;
        public char Symbol { get; set; }
        public int LightRadius { get; set; } = 10;
        public bool IsDead => CurrentHp <= 0;

        public Hero(int xPos, int yPos, int currentHp, int maxHp, char symbol)
        {
            X = xPos;
            Y = yPos;
            CurrentHp = currentHp;
            MaxHp = maxHp;
            Symbol = symbol;
        }

        public void TakeDamage(int amount)
        {
            CurrentHp -= amount;
        }

        public void Heal(int amount)
        {
            if (amount + CurrentHp > MaxHp)
            {
                CurrentHp = MaxHp;
            }
            else
            {
                CurrentHp += amount;
            }
        }
    }
}
