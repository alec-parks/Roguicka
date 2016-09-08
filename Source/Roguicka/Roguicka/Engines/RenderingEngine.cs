using System.Collections.Generic;
using System.Linq;
using RLNET;
using RogueSharp;
using Roguicka.Actors;
using Roguicka.Maps;

namespace Roguicka.Engines
{

    public class RenderingEngine
    {
        private readonly RLRootConsole _rlConsole;
        private readonly IRoguickaMap _map;

        public RenderingEngine(IRoguickaMap map, RLRootConsole rootConsole)
        {
            _map = map;
            _rlConsole = rootConsole;
        }

        public void ComputeFov(Hero hero)
        {
            _map.ComputeFov(hero.X,hero.Y,hero.LightRadius,true);
        }

        public void UpdateExploredArea()
        {
            _rlConsole.Clear();
            foreach (var cell in _map.GetAllCells())
            {
                SetCell(cell);
            }
        }

        private void SetCell(Cell cell)
        {
            if (cell.IsInFov)
            {
                _map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                if (cell.IsWalkable)
                {
                    _rlConsole.Set(cell.X, cell.Y, RLColor.Gray, null, '.');
                }
                else
                {
                    _rlConsole.Set(cell.X, cell.Y, RLColor.LightGray, null, '#');
                }
            }
            else if (cell.IsExplored)
            {
                if (cell.IsWalkable)
                {
                    _rlConsole.Set(cell.X, cell.Y, new RLColor(30, 30, 30), null, '.');
                }
                else
                {
                    _rlConsole.Set(cell.X, cell.Y, RLColor.Gray, null, '#');
                }
            }
        }

        public void DrawVisiblePlayers(IEnumerable<Player> actors)
        {
            foreach (var actor in
                from actor in actors.OrderBy(actor => actor.CurrentHp)
                let cell = _map.GetCell(actor.X, actor.Y)
                where cell.IsInFov && ( actor.Type == ActorType.Hero || actor.Type == ActorType.Monster )
                select actor)
            {
                _rlConsole.Set(actor.X, actor.Y, actor.Color, null, actor.Symbol);
            }
        }

        public void DrawConsole()
        {
            _rlConsole.Draw();
        }
    }
}