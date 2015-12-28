using RLNET;
using RogueSharp;
using Roguicka.Actors;

namespace Roguicka
{
	class MainClass
	{
        private static readonly int ScreenWidth = 50;
        private static readonly int ScreenHeight = 50;
        private static RoguickaMap _map;

        public static void Main (string[] args)
		{
            _map = new RoguickaMap(Map.Create(new CaveMapCreationStrategy<Map>(ScreenWidth,ScreenHeight,45,4,3) ));
            string FontFileName = @"C:\projects\Roguicka\Source\Roguicka\Roguicka\Fonts\terminal8x8.png";
            Engine engine = new Engine(ScreenWidth,ScreenHeight,FontFileName,_map);
            Hero player = new Hero(25,25,30,30,'@');
            Monster mob = new Monster(5,5,10,15,RLColor.Green,'T');
            engine.AddActor(player);
            engine.AddActor(mob);
            engine.Update();
            engine.Render();
            while (!engine.RootConsole().IsWindowClosed())
            {
                engine.RootConsole().Run();
            }

        }
    }
}
