namespace Roguicka.Actors
{
    interface IActor
    {
        ActorType Type { get; set; }
        int CurrentHP { get; set; }
        int MaxHP { get; set; }
        int X { get; set; }
        int Y { get; set; }

        bool IsDead();
    }
}
