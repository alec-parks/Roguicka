using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Interact {
    public class InteractEvent {
        public virtual void Trigger() { }

        public string[] Messages = new string[10];

    }
}
