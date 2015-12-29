namespace Roguicka.Actors
{
    public interface IElement
    {
        ElementType Type { get; }
        CastType CastType { get; }
        int Range { get; }
        int Power { get; }
    }
}