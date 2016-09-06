using System;
using System.Collections.Generic;
using System.Linq;
using RLNET;
using Roguicka.Actors;
using Roguicka.Interact;

namespace Roguicka.Engines {
    public class Engine {
        private static List<IActor> _actors = new List<IActor>();
        private const int ChaseTurns = 3;
        private readonly RLRootConsole _rootConsole;
        private GameState _gameState;
        private RenderingEngine _renderingEngine;
        private LogicEngine _logicEngine;

        private bool _leftPressed;
        private bool _leftReleased;
        public Engine(int width, int height, string file) {
            _rootConsole = new RLRootConsole(file, width, height, 8, 8, 1f, "RoguickaRL");
            _gameState = GameState.PlayerTurn;
            _renderingEngine = new RenderingEngine(Game.Instance.Map, _rootConsole);
            _logicEngine = new LogicEngine();

        }

        public RLRootConsole RootConsole() {
            return _rootConsole;
        }

        public void Update() {
            _rootConsole.Update += OnRootConsoleUpdate;
        }

        public void Render() {
            _rootConsole.Render += OnRootConsoleRender;
        }

        private IEnumerable<IActor> GetActors(ActorType type)
            => _actors.Where(actor => actor.Type == type);

        private IEnumerable<IActor> GetActors() => _actors;

        private IEnumerable<IDestructible> GetDestructible(ActorType type) {
            return from destructible
                   in _actors
                   where destructible.Type == type
                   select destructible as IDestructible;
        }

        public static Hero GetHero() {
            return (Hero)_actors.Single(actor => actor.Type == ActorType.Player);
        }

        private void OnRootConsoleRender(object sender, UpdateEventArgs e) {

            var player = GetHero();
            _renderingEngine.ComputeFov(player);
            _renderingEngine.UpdateExploredArea();
            _renderingEngine.DrawVisibleActors(_actors);
            _renderingEngine.DrawConsole();
        }

        private void OnRootConsoleUpdate(object sender, UpdateEventArgs e) {
            var keyPress = _rootConsole.Keyboard.GetKeyPress();

            

            //Get info at event
            if (_rootConsole.Mouse.GetLeftClick()) {
                int x = _rootConsole.Mouse.X;
                int y = _rootConsole.Mouse.Y;
                //IActor actor = GetActorAt(x, y);
                //Console.WriteLine(actor.Description);
            }
            
            var player = GetHero();
            _logicEngine.Actors(_actors);
            //Add energy to hero
            player.Stats.Energy += player.Stats.EnergyGain;
            //If we have enough energy, attack and reset. Same for monsters
            if (player.Stats.Energy >= player.Stats.NeededEnergy) {
                if (keyPress != null) {
                    HandleInput(player, keyPress);
                    player.Stats.Energy = 0;
                }
            }
            else {
                MonsterMash();

            }

            InteractStack.ExecuteStack();
        }

        private void HandleInput(IActor player, RLKeyPress keyPress) {
            switch (keyPress.Key) {
                case RLKey.Keypad8:
                case RLKey.Up:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveUp));
                    break;
                case RLKey.Keypad2:
                case RLKey.Down:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveDown));
                    break;
                case RLKey.Keypad4:
                case RLKey.Left:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveLeft));
                    break;
                case RLKey.Keypad6:
                case RLKey.Right:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveRight));
                    break;
                case RLKey.Keypad7:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveUL));
                    break;
                case RLKey.Keypad9:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveUR));
                    break;
                case RLKey.Keypad1:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveDL));
                    break;
                case RLKey.Keypad3:
                    InteractStack.Push(new MoveEvent(player, Game.Instance.Map, false, EMoveEvent.MoveDR));
                    break;
                case RLKey.Keypad5:

                    break;
                case RLKey.C:
                    GetHero().AddElement(new FireElement());

                    break;
                case RLKey.Space:
                    InteractStack.Push(new SpawnEvent(10));
                    break;
            }
            //_gameState = GameState.NewTurn;
            //MonsterMash();
        }

        private void MonsterMash() {
            var hero = GetHero();

            foreach (var monster in GetDestructible(ActorType.Monster).Where(monster => !monster.IsDead).Cast<Monster>()) {
                ;
                monster.Stats.Energy += monster.Stats.EnergyGain;
                if (monster.Stats.Energy >= monster.Stats.NeededEnergy) {
                    monster.Stats.Energy = 0;
                    Game.Instance.Map.ComputeFov(monster.X, monster.Y, 5, true);
                    if (monster.Chase > 0) {
                        MonsterMove(monster, hero.X, hero.Y);
                        monster.Chase--;
                    }
                    else if (monster.Target == null) {
                        monster.Target = Game.Instance.Map.GetRandomCell();
                        //Do the Move
                        MonsterMove(monster, monster.Target.X, monster.Target.Y);
                    }
                    else {
                        MonsterMove(monster, monster.Target.X, monster.Target.Y);
                    }

                    if (Game.Instance.Map.GetCell(hero.X, hero.Y).IsInFov) {
                        monster.Chase = ChaseTurns;
                        monster.Target = null;
                    }
                }
            }
            _gameState = GameState.PlayerTurn;
        }

        private void MonsterMove(IActor sourceActor, int targetX, int targetY) {
            int dx = targetX - sourceActor.X;
            int dy = targetY - sourceActor.Y;
            int stepDx = (dx > 0 ? 1 : -1);
            int stepDy = (dy > 0 ? 1 : -1);
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance > 2) {
                dx = (int)Math.Round(dx / distance);
                dy = (int)Math.Round(dy / distance);
                // Check for ability to move at diagonal
                if (CanMove(sourceActor.X + dx, sourceActor.Y + dy)) {
                    InteractStack.Push(new MoveEvent(sourceActor, Game.Instance.Map, false, MoveEvent.ConvertCoordToMoveEvent(dx, dy)));
                }// Else, check for ability to move step DX
                else if (CanMove(sourceActor.X + stepDx, sourceActor.Y)) {
                    InteractStack.Push(new MoveEvent(sourceActor, Game.Instance.Map, false, MoveEvent.ConvertCoordToMoveEvent(dx, 0)));

                }// Else, check for ability to move step DY
                else if (CanMove(sourceActor.X, sourceActor.Y + stepDy)) {
                    InteractStack.Push(new MoveEvent(sourceActor, Game.Instance.Map, false, MoveEvent.ConvertCoordToMoveEvent(0, dy)));

                }
            }
            else {
                InteractStack.Push(new MoveEvent(sourceActor, Game.Instance.Map, false, MoveEvent.ConvertCoordToMoveEvent(dx, dy)));
            }
        }

        private bool CanMove(int x, int y) {
            return Game.Instance.Map.GetCell(x, y).IsWalkable;
        }

        public static IActor GetActorAt(int x, int y) {
            return _actors.Single(a => a.X == x && a.Y == y);
        }


        public static void AddActor(IActor actor) {
            _actors.Add(actor);
        }


    }
}
