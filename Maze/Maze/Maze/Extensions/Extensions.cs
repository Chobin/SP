using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Maze.Extensions
{
    public static class Extensions
    {
        public static Point Inflate(this Point point, double factor)
        {
            return point.Inflate(factor, factor);
        }

        public static Point Inflate(this Point point, double xFactor, double yFactor)
        {
            return new Point((int)(point.X * xFactor), (int)(point.Y * yFactor));
        }
    }
}
