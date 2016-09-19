using System;
using System.Collections.Generic;
using System.Linq;
using RLNET;
using Roguicka.Actors;
using Roguicka.Interact;
using Roguicka.Maps;
using Roguicka.Helpers;
using RogueSharp;

namespace Roguicka.Engines {
    public class Engine {
        private static List<IActor> _actors = new List<IActor>();
        private const int ChaseTurns = 3;
        private readonly RLRootConsole _rootConsole;
        private GameState _gameState;
        private RenderingEngine _renderingEngine;
        private LogicEngine _logicEngine;

        public Engine(int width, int height, string file) {
            _rootConsole = new RLRootConsole(file, width, height, 8, 8, 1f, "RoguickaRL");
            _gameState = GameState.PlayerTurn;

            var levelHelper = new FloorLevelHelper();

            levelHelper.AddLevel("First", new RoguickaMap(Map.Create(new CaveMapCreationStrategy<Map>(50, 50, 45, 4, 3))));
            levelHelper.AddLevel("Second", new RoguickaMap(Map.Create(new CaveMapCreationStrategy<Map>(50, 50, 55, 4, 3))));

            levelHelper.SetLevel("First");

            Stairs stairs = new Stairs(levelHelper, 25, 26);
            AddActor(stairs);

            _renderingEngine = new RenderingEngine(levelHelper.CurrentLevel, _rootConsole);
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
            return _actors.Single(actor => actor.GetType() == typeof(Hero)) as Hero;
        }

        private void OnRootConsoleRender(object sender, UpdateEventArgs e) {

            var player = GetHero();
            _renderingEngine.ComputeFov(player);
            _renderingEngine.UpdateExploredArea();
            var _players = _actors.OfType<Player>();
            var _entities = _actors.OfType<Entity>();
            _renderingEngine.DrawVisibleEntities(_entities);
            _renderingEngine.DrawVisiblePlayers(_players);
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
            player.Stats.AddEnergy();
            //If we have enough energy, attack and reset. Same for monsters
            if (player.Stats.Stat["Energy"] > player.Stats.Stat["NeededEnergy"] && !player.IsDead) {
                if (keyPress != null) {
                    
                    HandleInput(player, keyPress);
                    //Here we can maybe check for collisions with any IObjects
                    //_logicEngine.CheckCollisionWithEntity(player);
                    
                    player.Stats.UseEnergy();
                }
            }
            else {
                if(!player.IsDead)
                MonsterMash();

            }

            InteractStack.ExecuteStack();
           
        }

        private void HandleInput(Player player, RLKeyPress keyPress) {
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
                    GetHero().AddElement(ElementType.Fire);

                    break;
                case RLKey.Space:
                    InteractStack.Push(new SpawnEvent(10));
                    break;
            }
            //_gameState = GameState.NewTurn;
            MonsterMash();
        }

        private void MonsterMash() {
            var hero = GetHero();

            foreach (var monster in GetDestructible(ActorType.Monster).Where(monster => !monster.IsDead).Cast<Monster>()) {
                ;
                monster.Stats.AddEnergy();
                if (monster.Stats.Stat["Energy"] >= monster.Stats.Stat["NeededEnergy"]) {
                    monster.Stats.UseEnergy();
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

        private void MonsterMove(Player sourceActor, int targetX, int targetY) {
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
