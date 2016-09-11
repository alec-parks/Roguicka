using Roguicka.Engines;
using Roguicka.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguicka.Helpers {
    public class FloorLevelHelper {

        Dictionary<string, RoguickaMap> _levels { get; set; }

        public string CurrentLevelName { get; private set; }
        public RoguickaMap CurrentLevel { get; private set; }

        public FloorLevelHelper() {
            _levels = new Dictionary<string, RoguickaMap>();
        }

        public void SetLevel(string levelName) {
            CurrentLevel = _levels[levelName];
            Game.Instance.Map = CurrentLevel;
            RenderingEngine.SetMap(CurrentLevel);
            //Unsure how to change name
        }

        public void AddLevel(string levelName, RoguickaMap map) {
            _levels[levelName] = map;
        }

    }
}
