using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.PairsAnalysis
{
    public static class Analysis
    {
        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> collection)
        {
            T item1 = collection.First();
            foreach (var item in collection.Skip(1))
            {
                yield return Tuple.Create(item1, item);
                item1 = item;
            }
        }

        public static int MaxIndex<T>(this IEnumerable<T> collection)
        {
            return collection.Select((a, index) => (a, index)).Max().index;
        }

        public static IEnumerable<double> Process(this IEnumerable<Tuple<DateTime, DateTime>> collection)
        {
            return collection.Select(a => (a.Item2 - a.Item1).TotalSeconds);
        }

        public static IEnumerable<int> Process(this IEnumerable<Tuple<int, int>> collection)
        {
            return collection.Select(a => (a.Item2 - a.Item1));
        }

        public static int FindMaxPeriodIndex(params DateTime[] data)
        {
            return data.Pairs().Process().MaxIndex();
        }

        public static double FindAverageRelativeDifference(params double[] data)
        {
            return data.Pairs().Average(source => (source.Item2 - source.Item1) / source.Item1);
        }
    }
}
