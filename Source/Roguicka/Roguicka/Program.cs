using System;
using RLNET;
using RogueSharp;

namespace Roguicka
{
	class MainClass
	{
        private static readonly int _screenWidth = 50;
        private static readonly int _screenHeight = 50;
        private static int _playerX = 25;
        private static int _playerY = 25;

        private static RLRootConsole _rootConsole;
        private static IMap _map;

        public static void Main (string[] args)
		{
            _map = Map.Create(new CaveMapCreationStrategy<Map>(_screenWidth,_screenHeight,45,4,3) );
            String fontFileName = @"C:\projects\Roguicka\Source\Roguicka\Roguicka\Fonts\terminal8x8.png";
            String consoleTitle = "Roguicka";
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight, 8, 8, 1f, consoleTitle);
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();

        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            var keyPress = _rootConsole.Keyboard.GetKeyPress();
            if (keyPress == null) return;
            switch (keyPress.Key)
            {
                case RLKey.Up:
                    if (_map.GetCell(_playerX, _playerY - 1).IsWalkable)
                    {
                        _playerY--;
                    }
                    break;
                case RLKey.Down:
                    if (_map.GetCell(_playerX, _playerY + 1).IsWalkable)
                    {
                        _playerY++;
                    }
                    break;
                case RLKey.Left:
                    if (_map.GetCell(_playerX -1 , _playerY).IsWalkable)
                    {
                        _playerX--;
                    }
                    break;
                case RLKey.Right:
                    if (_map.GetCell(_playerX+1, _playerY).IsWalkable)
                    {
                        _playerX++;
                    }
                    break;
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            _rootConsole.Clear();
            _map.ComputeFov(_playerX,_playerY,50,true);

            foreach (var cell in _map.GetAllCells())
            {
                if (cell.IsInFov)
                {
                    _map.SetCellProperties(cell.X,cell.Y,cell.IsTransparent,cell.IsWalkable,true);
                    if (cell.IsWalkable)
                    {
                        _rootConsole.Set(cell.X,cell.Y,RLColor.Gray,null,'.');
                    }
                    else
                    {
                        _rootConsole.Set(cell.X,cell.Y,RLColor.LightGray, null, '#');
                    }
                }
                else if (cell.IsExplored)
                {
                    if (cell.IsWalkable)
                    {
                        _rootConsole.Set(cell.X, cell.Y, new RLColor(30,30,30), null, '.');
                    }
                    else
                    {
                        _rootConsole.Set(cell.X, cell.Y, RLColor.Gray, null, '#');
                    }
                }
            }

            _rootConsole.Set(_playerX, _playerY, RLColor.White, null, '@');

            _rootConsole.Draw();
        }
    }
}
