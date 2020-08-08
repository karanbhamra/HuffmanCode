using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HuffmanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = "this is some text to be compressed";
            string input = "this is some text to be compressed";
            var frequency = GetFrequenciesFromString(input);

            var heap = BuildHeapFromFrequencies(frequency);
            var treeRoot = BuildTreeFromHeap(heap);
            var mapping = BuildMappingFromTree(treeRoot);

            string output = HuffmanToString(input, mapping);

            Console.WriteLine($"{input} = {output}");
        }

        private static string HuffmanToString(string input, Dictionary<char, string> mapping)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var letter in input)
            {
                sb.Append(mapping[letter]);
                sb.Append(' ');
            }

            return sb.ToString();
        }

        private static Dictionary<char, string> BuildMappingFromTree(HuffmanNode treeRoot)
        {
            Dictionary<char, string> mapping = new Dictionary<char, string>();

            TraverseTree(treeRoot, "");

            void TraverseTree(HuffmanNode node, string path)
            {
                if (node == null)
                {
                    return;
                }
                // print the leaf if the left and right are null
                if (node.Left == null && node.Right == null)// && node.Char != '\0')
                {
                    mapping[node.Char] = path;
                }

                TraverseTree(node.Left, path + "0");
                TraverseTree(node.Right, path + "1");
            }

            return mapping;
        }

        private static HuffmanNode BuildTreeFromHeap(MinHeap<HuffmanNode> heap)
        {
            HuffmanNode root = null;

            while (heap.Count > 1)
            {
                var first = heap.Pop();
                var second = heap.Pop();

                int sum = first.Frequency + second.Frequency;

                HuffmanNode node = new HuffmanNode('\0', sum);
                node.Left = first;

                node.Right = second;

                root = node;

                heap.Add(node);
            }

            return root;
        }

        private static Dictionary<char, int> GetFrequenciesFromString(string input)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();

            foreach (var letter in input)
            {
                if (!frequency.ContainsKey(letter))
                {
                    frequency[letter] = 0;
                }

                frequency[letter]++;
            }

            return frequency;
        }

        static MinHeap<HuffmanNode> BuildHeapFromFrequencies(Dictionary<char, int> frequency)
        {
            var comparer = Comparer<HuffmanNode>.Create((fnode, snode) => fnode.Frequency.CompareTo(snode.Frequency));
            var minHeap = new MinHeap<HuffmanNode>(comparer);

            minHeap.AddRange(frequency.Select(kvp => new HuffmanNode(kvp.Key, kvp.Value)));

            return minHeap;
        }
    }
}
