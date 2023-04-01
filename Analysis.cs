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

        public static int MaxIndex<T>(this IEnumerable<T> collection, Func<T, double> process)
        {
            return collection.Select((a, index) => new { a, index }).Where(b => process(b.a) ==
                collection.Select(c => process(c)).Max()).Select(d => d.index).First();
        }

        public static int FindMaxPeriodIndex(params DateTime[] data)
        {
            Func<Tuple<DateTime, DateTime>, double> process = source => (source.Item2 - source.Item1).TotalSeconds;
            var query = data.Pairs().MaxIndex(process);
            return query;
        }

        public static double FindAverageRelativeDifference(params double[] data)
        {
            Func<Tuple<double, double>, double> process = source => (source.Item2 - source.Item1) / source.Item1;
            var query = data.Pairs().Average(process);
            return query;
        }
    }
}
