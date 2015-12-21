using System.Collections;
using System.Collections.Generic;
using RogueSharp;

namespace Roguicka
{
    public class RoguickaMap :IMap
    {

        public IMap Map { get; set; }

        public void Initialize(int width, int height)
        {
            Map.Initialize(width, height);
        }

        public bool IsTransparent(int x, int y)
        {
            return Map.IsTransparent(x, y);
        }

        public bool IsWalkable(int x, int y)
        {
            return Map.IsWalkable(x, y);
        }

        public bool IsInFov(int x, int y)
        {
            return Map.IsInFov(x, y);
        }

        public bool IsExplored(int x, int y)
        {
            return Map.IsExplored(x, y);
        }

        public void SetCellProperties(int x, int y, bool isTransparent, bool isWalkable, bool isExplored = false)
        {
            Map.SetCellProperties(x, y, isTransparent, isWalkable, isExplored);
        }

        public void Clear(bool isTransparent = false, bool isWalkable = false)
        {
            Map.Clear(isTransparent, isWalkable);
        }

        public IMap Clone()
        {
            return Map.Clone();
        }

        public void Copy(IMap sourceMap, int left = 0, int top = 0)
        {
            Map.Copy(sourceMap, left, top);
        }

        public void ComputeFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
        {
            Map.ComputeFov(xOrigin, yOrigin, radius, lightWalls);
        }

        public void AppendFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
        {
            Map.AppendFov(xOrigin, yOrigin, radius, lightWalls);
        }

        public IEnumerable GetAllCells()
        {
            return Map.GetAllCells();
        }

        public IEnumerable GetCellsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            return Map.GetCellsAlongLine(xOrigin, yOrigin, xDestination, yDestination);
        }

        public IEnumerable GetCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return Map.GetCellsInRadius(xOrigin, yOrigin, radius);
        }

        public IEnumerable GetCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return Map.GetCellsInArea(xOrigin, yOrigin, distance);
        }

        public IEnumerable GetBorderCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return Map.GetBorderCellsInRadius(xOrigin, yOrigin, radius);
        }

        public IEnumerable GetBorderCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return Map.GetBorderCellsInArea(xOrigin, yOrigin, distance);
        }

        public IEnumerable GetCellsInRows(params int[] rowNumbers)
        {
            return Map.GetCellsInRows(rowNumbers);
        }

        public IEnumerable GetCellsInColumns(params int[] columnNumbers)
        {
            return Map.GetCellsInColumns(columnNumbers);
        }

        public Cell GetCell(int x, int y)
        {
            return Map.GetCell(x, y);
        }

        public string ToString(bool useFov)
        {
            return Map.ToString(useFov);
        }

        public MapState Save()
        {
            return Map.Save();
        }

        public void Restore(MapState state)
        {
            Map.Restore(state);
        }

        public Cell CellFor(int index)
        {
            return Map.CellFor(index);
        }

        public int IndexFor(int x, int y)
        {
            return Map.IndexFor(x, y);
        }

        public int IndexFor(Cell cell)
        {
            return Map.IndexFor(cell);
        }

        IEnumerable<Cell> IMap.GetAllCells()
        {
            return Map.GetAllCells();
        }

        IEnumerable<Cell> IMap.GetCellsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            return Map.GetCellsAlongLine(xOrigin, yOrigin, xDestination, yDestination);
        }

        IEnumerable<Cell> IMap.GetCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return Map.GetCellsInRadius(xOrigin, yOrigin, radius);
        }

        IEnumerable<Cell> IMap.GetCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return Map.GetCellsInArea(xOrigin, yOrigin, distance);
        }

        IEnumerable<Cell> IMap.GetBorderCellsInRadius(int xOrigin, int yOrigin, int radius)
        {
            return Map.GetBorderCellsInRadius(xOrigin, yOrigin, radius);
        }

        IEnumerable<Cell> IMap.GetBorderCellsInArea(int xOrigin, int yOrigin, int distance)
        {
            return Map.GetBorderCellsInArea(xOrigin, yOrigin, distance);
        }

        IEnumerable<Cell> IMap.GetCellsInRows(params int[] rowNumbers)
        {
            return Map.GetCellsInRows(rowNumbers);
        }

        IEnumerable<Cell> IMap.GetCellsInColumns(params int[] columnNumbers)
        {
            return Map.GetCellsInColumns(columnNumbers);
        }

        public int Width => Map.Width;
        public int Height => Map.Height;
    }
}
