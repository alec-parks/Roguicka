namespace Roguicka.Actors
{
    public interface IDestructible
    {
        int CurrentHp { get; set; }
        int MaxHp { get; set; }
        bool IsDead { get; }

        void TakeDamage(int amount);
        void Heal(int amount);
    }
}