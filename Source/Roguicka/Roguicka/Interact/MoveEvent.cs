using Roguicka.Actors;
using Roguicka.Engines;
using Roguicka.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Interact {

    public enum EMoveEvent {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        MoveUL,
        MoveUR,
        MoveDL,
        MoveDR
    }

    public class MoveEvent : InteractEvent {

        EMoveEvent[] MovementTypes;

        IActor Actor;
        IRoguickaMap Map;

        //Do we check for valid move every move?
        //if false, it will allow diagonal movement
        //if true, it will allow character to move more than once, checking each frame
        bool CheckMove;

        int moveX, moveY;

        public MoveEvent(IActor hero, IRoguickaMap map, bool checkMove, params EMoveEvent[] e) {
            Actor = hero;
            MovementTypes = e;
            CheckMove = checkMove;
            Map = map;
            moveX = moveY = 0;
            
        }

        bool Move() {
            int newX = Actor.X + moveX;
            int newY = Actor.Y + moveY;

            
            bool turn = false;
            if (Map.GetCell(newX, newY).IsWalkable) {
                if (CheckForBlock(newX, newY)) {
                    //if (Actor.Type == ActorType.Player) {
                    var blocker = LogicEngine._actors.First(blocked => blocked.X == newX && blocked.Y == newY && blocked is IDestructible) as IActor;
                    if (blocker.Type != Actor.Type) {
                        InteractStack.Push(new FightEvent(Actor, (IDestructible)blocker, EFightEvent.Hit));
                    }
                    //}
                    //InteractStack.Pop();
                    //blocker?.TakeDamage(10);

                    turn = true;
                }
                else {
                    Actor.X = newX;
                    Actor.Y = newY;
                    //Message = Actor.Type.ToString() + " moved: (" + moveX + "," + moveY + ")";
                    turn = true;
                    
                }
            }
            moveX = moveY = 0;
            return turn;
        }

        bool CheckForBlock(int x, int y) {
            bool blocked = false;
            foreach (var actor in LogicEngine._actors) {
                if (actor.X == x && actor.Y == y && actor.Blocks) {
                    blocked = true;
                }
            }
            return blocked;
        }

        public static EMoveEvent ConvertCoordToMoveEvent(int dx, int dy) {
            if (dx == 1 && dy == 0) return EMoveEvent.MoveRight;
            if (dx == -1 && dy == 0) return EMoveEvent.MoveLeft;
            if (dx == 0 && dy == 1) return EMoveEvent.MoveDown;
            if (dx == 0 && dy == -1) return EMoveEvent.MoveUp;
            if (dx == -1 && dy == 1) return EMoveEvent.MoveDL;
            if (dx == 1 && dy == -1) return EMoveEvent.MoveUR;
            if (dx == -1 && dy == -1) return EMoveEvent.MoveUL;
            return EMoveEvent.MoveDR;
        }

        //This gets called right before it's pushed off the stack
        //For diagonal movement, just simply push two different Movements on there
        public override void Trigger() {
            foreach (EMoveEvent e in MovementTypes) {
                switch (e) {
                    case EMoveEvent.MoveDown:
                        moveY++;
                        break;
                    case EMoveEvent.MoveLeft:
                        moveX--;
                        break;
                    case EMoveEvent.MoveRight:
                        moveX++;
                        break;
                    case EMoveEvent.MoveUp:
                        moveY--;
                        break;
                    case EMoveEvent.MoveDL:
                        moveX--;
                        moveY++;
                        break;
                    case EMoveEvent.MoveDR:
                        moveX++;
                        moveY++;
                        break;
                    case EMoveEvent.MoveUL:
                        moveX--;
                        moveY--;
                        break;
                    case EMoveEvent.MoveUR:
                        moveX++;
                        moveY--;
                        break;
                }
                if (CheckMove) Move();
            }
            if (!CheckMove) Move();
        }
    }
}
