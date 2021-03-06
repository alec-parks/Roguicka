﻿using Roguicka.Actors;
using Roguicka.Interact;
using System;

namespace Roguicka.Helpers {
    public static class LevelUpHelper {

        
        public static int XpRequirement(int level) {
            return (int)(4 * (Math.Pow(level,3)) / 2);
        }

        public static void CheckLeveledUp(IActor actor) {
            if (actor.Stats.Exp > XpRequirement(actor.Stats.Level)) {
                InteractStack.Push(new LevelUpEvent(actor));
            }
        }

    }
}
