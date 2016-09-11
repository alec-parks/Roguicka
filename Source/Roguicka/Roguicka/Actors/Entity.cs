using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Roguicka.Actors {
    public class Entity : IActor {
        public bool Blocks { get; }
        public RLColor Color { get; }
        public string Description { get; set; }
        public char Symbol { get; private set; }
        public ActorType Type { get; }
        public int X { get; set; }
        public int Y { get; set; }


       public Entity() {
            Blocks = false;
            Color = RLColor.White;
            Symbol = '0';
            X = 1;
            Y = 1;
        }

        public Entity(ActorType actorType, int x, int y, char symbol, bool blocker, RLColor color) {
            Type = actorType;
            X = x;
            Y = y;
            Blocks = false;
            Color = color;
            Symbol = symbol;
        }

        public bool IsStandingOn(Player p) {
            if (p.X == X && p.Y == Y) return true;
            return false;
        }

        //There may be more of these in the future, depending on what type of behaviour we want
        public virtual void Interact(Player p) { }

    }
}
