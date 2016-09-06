using System.Collections.Generic;
using RLNET;

namespace Roguicka.Engines
{
    public class LoggingEngine
    {
        private readonly List<string> _messages = new List<string>();
        private int messageCount = 10;

        public void WriteMessages(RLConsole rlConsole)
        {
            var lines = 0;
            for (var i = _messages.Count; lines < messageCount && i > 0; i--)
            {
                rlConsole.Print(0, lines, _messages[i-1],RLColor.Black);
                lines++;
            }
        }

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }
    }
}