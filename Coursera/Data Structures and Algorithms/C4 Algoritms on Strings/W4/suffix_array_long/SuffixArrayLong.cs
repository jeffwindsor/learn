using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnStrings.W4
{
    public class SuffixArrayLong
    {
        public static void Main(string[] args)
        {
            string s;
            var inputs = new List<string>();
            while ((s = Console.ReadLine()) != null)
                inputs.Add(s);

            foreach (var result in Answer(inputs.ToArray()))
                Console.WriteLine(result);
        }

        public static IList<string> Answer(IList<string> inputs)
        {
            var text = inputs[0];
            var orders = SuffixArray.BuildSuffixArray(text, SuffixArray.NucleotideAlphabet);

            //Spaced Values
            var answer = string.Join(" ", orders.Select(i => i.ToString()));
            return (string.IsNullOrEmpty(answer))
                ? Enumerable.Empty<string>().ToArray()
                : new[] { answer };
        }
    }
    public class SuffixArray
    {
        public const string NucleotideAlphabet = "$ACGT";

        public string Text { get; private set; }
        public int[] Order { get; private set; }

        public SuffixArray(string source, string alphabet = NucleotideAlphabet)
        {
            Text = source;
            Order = BuildSuffixArray(source, alphabet);
        }

        public static int[] BuildSuffixArray(string source, string alphabet)
        {
            var order = SortCharacters(source, alphabet);
            var classes = ComputeCharClasses(source, order);
            var l = 1;
            while (l < source.Length)
            {
                order = SortDoubled(source, l, order, classes);
                classes = UpdateClasses(order, classes, l);
                l = 2 * l;
            }
            return order;
        }

        public static int[] UpdateClasses(int[] order, int[] classes, int l)
        {
            var n = order.Length;
            var newClasses = new int[n];
            for (var i = 1; i < n; i++)
            {
                var cur = order[i];
                var prev = order[i - 1];
                var mid = cur + l;
                var midPrev = (prev + l)%n;
                var x = (classes[cur] != classes[prev] || classes[mid] != classes[midPrev]) ? 1 : 0;
                newClasses[cur] = newClasses[prev] + x;
            }
            return newClasses;
        }

        public static int[] SortDoubled(string source, int l, int[] order, int[] classes)
        {
            var sourceLength = source.Length;
            var count = new int[sourceLength];
            var newOrder = new int[sourceLength];
            for (var i = 0; i < sourceLength; i++)
            {
                count[classes[i]] = count[classes[i]] + 1;
            }
            for (var i = 1; i < sourceLength; i++)
            {
                count[i] = count[i] + count[i - 1];
            }
            for (var n = 1; n <= sourceLength; n++)
            {
                var i = sourceLength - n;
                var start = (order[i] - l + sourceLength) %sourceLength;
                var cl = classes[start];
                count[cl] = count[cl] - 1;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        public static int[] ComputeCharClasses(string source, int[] order)
        {
            var sourceLength = source.Length;
            var classes = new int[sourceLength];
            for (var i = 1; i < sourceLength; i++)
            {
                var x = (source[order[i]] != source[order[i - 1]]) ? 1 : 0;
                classes[order[i]] = classes[order[i - 1]] + x;
            }
            return classes;
        }

        public static int[] SortCharacters(string source, string alphabet)
        {
            var sourceLength = source.Length;
            var alphabetLength = alphabet.Length;
            var alphaToIndex = alphabet
                //Sort Just in Case
                .OrderBy(c => c)
                //Make Dictionary of char to index
                .Select((c, i) => new {c, i})
                .ToDictionary(o => o.c, o => o.i);

            var order = new int[sourceLength];
            var count = new int[alphabetLength];

            //Set Counts for Alphabet Chars in String
            foreach(var c in source)
            {
                var ai = alphaToIndex[c];
                count[ai] = count[ai] + 1;
            }
            //Makes count = first position of next alphabet char (in the sorted text)
            for (var i = 1; i < alphabetLength; i++)
            {
                count[i] = count[i] + count[i - 1];
            }

            for (var i = sourceLength - 1; i >= 0; i--)
            {
                var c = source[i];
                var ai = alphaToIndex[c];
                var acount = count[ai] - 1;

                count[ai] = acount;
                order[acount] = i;
            }
            return order;
        }
    }
}