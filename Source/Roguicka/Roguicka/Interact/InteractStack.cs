using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguicka.Interact {
    /// <summary>
    /// This is actually a Queue
    /// Any commands can get pushed to this, then are executed at the end of the round
    /// 
    /// </summary>
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
                    //TODO: Convert this system over to logging engine
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
