using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    public static class LINQExtensions
    {
        public static void ForEach<T>(this T[,] enumerable, Action<T> action)
        {
            enumerable.OfType<T>().ForEach(action);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
