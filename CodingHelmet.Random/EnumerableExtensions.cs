using System.Collections.Generic;
using System.Linq;

namespace CodingHelmet.Random
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ToRandomSequence<T>(this IEnumerable<T> sequence) =>
            sequence.ToArray().ToRandomSequence();

        public static IEnumerable<T> ToRandomSequence<T>(this T[] array) =>
            new RandomNumbersSequence(0, array.Length - 1).Select(index => array[index]);

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence) =>
            sequence.ToArray().ShuffleCopy();

        private static IEnumerable<T> ShuffleCopy<T>(this T[] content)
        {
            int remaining = content.Length;
            RandomNumbers generator = new RandomNumbers();

            while (remaining > 0)
            {
                generator.MoveNext(0, remaining - 1);
                yield return content[generator.Current];
                content[generator.Current] = content[remaining - 1];
                remaining -= 1;
            }
        }
    }
}
