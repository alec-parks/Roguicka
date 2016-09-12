using Roguicka.Actors;

namespace Roguicka.Interact {
    public class SteppedOnEvent : InteractEvent {

        Player _player;
        Entity _entity;



        public SteppedOnEvent(Player player, Entity entity) {
            _player = player;
            _entity = entity;
        }

        //This is where we can do checks for things like stepping on traps, or walking on items/stairs
        public override void Trigger() {
            //Due to the event system, this must be called here
            _entity.Interact(_player);
        }

    }
}
