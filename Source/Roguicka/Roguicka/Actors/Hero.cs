using System.Collections.Generic;
using System.Linq;
using RLNET;

namespace Roguicka.Actors
{
    public class Hero : IActor, IDestructible
    {
        public ActorType Type { get; } = ActorType.Player;
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Blocks { get; } = true;
        public RLColor Color { get; } = RLColor.White;
        public char Symbol { get; }
        public int LightRadius { get; } = 10;
        public bool IsDead => CurrentHp <= 0;
        private List<IElement> Elements { get; } = new List<IElement>();

        public Hero()
        {
            X = 1;
            Y = 1;
            CurrentHp = 1;
            MaxHp = 1;
            Symbol = '@';
        }

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
            if (amount >= 0)
            {
                CurrentHp -= amount;
            }
            
        }

        public void Heal(int amount)
        {
            if (amount + CurrentHp > MaxHp)
            {
                CurrentHp = MaxHp;
            }
            else if (amount >= 0)
            {
                CurrentHp += amount;
            }
        }

        public void AddElement(IElement element)
        {
            Elements.Add(element);
        }

        public int CastSpell(ElementType type)
        {
            var typeCount = Elements.Where(element => element.Type == type)
                                    .Sum(element=> element.Power);
            Elements.Clear();
            return typeCount;
        }
    }
}
