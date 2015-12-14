using RLNET;

namespace Roguicka.Actors
{
    interface IActor
    {
        ActorType Type { get; set; }
        int CurrentHP { get; set; }
        int MaxHP { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool Blocks { get; set; }

        RLColor Color { get; set; }

        char Symbol { get; set; }

        bool IsDead();
        void TakeDamage(int amount);
        void Heal(int amount);
    }
}
