namespace Roguicka.Actors
{
    public class FireElement : IElement
    {
        public ElementType Type { get; } = ElementType.Fire;
        public int Range { get; } = 10;
        public int Power { get; } = 10;
    }
}