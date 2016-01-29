using System.Collections.Generic;
using System.Linq;
using Roguicka.Actors;
using Roguicka.Maps;

namespace Roguicka.Engines
{
    public class LogicEngine
    {
        private const int ChaseTurns = 3;
        private IEnumerable<IActor> _actors;

        public bool Move(IActor actor, int newX, int newY, IRoguickaMap map)
        {
            bool turn = false;
            if (map.GetCell(newX, newY).IsWalkable)
            {
                if (CheckForBlock(newX, newY))
                {
                    var blocker = _actors.First(blocked => blocked.X == newX && blocked.Y == newY && blocked is IDestructible) as IDestructible;
                    blocker?.TakeDamage(10);
                    turn = true;
                }
                else
                {
                    actor.X = newX;
                    actor.Y = newY;
                    turn = true;
                }
            }
            return turn;
        }

        private bool CheckForBlock(int x, int y)
        {
            bool blocked = false;
            foreach (var actor in _actors)
            {
                if (actor.X == x && actor.Y == y && actor.Blocks)
                {
                    blocked = true;
                }
            }
            return blocked;
        }

        public void Actors(IEnumerable<IActor> actors)
        {
            _actors = actors;
        }
    }
}