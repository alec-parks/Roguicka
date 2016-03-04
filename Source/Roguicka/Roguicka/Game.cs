using Roguicka.Maps;

namespace Roguicka
{
  public class Game
  {
    private static Game _instance;
    public RoguickaMap Map { get; set; }
    public string FontFile { get; set; }

    private Game(){}

    public static Game Instance
    {
      get { return _instance ?? (_instance = new Game()); }
    }
  }
}