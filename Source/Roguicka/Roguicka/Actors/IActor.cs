using RLNET;

namespace Roguicka.Actors
{

    public interface IActor
    {
        ActorType Type { get; }
        //Stats Stats { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool Blocks { get; }
        RLColor Color { get; }
        char Symbol { get; }
        string Description { get; }
    }
}
