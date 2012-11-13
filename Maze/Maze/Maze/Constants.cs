using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    public static class Constants
    {
        public struct Maze
        {
            public const int Width = 10;
            public const int WidthPixels = Width * Tile.Width;
            public const int Height = 10;
            public const int HeightPixels = Height * Tile.Height;
        }

        public struct Tile
        {
            public const int Width = 25;
            public const int Height = 25;
        }

        // Pretty sure these two will need to be the same as the Tile width/height, or craziness will ensue.
        public struct Player
        {
            public const int Width = 25;
            public const int Height = 25;

            public const int MoveSpeed = 5;

            public const float Scale = 0.25f;
        }
    }
}
