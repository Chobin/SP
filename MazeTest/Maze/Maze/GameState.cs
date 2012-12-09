using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    class GameState
    {
        public enum MenuState
        {
            Start = 1,
            Something,
            Quit,
            Off,
        }
        public enum InState
        {
            Menu = 1,
            Started,
            Quit,
        }
        public MenuState menuState;
        public InState gameState;

    }
}
