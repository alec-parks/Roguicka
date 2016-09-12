using RLNET;
using Roguicka.Helpers;

namespace Roguicka.Actors {
    public class Stairs : Entity {

        //This kinda makes sense actually
        FloorLevelHelper _helper;

        public Stairs(FloorLevelHelper helper, int x, int y) 
            :base(ActorType.Object, x, y, '&', false, RLColor.Yellow){
            _helper = helper;
        }

        //By this point, we already know that the player is standing on it
        public override void Interact(Player p) {
            if (p.Type == ActorType.Hero) {
                _helper.SetLevel("Second");
                //Trigger a map change
                //Game.Instance.Map = new Maps.RoguickaMap(Map.Create(new CaveMapCreationStrategy<Map>(50, 50, 55, 4, 3)));
            }
        }

    }
}
