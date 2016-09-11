using System.Collections.Generic;
using System.Linq;
using Roguicka.Actors;
using Roguicka.Maps;
using Roguicka.Interact;

namespace Roguicka.Engines
{
    public class LogicEngine
    {
        private const int ChaseTurns = 3;
        public static IEnumerable<IActor> _actors;


        //Im sorry for using a static
        public static void CheckCollisionWithEntity(Player p) {
            var _entities = _actors.OfType<Entity>();
            foreach(var e in _entities) {
                if (e.IsStandingOn(p)) {
                    InteractStack.Push(new SteppedOnEvent(p, e));
                }
            }
        }

        public void Actors(IEnumerable<IActor> actors)
        {
            _actors = actors;
        }
    }
}