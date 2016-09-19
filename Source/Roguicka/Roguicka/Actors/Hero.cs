using System.Collections.Generic;
using RLNET;

namespace Roguicka.Actors {
    public class Hero : Player {
        public int LightRadius { get; } = 10;
        private List<ElementType> Elements { get; } = new List<ElementType>();

        public Hero() : base(ActorType.Hero, 1, 1, '@', 1, true, RLColor.White) { }

        public Hero(int xPos, int yPos, int currentHp, char symbol) :
            base(ActorType.Hero, xPos, yPos, symbol, currentHp, true, RLColor.White) { }

        public void AddElement(ElementType element) {
            Elements.Add(element);
        }

        }
}
