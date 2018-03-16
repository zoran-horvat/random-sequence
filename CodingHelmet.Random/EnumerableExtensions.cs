using System.Collections.Generic;
using System.Linq;

namespace CodingHelmet.Random
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ToRandomSequence<T>(this IEnumerable<T> sequence) =>
            sequence.ToArray().ToRandomSequence();

        public static IEnumerable<T> ToRandomSequence<T>(this T[] array) =>
            RandomNumbersSequence.Create(0, array.Length).Select(index => array[index]);

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence) =>
            sequence.ToArray().ShuffleCopy();

        private static IEnumerable<T> ShuffleCopy<T>(this T[] content)
        {
            int remaining = content.Length;
            RandomNumbers generator = new RandomNumbers();

            while (remaining > 0)
            {
                generator.MoveNext(0, remaining);
                yield return content[generator.Current];
                remaining -= 1;
                content[generator.Current] = content[remaining];
            }
        }
    }
}
