using System.Collections;
using System.Collections.Generic;
using RogueSharp;

namespace Roguicka
{
    public class RoguickaMap :IMap
    {

        private readonly Map _map;

        public RoguickaMap()
        {
            _map = new Map();
        }

        public RoguickaMap(Map map)
        {
            _map = map;
        }

        public void Initialize(int width, int height)
        {
            _map.Initialize(width, height);
        }

        public bool IsTransparent(int x, int y)
        {
            return _map.IsTransparent(x, y);
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

        public IMap Clone()
        {
            return _map.Clone();
        }

        public void Copy(IMap sourceMap, int left = 0, int top = 0)
        {
            _map.Copy(sourceMap, left, top);
        }

        public void ComputeFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
        {
            _map.ComputeFov(xOrigin, yOrigin, radius, lightWalls);
        }

        public void AppendFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
        {
            _map.AppendFov(xOrigin, yOrigin, radius, lightWalls);
        }

        public IEnumerable GetAllCells()
        {
            return _map.GetAllCells();
        }

        public IEnumerable GetCellsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            return _map.GetCellsAlongLine(xOrigin, yOrigin, xDestination, yDestination);
        }

        public IEnumerable GetCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return _map.GetCellsInRadius(xOrigin, yOrigin, radius);
        }

        public IEnumerable GetCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return _map.GetCellsInArea(xOrigin, yOrigin, distance);
        }

        public IEnumerable GetBorderCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return _map.GetBorderCellsInRadius(xOrigin, yOrigin, radius);
        }

        public IEnumerable GetBorderCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return _map.GetBorderCellsInArea(xOrigin, yOrigin, distance);
        }

        public IEnumerable GetCellsInRows(params int[] rowNumbers)
        {
            return _map.GetCellsInRows(rowNumbers);
        }

        public IEnumerable GetCellsInColumns(params int[] columnNumbers)
        {
            return _map.GetCellsInColumns(columnNumbers);
        }

        public Cell GetCell(int x, int y)
        {
            return _map.GetCell(x, y);
        }

        public string ToString(bool useFov)
        {
            return _map.ToString(useFov);
        }

        public MapState Save()
        {
            return _map.Save();
        }

        public void Restore(MapState state)
        {
            _map.Restore(state);
        }

        public Cell CellFor(int index)
        {
            return _map.CellFor(index);
        }

        public int IndexFor(int x, int y)
        {
            return _map.IndexFor(x, y);
        }

        public int IndexFor(Cell cell)
        {
            return _map.IndexFor(cell);
        }

        IEnumerable<Cell> IMap.GetAllCells()
        {
            return _map.GetAllCells();
        }

        IEnumerable<Cell> IMap.GetCellsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            return _map.GetCellsAlongLine(xOrigin, yOrigin, xDestination, yDestination);
        }

        IEnumerable<Cell> IMap.GetCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return _map.GetCellsInRadius(xOrigin, yOrigin, radius);
        }

        IEnumerable<Cell> IMap.GetCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return _map.GetCellsInArea(xOrigin, yOrigin, distance);
        }

        IEnumerable<Cell> IMap.GetBorderCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return _map.GetBorderCellsInRadius(xOrigin, yOrigin, radius);
        }

        IEnumerable<Cell> IMap.GetBorderCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return _map.GetBorderCellsInArea(xOrigin, yOrigin, distance);
        }

        IEnumerable<Cell> IMap.GetCellsInRows(params int[] rowNumbers)
        {
            return _map.GetCellsInRows(rowNumbers);
        }

        IEnumerable<Cell> IMap.GetCellsInColumns(params int[] columnNumbers)
        {
            return _map.GetCellsInColumns(columnNumbers);
        }

        public int Width => _map.Width;
        public int Height => _map.Height;

    }
}
