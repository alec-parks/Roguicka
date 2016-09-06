using RLNET;
using RogueSharp;

namespace Roguicka.Actors
{
    public class Monster: Player
    {
        public new ActorType Type { get; } = ActorType.Monster;
        public int Chase { get; set; } = 0;
        public Cell Target { get; set; }

        public Monster() : base(ActorType.Monster, 1,1,'M',1, true, RLColor.Green)
        {}

        public Monster(int maxHp, int xPos, int yPos, RLColor color, char symbol, bool blocker):
            base(ActorType.Monster, xPos,yPos,symbol,maxHp,blocker,color)
        {}

    }
}
