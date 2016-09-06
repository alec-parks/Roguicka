using System.Collections.Generic;
using RogueSharp;
using RogueSharp.Random;

namespace Roguicka.Maps
{
    public class RoguickaMap : IRoguickaMap
    {

        private readonly IMap _map;

        public RoguickaMap(IMap map)
        {
            _map = map;
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
            DotNetRandom dnr = new DotNetRandom();
            do
            {
                var x = dnr.Next(Width - 1);
                var y = dnr.Next(Height - 1);
                cell = _map.GetCell(x, y);
            } while (!cell.IsWalkable);
            return cell;
        }

        

        public int Width => _map.Width;
        public int Height => _map.Height;

    }
}
