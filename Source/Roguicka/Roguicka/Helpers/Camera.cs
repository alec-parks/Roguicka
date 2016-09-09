namespace Roguicka.Helpers {
    public static class Camera {

        public static int xOffset { get; private set; }
        public static int yOffset { get; private set; }

        public static void UpdateCamera(int x, int y) {
            xOffset += x;
            yOffset += y;
        }

    }
}
