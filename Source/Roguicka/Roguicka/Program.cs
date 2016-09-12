﻿using RogueSharp;
using Roguicka.Actors;
using Roguicka.Engines;
using Roguicka.Helpers;
using Roguicka.Interact;
using Roguicka.Maps;
using static System.IO.Path;

namespace Roguicka
{
	public class MainClass
	{
        private static readonly int ScreenWidth = 50;
        private static readonly int ScreenHeight = 50;
	    private static readonly char DirSeparator = DirectorySeparatorChar;
        private static RoguickaMap _map;

        public static void Main (string[] args)
		{
            _map = new RoguickaMap(Map.Create(new CaveMapCreationStrategy<Map>(ScreenWidth,ScreenHeight,45,4,3) ));
            Game.Instance.Map = _map;
			string FontFileName = "Assets" + DirSeparator +
			                      "Fonts"+ DirSeparator + "terminal8x8.png";
            Engine engine = new Engine(ScreenWidth,ScreenHeight,FontFileName);
		    Hero player = new Hero(25, 25, 50, '@') {Stats = new Stats(10, 10, 10, 8, 0, 0, 1)};
		    Engine.AddActor(player);
           
            MonsterGenerator.LoadMonstersFromJSON("Assets"+ DirSeparator +
                                                  "Bestiary" + DirSeparator + "Monsters");
            InteractStack.Push(new SpawnEvent(10));
            engine.Update();
            engine.Render();
            while (!engine.RootConsole().IsWindowClosed())
            {
                engine.RootConsole().Run();
            }

        }
    }
}
