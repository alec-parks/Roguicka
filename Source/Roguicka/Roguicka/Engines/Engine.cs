using System;
using System.Collections.Generic;
using System.Linq;
using RLNET;
using Roguicka.Actors;
using Roguicka.Maps;

namespace Roguicka.Engines
{
    public class Engine
    {
        private List<IActor> _actors = new List<IActor>();
        private const int ChaseTurns = 3;
        private readonly RLRootConsole _rootConsole;
        private readonly IRoguickaMap _map;
        private GameState _gameState;
        private RenderingEngine _renderingEngine;
        private LogicEngine _logicEngine;

        public Engine(int width, int height, string file, IRoguickaMap map)
        {
            _map = map;
            _rootConsole = new RLRootConsole(file,width,height,8,8,1f,"RoguickaRL");
            _gameState = GameState.PlayerTurn;
            _renderingEngine = new RenderingEngine(_map,_rootConsole);
            _logicEngine = new LogicEngine();
        }

        public RLRootConsole RootConsole()
        {
            return _rootConsole;
        }

        public void Update()
        {
            _rootConsole.Update += OnRootConsoleUpdate;
        }

        public void Render()
        {
            _rootConsole.Render += OnRootConsoleRender;
        }

        private IEnumerable<IActor> GetActors(ActorType type) 
            => _actors.Where(actor=> actor.Type == type);

        private IEnumerable<IActor> GetActors() => _actors;

        private IEnumerable<IDestructible> GetDestructible(ActorType type)
        {
            return from destructible 
                   in _actors
                   where destructible.Type == type
                   select destructible as IDestructible;
        }

        private Hero GetHero()
        {
            return (Hero) _actors.Single(actor => actor.Type == ActorType.Player);
        }

        private void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            
            var player = GetHero();
            _renderingEngine.ComputeFov(player);
            _renderingEngine.UpdateExploredArea();
            _renderingEngine.DrawVisibleActors(_actors);
            _renderingEngine.DrawConsole();
        }

        private void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            var keyPress = _rootConsole.Keyboard.GetKeyPress();
            if (keyPress == null) return;
            var player = GetHero();
            _logicEngine.Actors(_actors);
            HandleInput(player,keyPress);
        }

        private void HandleInput(IActor player, RLKeyPress keyPress)
        {
            bool newTurn = false;
            int x = 0;
            int y = 0;
            char action='z';
            switch (keyPress.Key)
            {
                case RLKey.Keypad8:
                case RLKey.Up:
                    x = player.X;
                    y = player.Y - 1;
                    action = 'm';
                    break;
                case RLKey.Keypad2:
                case RLKey.Down:
                    x = player.X;
                    y = player.Y + 1;
                    action = 'm';
                    break;
                case RLKey.Keypad4:
                case RLKey.Left:
                    x = player.X-1;
                    y = player.Y;
                    action = 'm';
                    break;
                case RLKey.Keypad6:
                case RLKey.Right:
                    x = player.X+1;
                    y = player.Y;
                    action = 'm';
                    break;
                case RLKey.Keypad7:
                    x = player.X-1;
                    y = player.Y-1;
                    action = 'm';
                    break;
                case RLKey.Keypad9:
                    x = player.X+1;
                    y = player.Y-1;
                    action = 'm';
                    break;
                case RLKey.Keypad1:
                    x = player.X-1;
                    y = player.Y+1;
                    action = 'm';
                    break;
                case RLKey.Keypad3:
                    x = player.X+1;
                    y = player.Y+1;
                    action = 'm';
                    break;
                case RLKey.Keypad5:
                    newTurn = true;
                    break;
                case RLKey.C:
                    GetHero().AddElement(new FireElement());
                    newTurn = true;
                    break;
            }
            if (action=='m')
            {
               newTurn = _logicEngine.Move(player, x, y, _map);
            }
            if (newTurn)
            {
                _gameState = GameState.NewTurn;
                MonsterMash();
            }
        }

        private void MonsterMash()
        {
            var hero = GetHero();
            
            foreach (var monster in GetDestructible(ActorType.Monster).Where(monster=>!monster.IsDead).Cast<Monster>())
            {
                _map.ComputeFov(monster.X, monster.Y, 5, true);
                if (monster.Chase > 0)
                {
                    MonsterMove(monster,hero.X,hero.Y);
                    monster.Chase--;
                }
                else if (monster.Target == null)
                {
                    monster.Target = _map.GetRandomCell();
                    //Do the Move
                    MonsterMove(monster, monster.Target.X, monster.Target.Y);
                }
                else
                {
                    MonsterMove(monster, monster.Target.X, monster.Target.Y);
                }

                if (_map.GetCell(hero.X, hero.Y).IsInFov)
                {
                    monster.Chase = ChaseTurns;
                    monster.Target = null;
                }
            }
            _gameState = GameState.PlayerTurn;
        }

        private void MonsterMove(IActor sourceActor,int targetX, int targetY)
        {
            int dx = targetX - sourceActor.X;
            int dy = targetY - sourceActor.Y;
            int stepDx = (dx > 0 ? 1 : -1);
            int stepDy = (dy > 0 ? 1 : -1);
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance > 2)
            {
                dx = (int)Math.Round(dx / distance);
                dy = (int)Math.Round(dy / distance);
                // Check for ability to move at diagonal
                if (CanMove(sourceActor.X + dx, sourceActor.Y + dy))
                {
                    _logicEngine.Move(sourceActor, sourceActor.X + dx, sourceActor.Y + dy,_map);
                }// Else, check for ability to move step DX
                else if (CanMove(sourceActor.X + stepDx, sourceActor.Y))
                {
                    _logicEngine.Move(sourceActor, sourceActor.X + stepDx, sourceActor.Y,_map);
                }// Else, check for ability to move step DY
                else if (CanMove(sourceActor.X, sourceActor.Y + stepDy))
                {
                    _logicEngine.Move(sourceActor, sourceActor.X, sourceActor.Y + stepDy,_map);
                }
            }
            else
            {
                _logicEngine.Move(sourceActor, sourceActor.X + dx, sourceActor.Y + dy,_map);
            }
        }

        private bool CanMove(int x, int y)
        {
            return _map.GetCell(x, y).IsWalkable;
        }

        

        public void AddActor(IActor actor)
        {
            _actors.Add(actor);
        }


    }
}
