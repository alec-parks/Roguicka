using Newtonsoft.Json;

namespace Roguicka.Actors
{
    public class Spell
    {
        [JsonProperty]
        public int Power { get; protected set; }
        [JsonProperty]
        public int Range { get; protected set; }
        [JsonProperty]
        public int Area { get; protected set; }
        [JsonProperty]
        public ElementType[] Elements { get; protected set; }
        [JsonProperty]
        public string DamageType { get; protected set; }
        [JsonProperty]
        public string Name { get; protected set; }

        public Spell()
        {
            Power = 1;
            Range = 1;
            Area = 1;
        }

    }
}