using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HuffmanCode
{
    [DebuggerDisplay("Char: {Char}, Freq: {Frequency}")]
    class HuffmanNode
    {
        public char Char { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public HuffmanNode(char c, int freq)
        {
            Char = c;
            Frequency = freq;
        }
    }
}
