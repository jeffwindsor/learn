﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W4
{
    public class ShortestPaths
    {
        //public static void Main(string[] args)
        //{
        //    string s;
        //    var inputs = new List<string>();
        //    while ((s = Console.ReadLine()) != null)
        //        inputs.Add(s);

        //    foreach (var result in Answer(inputs.ToArray()))
        //        Console.WriteLine(result);
        //}

        public static IList<string> Answer(IList<string> inputs)
        {
            var gis = new AdjacencyListGraphInput(inputs);
            var g = gis.ToEdges();
            var start = gis.NextAsInt();


            var path = ShortestPath(start, g.Item1, g.Item2.ToList());

            return path.Distance.Values
                .Select(v =>
                {
                    switch (v)
                    {
                        case NegativeInfinity:
                            return "-";
                        case PositiveInfinity:
                            return "*";
                        default:
                            return v.ToString();
                    }
                })
                .ToArray();
        }

        private const int PositiveInfinity = int.MaxValue;
        private const int NegativeInfinity = int.MinValue;
        
        private static BellmanFordResult ShortestPath(int start, int size, List<Edge> edges)
        {
            var g = new AdjacencyListGraph(size);
            foreach (var edge in edges)
            {
                g.AddDirectedEdge(edge);
            }

            var result = BellmanFord(start, size, edges);
            var negative = edges
                .Where(e => ShouldRelax(e, result).Item1)
                .Select(e => e.Right)
                .ToList();
            
            foreach (var i in BreathFirstSearch(negative, g))
            {
                result.Distance.SetValue(i,NegativeInfinity);
            }
            return result;
        }
        
        private static BellmanFordResult BellmanFord(int start, int size, List<Edge> edges)
        {
            var result = new BellmanFordResult(size);
            result.Distance.SetValue(start,0);

            //Console.WriteLine("Initial: {0}",result.Distance);
            IEnumerable<Edge> workingEdges = edges;
            for (var i = 0; i < size; i++)
            {
                foreach (var edge in edges) { Relax(edge, result); }
                //Console.WriteLine("{1}: {0}", result.Distance,i);
            }
            return result;
        }

        private static bool Relax(Edge e, BellmanFordResult r)
        {
            return Relax(e.Left, e.Right, e.Weight, r);
        }

        private static bool Relax(int left, int right, int weight, BellmanFordResult r)
        {
            var should = ShouldRelax(left, right, weight, r);

            if (should.Item1)
            {
                r.Distance.SetValue(right, should.Item2);
                r.VisitedFrom.SetValue(right, left);
            }
            return should.Item1;
        }

        private static Tuple<bool, int> ShouldRelax(Edge e, BellmanFordResult r)
        {
            return ShouldRelax(e.Left, e.Right, e.Weight, r);
        }
        private static Tuple<bool, int> ShouldRelax(int left, int right, int weight, BellmanFordResult r)
        {
            var leftDistance = r.Distance.GetValue(left);
            var relaxedDistance = leftDistance == PositiveInfinity ? leftDistance : leftDistance + weight;
            var currentDistance = r.Distance.GetValue(right);

            return new Tuple<bool, int>(currentDistance > relaxedDistance, relaxedDistance);
        }

        private class BellmanFordResult
        {
            public BellmanFordResult(int size)
            {
                VisitedFrom = new SearchData<int>(size, -1);
                Distance = new SearchData<int>(size, PositiveInfinity);
            }
            public SearchData<int> Distance { get; private set; }
            public SearchData<int> VisitedFrom { get; private set; }
        }
        
        public static IEnumerable<int> BreathFirstSearch(IEnumerable<int> starts, AdjacencyListGraph graph)
        {
            var visited = new SearchData<bool>(graph.Size(), false);
            var queue = new Queue<int>();
            foreach (var start in starts)
            {
                visited.SetValue(start, false);
                queue.Enqueue(start);
            }

            while (queue.Any())
            {
                var current = queue.Dequeue();
                foreach (var neighbor in graph.NeighborIndexes(current).Where(i => i != current))
                {
                    if (visited.Visited(neighbor)) continue;

                    queue.Enqueue(neighbor);
                    visited.SetValue(neighbor, true);
                }
            }

            var visitedIndexes = visited.Values
                .Select((v, i) => new {Visited = v, Index = i})
                .Where(x => x.Visited)
                .Select(x => x.Index);
            return visitedIndexes;
        }
    }
    
}
