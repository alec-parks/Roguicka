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
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public string FontFile { get; set; }

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

        public IActor GetHero()
        {
            return _actors.Single(actor => actor.Type == ActorType.Player);
        }

        private void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            var player = GetHero();
            _rootConsole.Clear();
            _map.ComputeFov(player.X, player.Y, 50, true);

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

            foreach (var actor in _actors)
            {
                _rootConsole.Set(actor.X,actor.Y,actor.Color,null,actor.Symbol);
            }

            _rootConsole.Draw();

        }

        private void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            var player = GetHero();
            var keyPress = _rootConsole.Keyboard.GetKeyPress();
            if (keyPress == null) return;
            switch (keyPress.Key)
            {
                case RLKey.Up:
                    if (_map.GetCell(player.X, player.Y - 1).IsWalkable)
                    {
                        player.Y--;
                    }
                    break;
                case RLKey.Down:
                    if (_map.GetCell(player.X, player.Y + 1).IsWalkable)
                    {
                        player.Y++;
                    }
                    break;
                case RLKey.Left:
                    if (_map.GetCell(player.X - 1, player.Y).IsWalkable)
                    {
                        player.X--;
                    }
                    break;
                case RLKey.Right:
                    if (_map.GetCell(player.X + 1, player.Y).IsWalkable)
                    {
                        player.X++;
                    }
                    break;
            }

        }

        public void AddActor(IActor actor)
        {
            _actors.Add(actor);
        }
    }
}
