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
        //public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> collection)
        //{
        //    return collection.Select((a, index) => Tuple.Create(collection.Skip(index + 1).First(), a)).Take(collection.Count() - 1);
        //}

        //public static IEnumerable<Tuple<T, T>> _Pairs1<T>(this IEnumerable<T> collection)
        //{
        //    T item1 = collection.First();
        //    foreach (var item in collection.Skip(1))
        //    {
        //        yield return Tuple.Create(item1, item);
        //        item1 = item;
        //    }
        //}

        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> collection)
        {
            int count = 0;
            object previus = collection.Skip(1).First();
            previus = collection.First();
            foreach (var e in collection)
            {
                if (count != 0)
                {
                    yield return Tuple.Create((T)previus, e);
                }
                previus = e;
                count++;
            }
        }

        //public static IEnumerable<Tuple<T, T>> Pairs2<T>(this IEnumerable<T> collection)
        //{
        //    var index = 0;
        //    T item1 = collection.First();
        //    foreach (var item in collection)
        //    {
        //        if(index++ != 0)
        //            yield return Tuple.Create(item1, item);
        //        item1 = item;
        //    }
        //}

        public static int MaxIndex<T>(this IEnumerable<T> collection, IComparer comparer)
        {
            var index = 0;
            var indexMax = 0;
            T max = default;
            foreach (var item in collection)
            {
                if (index == 0)
                    max = item;
                if (comparer.Compare(item, max) > 0) 
                {
                    indexMax = index;
                    max = item;
                }
                index++;
            }
            return indexMax;
        }

        public static int MaxIndex<T>(this IEnumerable<T> collection)
        {
            if (collection == Enumerable.Empty<T>())
                throw new InvalidOperationException();
            IComparer comparer = new ValueComparer<T>();
            return MaxIndex(collection, comparer);
        }

        //public static int MaxIndex2<T>(this IEnumerable<T> collection)
        //{
        //    return collection.Select((a, index) => (a, index)).Max().index;
        //}

        //public static IEnumerable<double> Process(this IEnumerable<Tuple<DateTime, DateTime>> collection)
        //{
        //    var col = new List<double>();
        //    foreach (var item in collection)
        //    {
        //        col.Add((item.Item2 - item.Item1).TotalSeconds);
        //    }
        //    return col;
        //}
        //public static IEnumerable<double> Process1(this IEnumerable<Tuple<DateTime, DateTime>> collection)
        //{
        //    return collection.Select(a => (a.Item2 - a.Item1).TotalSeconds);
        //}

        //public static IEnumerable<int> Process(this IEnumerable<Tuple<int, int>> collection)
        //{
        //    var col = new List<int>();
        //    foreach (var item in collection)
        //    {
        //        col.Add(item.Item2 - item.Item1);
        //    }
        //    return col;
        //}

        //public static IEnumerable<int> Process2(this IEnumerable<Tuple<int, int>> collection)
        //{
        //    return collection.Select(a => (a.Item2 - a.Item1));
        //}

        public static int FindMaxPeriodIndex(params DateTime[] data)
        {
            var r = data.Pairs();
            var v = r.MaxIndex();
            return v;
        }

        public static double FindAverageRelativeDifference(params double[] data)
        {
            var r = data.Pairs();
            return r.Average(source => (source.Item2 - source.Item1) / source.Item1);
        }
    }

    internal class ValueComparer<T> : IComparer
    {
        public int CompareTupleDate(Tuple<DateTime, DateTime> x, Tuple<DateTime, DateTime> y)
        {
            return (x.Item2 - x.Item1).TotalSeconds.CompareTo((y.Item2 - y.Item1).TotalSeconds);
        }
        public int CompareTupleInt(Tuple<int, int> x, Tuple<int, int> y)
        {
            return (x.Item2 - x.Item1).CompareTo(y.Item2 - y.Item1);
        }
        public int Compare(object x, object y)
        {
            Type d1 = typeof(T);
            if (d1.FullName.Contains("Tuple`2[[System.DateTime"))
            {
                var f = (Tuple<DateTime, DateTime>)x;
                var s = (Tuple<DateTime, DateTime>)y;
                return CompareTupleDate(f, s);
            }
            else if (d1.FullName.Contains("Tuple`2[[System.Int32"))
            {
                var f = (Tuple<int, int>)x;
                var s = (Tuple<int, int>)y;
                return CompareTupleInt(f, s);
            }
            else
            {
                var f = (int)x;
                var s = (int)y;
                return f.CompareTo(s);
            }
        }
    }
}
