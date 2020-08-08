using System;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCode
{
    public static class Extensions
    {
        public static void PrintPair<TKey, TValue>(this KeyValuePair<TKey, TValue> pair)
        {
            Console.WriteLine($"{pair.Key}, {pair.Value}");
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
