﻿using CodingHelmet.Randomization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1_000_000;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int taken = RandomNumbersSequence.Create(0, 97).Take(count).Count();
            sw.Stop();

            int numbersPerSecond = (int)((long)taken * 1000 / sw.ElapsedMilliseconds);

            Console.WriteLine($"Generating {numbersPerSecond:#,##0} random numbers per second.");

            string validLetters = "bcdfghjkmnpqrstvwxyz";

            sw = new Stopwatch();
            sw.Start();

            int passwordsCount =
                RandomNumbersSequence.CreateInclusive(4, 8)
                    .Select(length => validLetters.ToRandomSequence().Take(length))
                    .Select(chars => new string(chars.ToArray()))
                    .Take(300_000)
                    .Count();
            sw.Stop();

            int passwordsPerSecond = (int)((long)passwordsCount * 1000 / sw.ElapsedMilliseconds);

            Console.WriteLine($"\nGenerating {passwordsPerSecond:#,##0} passwords per second.");

            IEnumerable<string> passwords =
                RandomNumbersSequence.CreateInclusive(4, 8)
                    .Select(length => validLetters.ToRandomSequence().Take(length))
                    .Select(chars => new string(chars.ToArray()))
                    .Take(10);

            Console.WriteLine("\nSample passwords:\n" + string.Join(Environment.NewLine, passwords.ToArray()));

            IEnumerable<int> shuffled = Enumerable.Range(1, 10).Shuffle();
            string report = string.Join(", ", shuffled.Select(k => k.ToString()).ToArray());
            Console.WriteLine($"\nShuffled numbers: {report}");

            Console.WriteLine("\nPress ENTER to exit. . . ");
            Console.ReadLine();
        }
    }
}
