namespace Roguicka.Actors
{
    public interface IElement
    {
        ElementType Type { get; }
        int Range { get; }
        int Power { get; }
    }
}