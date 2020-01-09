using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random, double truePercentage = 0.50)
        {
            return random.NextDouble() < truePercentage;
        }

        public static T Next<T>(this Random random, IList<T> items) where T : class
        {
            return 0 == items.Count ? null : items[random.Next(0, items.Count - 1)];
        }

        public static IEnumerable<T> Next<T>(this Random random, IList<T> items, int howMany) where T : class
        {
            for (var count = 0; count < howMany; count++)
            {
                yield return random.Next<T>(items);
            }
        }
    }
}
