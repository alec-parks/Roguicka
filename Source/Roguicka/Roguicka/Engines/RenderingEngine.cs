using System;
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
        private int _centerX;
        private int _centerY;


        public RenderingEngine(IRoguickaMap map, RLRootConsole rootConsole)
        {
            _map = map;
            _rlConsole = rootConsole;
            _centerX = rootConsole.Width / 2;
            _centerY = rootConsole.Height / 2;
        }

        public void ComputeFov(Hero hero)
        {
            _map.ComputeFov(hero.X,hero.Y,hero.LightRadius,true);
            SetCenter(hero.X,hero.Y);
        }

        public void UpdateExploredArea()
        {
            var top = ScrollY();
            var left = ScrollX();
            _rlConsole.Clear();
            foreach (var cell in _map.GetCellsInRange(left, _rlConsole.Width, top, _rlConsole.Height))
            {
                SetCell(cell);
            }
        }

        private void SetCenter(int x, int y)
        {
            _centerX = Math.Max(0, Math.Min(x, _map.Width - 1));
            _centerY = Math.Max(0, Math.Min(y, _map.Height - 1));
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

        private int ScrollX()
        {
            return Math.Max(0, Math.Min(_centerX - _rlConsole.Width / 2, _map.Width - _rlConsole.Width));
        }

        private int ScrollY()
        {
            return Math.Max(0, Math.Min(_centerY - _rlConsole.Height / 2, _map.Height - _rlConsole.Height));
        }
    }
}