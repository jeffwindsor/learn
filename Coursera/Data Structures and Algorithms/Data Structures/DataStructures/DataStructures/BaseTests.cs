﻿using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using System.Collections.Generic;

namespace DataStructures
{
    public abstract class BaseTests
    {
        const string input_ext = ".i";
        const string answer_ext = ".a";

        private static string CurrentPath()
        {
            return string.Format(@"{0}\..\..", System.AppDomain.CurrentDomain.BaseDirectory);
        }
        private static string CurrentTestPath(string location)
        {
            return string.Format(@"{0}\{1}", CurrentPath(), location);
        }
        
        protected static void TestFromRelativeFilePath(string path, Func<IList<string>, IList<string>> getActual)
        {
            TestFromFilePath(CurrentTestPath(path), getActual);
        }

        protected static void TestFromFilePath(string path, Func<IList<string>, IList<string>> getActual)
        {
            var input = File.ReadAllLines(path + input_ext);
            var expected = File.ReadAllLines(path + answer_ext);
            Console.WriteLine("[File {0}]", path);

            var actual = getActual(input);
            actual.Count.Should().Be(expected.Length);

            //Output
            for (var a = 0; a < actual.Count; a++)
            {
                Console.WriteLine("{0} : {1}", actual[a], expected[a]);
            }
            //Validate
            for (var a = 0; a < actual.Count; a++)
            {
                actual[a].Should().Be(expected[a]);
            }
        }
        
        protected static void WriteTestFiles(string name, string location, IEnumerable<string> lines, IEnumerable<string> answerLines)
        {
            var l = CurrentTestPath(location);
            File.WriteAllLines(l + @"\" + name + input_ext, lines);
            File.WriteAllLines(l + @"\" + name + answer_ext, answerLines);
        }
    }
}
