using RLNET;

namespace Roguicka.Actors {
    public abstract class Entity : IActor {
        public bool Blocks { get; }
        public RLColor Color { get; }
        public char Symbol { get; }
        public ActorType Type { get; }
        public int X { get; set; }
        public int Y { get; set; }


        protected Entity() {
            Blocks = false;
            Color = RLColor.White;
            Symbol = '0';
            X = 1;
            Y = 1;
        }

        protected Entity(ActorType actorType, int x, int y, char symbol, bool blocker, RLColor color) {
            Type = actorType;
            X = x;
            Y = y;
            Blocks = blocker;
            Color = color;
            Symbol = symbol;
        }

        public bool IsStandingOn(Player p)
        {
            return p.X == X && p.Y == Y;
        }

        //There may be more of these in the future, depending on what type of behaviour we want
        public abstract void Interact(Player p);

    }
}
