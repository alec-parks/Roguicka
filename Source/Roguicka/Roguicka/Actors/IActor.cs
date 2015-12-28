using RLNET;

namespace Roguicka.Actors
{
    public interface IActor
    {
        ActorType Type { get; }
        int X { get; set; }
        int Y { get; set; }
        bool Blocks { get; }
        RLColor Color { get; }
        char Symbol { get; }
    }
}
