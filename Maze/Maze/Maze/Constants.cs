using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    public static class Constants
    {
        public const int TileWidth = 25;
        public const int TileHeight = 25;

        // Pretty sure these two will need to be the same as the Tile width/height, or craziness will ensue.
        public const int PlayerWidth = 25;
        public const int PlayerHeight = 25;

        public const float PlayerScale = 0.25f;
    }
}
