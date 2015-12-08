namespace Roguicka.Actors
{
    class Hero : IActor
    {
        public ActorType Type { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        Hero()
        {
          Type = ActorType.Player;  
        }

        public Hero(int xPos, int yPos, int currentHp, int maxHp)
        {
            X = xPos;
            Y = yPos;
            CurrentHP = currentHp;
            MaxHP = maxHp;
            Type = ActorType.Player;
        }

        public bool IsDead()
        {
            return CurrentHP <= 0 ;
        }
    }
}
