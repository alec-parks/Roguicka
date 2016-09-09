using System.Collections.Generic;
using RogueSharp;

namespace Roguicka.Maps
{
    public interface IRoguickaMap
    {
        int Width { get; }
        int Height { get; }

        bool IsWalkable(int x, int y);

        bool IsInFov(int x, int y);

        bool IsExplored(int x, int y);

        void SetCellProperties(int x, int y, bool isTransparent, bool isWalkable, bool isExplored = false);

        void Clear(bool isTransparent = false, bool isWalkable = false);

        void ComputeFov(int xOrigin, int yOrigin, int radius, bool lightWalls);

        Cell GetCell(int x, int y);

        string ToString(bool useFov);

        IEnumerable<Cell> GetAllCells();

        IEnumerable<Cell> GetCellsInRange(int xMin, int xMax, int yMin, int yMax);

        Cell GetRandomCell();
    }
}