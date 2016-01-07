using System;
using System.Collections.Generic;
using System.Linq;
using RLNET;
using RogueSharp;
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
        private int ScreenWidth { get; }
        private int ScreenHeight { get; }
        private string FontFile { get; }
        private GameState _gameState;

        public Engine(int width, int height, string file, IRoguickaMap map)
        {
            ScreenHeight = height;
            ScreenWidth = width;
            FontFile = file;
            _map = map;

            _rootConsole = new RLRootConsole(FontFile,ScreenWidth,ScreenHeight,8,8,1f,"RoguickaRL");
            _gameState = GameState.PlayerTurn;
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

        private IEnumerable<IActor> GetActors(ActorType type) => _actors.Where(actor=> actor.Type == type);

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
            _rootConsole.Clear();
            _map.ComputeFov(player.X, player.Y, player.LightRadius, true);

            foreach (Cell cell in _map.GetAllCells())
            {
                SetCell(cell);
            }

            foreach (var monster in 
                from monster in GetActors(ActorType.Monster)
                let cell = _map.GetCell(monster.X, monster.Y)
                where cell.IsInFov select monster)
            {
                _rootConsole.Set(monster.X, monster.Y, monster.Color, null, monster.Symbol);
            }

            _rootConsole.Set(player.X,player.Y,player.Color,null,player.Symbol);
            _rootConsole.Draw();

        }

        private void SetCell(Cell cell)
        {
            if (cell.IsInFov)
            {
                _map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                if (cell.IsWalkable)
                {
                    _rootConsole.Set(cell.X, cell.Y, RLColor.Gray, null, '.');
                }
                else
                {
                    _rootConsole.Set(cell.X, cell.Y, RLColor.LightGray, null, '#');
                }
            }
            else if (cell.IsExplored)
            {
                if (cell.IsWalkable)
                {
                    _rootConsole.Set(cell.X, cell.Y, new RLColor(30, 30, 30), null, '.');
                }
                else
                {
                    _rootConsole.Set(cell.X, cell.Y, RLColor.Gray, null, '#');
                }
            }
        }

        private void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            var player = GetHero();
            var keyPress = _rootConsole.Keyboard.GetKeyPress();
            if (keyPress == null) return;
            HandleInput(player,keyPress);
        }

        private void HandleInput(IActor player, RLKeyPress keyPress)
        {
            bool newTurn = false;
            int x;
            int y;
            switch (keyPress.Key)
            {
                case RLKey.Keypad8:
                case RLKey.Up:
                    x = player.X;
                    y = player.Y - 1;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad2:
                case RLKey.Down:
                    x = player.X;
                    y = player.Y + 1;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad4:
                case RLKey.Left:
                    x = player.X-1;
                    y = player.Y;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad6:
                case RLKey.Right:
                    x = player.X+1;
                    y = player.Y;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad7:
                    x = player.X-1;
                    y = player.Y-1;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad9:
                    x = player.X+1;
                    y = player.Y-1;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad1:
                    x = player.X-1;
                    y = player.Y+1;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad3:
                    x = player.X+1;
                    y = player.Y+1;
                    newTurn = Move(player, x, y);
                    break;
                case RLKey.Keypad5:
                    newTurn = true;
                    break;
                case RLKey.C:
                    GetHero().AddElement(new FireElement());
                    newTurn = true;
                    break;
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
                    Move(sourceActor, sourceActor.X + dx, sourceActor.Y + dy);
                }// Else, check for ability to move step DX
                else if (CanMove(sourceActor.X + stepDx, sourceActor.Y))
                {
                    Move(sourceActor, sourceActor.X + stepDx, sourceActor.Y);
                }// Else, check for ability to move step DY
                else if (CanMove(sourceActor.X, sourceActor.Y + stepDy))
                {
                    Move(sourceActor, sourceActor.X, sourceActor.Y + stepDy);
                }
            }
            else
            {
                Move(sourceActor, sourceActor.X + dx, sourceActor.Y + dy);
            }
        }

        private bool CanMove(int x, int y)
        {
            return _map.GetCell(x, y).IsWalkable;
        }

        private bool Move(IActor actor, int newX, int newY)
        {
            bool turn = false;
            if (_map.GetCell(newX, newY).IsWalkable)
            {
                if (CheckForBlock(newX, newY))
                {
                    var blocker = _actors.Find(blocked => blocked.X == newX && blocked.Y == newY) as IDestructible;
                    blocker?.TakeDamage(10);
                    turn = true;
                }
                else
                {
                    actor.X = newX;
                    actor.Y = newY;
                    turn = true;
                }
            }
            return turn;
        }

        public void AddActor(IActor actor)
        {
            _actors.Add(actor);
        }

        private bool CheckForBlock(int x, int y)
        {
            bool blocked = false;
            foreach (var actor in GetActors())
            {
                if (actor.X == x && actor.Y == y && actor.Blocks)
                {
                    blocked = true;
                }
            }
            
            return blocked;
        }
    }
}
