using System.Collections.Generic;
using System.Linq;

namespace RandomSequence
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ToRandomSequence<T>(this IEnumerable<T> sequence) =>
            sequence.ToArray().ToRandomSequence();

        public static IEnumerable<T> ToRandomSequence<T>(this T[] array) =>
            new RandomNumbersSequence(0, array.Length - 1).Select(index => array[index]);
    }
}
