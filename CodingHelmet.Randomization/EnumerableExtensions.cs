using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingHelmet.Randomization
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
            Random generator = RandomNumbers.SeedRandom();

            while (remaining > 0)
            {
                int index = generator.Next(0, remaining);
                yield return content[index];
                remaining -= 1;
                content[index] = content[remaining];
            }
        }
    }
}
