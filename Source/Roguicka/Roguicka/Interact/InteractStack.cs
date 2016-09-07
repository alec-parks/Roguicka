using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguicka.Interact {
    public static class InteractStack {

        static Queue<InteractEvent> InteractEvents = new Queue<InteractEvent>();

       
        public static void Push(InteractEvent iE) {
            InteractEvents.Enqueue(iE);
        }

        static void Pop() {
            InteractEvents.First().Trigger();
            string[] messages = InteractEvents.First().Messages;
            if (messages != null)
            foreach(string s in messages) {
                if (s != null) {
                    Console.WriteLine(s);
                }
            }
            InteractEvents.Dequeue();
        }

        public static void ExecuteStack() {
            
            while (InteractEvents.Count != 0) {

                Pop();
            }
        }

    }
}
