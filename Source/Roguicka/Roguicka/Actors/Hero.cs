using RLNET;

namespace Roguicka.Actors
{
    class Hero : IActor
    {
        public ActorType Type { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }

        Hero()
        {
          Type = ActorType.Player;  
        }

        public Hero(int xPos, int yPos, int currentHp, int maxHp, char symbol)
        {
            X = xPos;
            Y = yPos;
            CurrentHP = currentHp;
            MaxHP = maxHp;
            Type = ActorType.Player;
            Color = RLColor.White;
            Symbol = symbol;
        }

        public bool IsDead()
        {
            return CurrentHP <= 0 ;
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
