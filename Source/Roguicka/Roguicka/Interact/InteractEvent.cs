namespace Roguicka.Interact {
    public abstract class InteractEvent
    {
        public abstract void Trigger();

        public string[] Messages = new string[10];

        //Just in case you want to seperate input
        public string Spacer = "----------";
        public string Special = "!!!!!!!!!!";

    }
}
