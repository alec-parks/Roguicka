using Roguicka.Interact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Helpers {
    public static class Camera {

        public static int xOffset { get; private set; }
        public static int yOffset { get; private set; }

        /*public static Camera(int x, int y) {
            xOffset = x;
            yOffset = y;
        }*/

        public static void UpdateCamera(int x, int y) {
            xOffset += x;
            yOffset += y;
        }

    }
}
