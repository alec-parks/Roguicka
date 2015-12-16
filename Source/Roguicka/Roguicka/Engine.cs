using System.Collections.Generic;
using System.Linq;
using RLNET;
using RogueSharp;
using Roguicka.Actors;

namespace Roguicka
{
    class Engine
    {
        private List<IActor> _actors = new List<IActor>(); 
        private readonly RLRootConsole _rootConsole;
        private readonly IMap _map;
        private int ScreenWidth { get; }
        private int ScreenHeight { get; }
        private string FontFile { get; }

        public Engine(int width, int height, string file, IMap map)
        {
            ScreenHeight = height;
            ScreenWidth = width;
            FontFile = file;
            _map = map;

            _rootConsole = new RLRootConsole(FontFile,ScreenWidth,ScreenHeight,8,8,1f,"RoguickaRL");
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

        public IEnumerable<IActor> GetActors()
        {
            return _actors;
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

            foreach (var cell in _map.GetAllCells())
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

            foreach (var actor in _actors.Where(actor => actor.Type == ActorType.Monster))
            {
                var cell = _map.GetCell(actor.X, actor.Y);
                if (cell.IsInFov)
                {
                    _rootConsole.Set(actor.X,actor.Y,actor.Color,null,actor.Symbol);
                }
            }
            _rootConsole.Set(player.X,player.Y,player.Color,null,player.Symbol);
            _rootConsole.Draw();

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
            }
        }

        private bool Move(IActor actor, int newX, int newY)
        {
            bool turn = false;
            if (_map.GetCell(newX, newY).IsWalkable)
            {
                if (CheckForBlock(newX, newY))
                {
                    var blocker = _actors.Find(blocked => blocked.X == newX && blocked.Y == newY);
                    blocker.TakeDamage(10);
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
            foreach (var actor in _actors.Where(actor => actor.X == x && actor.Y ==y && actor.Blocks))
            {
                blocked = true;
            }
            return blocked;
        }
    }
}
