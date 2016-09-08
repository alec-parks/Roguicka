using Roguicka.Actors;
using Roguicka.Interact;
using System;

namespace Roguicka.Helpers {
    public static class LevelUpHelper {

        //XP check, which is a slightly modified Pokemon leveling system
        public static int XpRequirement(int level) {
            return (int)(4 * (Math.Pow(level,3)) / 2);
        }

        //If they have enough Exp at their level, push a new event
        public static void CheckLeveledUp(Player actor) {
            if (actor.Stats.Exp > XpRequirement(actor.Stats.Level)) {
                InteractStack.Push(new LevelUpEvent(actor));
            }
        }

    }
}
