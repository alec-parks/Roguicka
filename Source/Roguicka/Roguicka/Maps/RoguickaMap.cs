using System.Collections.Generic;
using RogueSharp;
using RogueSharp.Random;
using System;

namespace Roguicka.Maps
{
    public class RoguickaMap : IRoguickaMap
    {

        private readonly IMap _map;
        private DotNetRandom _random;

        public RoguickaMap(IMap map)
        {
            _map = map;
            _random = new DotNetRandom();
        }

        public bool IsWalkable(int x, int y)
        {
            return _map.IsWalkable(x, y);
        }

        public bool IsInFov(int x, int y)
        {
            return _map.IsInFov(x, y);
        }

        public bool IsExplored(int x, int y)
        {
            return _map.IsExplored(x, y);
        }

        public void SetCellProperties(int x, int y, bool isTransparent, bool isWalkable, bool isExplored = false)
        {
            _map.SetCellProperties(x, y, isTransparent, isWalkable, isExplored);
        }

        public void Clear(bool isTransparent = false, bool isWalkable = false)
        {
            _map.Clear(isTransparent, isWalkable);
        }

        public void ComputeFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
        {
            _map.ComputeFov(xOrigin, yOrigin, radius, lightWalls);
        }

        public Cell GetCell(int x, int y)
        {
            return _map.GetCell(x, y);
        }

        public string ToString(bool useFov)
        {
            return _map.ToString(useFov);
        }

        IEnumerable<Cell> IRoguickaMap.GetAllCells()
        {
            return _map.GetAllCells();
        }

        public Cell GetRandomCell()
        {
            Cell cell;
            do
            {
                var x = _random.Next(Width - 1);
                var y = _random.Next(Height - 1);
                cell = _map.GetCell(x, y);
            } while (!cell.IsWalkable);
            return cell;
        }

       

        public Tuple<int,int> GetFreeRandomCoord() {
            int x = _random.Next(Game.Instance.Map.Width - 1);
            int y = _random.Next(Game.Instance.Map.Height - 1);
            while (!Game.Instance.Map.IsWalkable(x, y)) {
                x = _random.Next(Game.Instance.Map.Width - 1);
                y = _random.Next(Game.Instance.Map.Height - 1);
            }
            return new Tuple<int, int>(x, y);
        }

        public int Width => _map.Width;
        public int Height => _map.Height;

    }
}
