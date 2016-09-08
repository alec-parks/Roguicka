using System.Collections.Generic;
using System.Linq;
using RLNET;

namespace Roguicka.Actors
{
    public class Hero : Player
    {
        public int LightRadius { get; } = 10;
        private List<IElement> Elements { get; } = new List<IElement>();

        public Hero() : base(ActorType.Hero,1,1,'@',1,true,RLColor.White)
        {}

        public Hero(int xPos, int yPos, int currentHp, char symbol) :
            base(ActorType.Hero, xPos,yPos,symbol,currentHp,true,RLColor.White)
        {}

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
