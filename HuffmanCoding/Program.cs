using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuffmanCoding
{
    internal static partial class Program
    {
        private static void Main()
        {
            var inputString = Console.ReadLine();
            var decodingTable = new Dictionary<char, bool[]>();
            var huffmanTree = new TreeRoot();

            Encode(inputString, ref decodingTable, ref huffmanTree);
            Console.WriteLine($"Input String: {inputString}\n\nTree:");
            PrintTree(huffmanTree);
            Console.WriteLine("\nDecoding Table:");
            PrintDecodingTable(decodingTable);
        }

        private static void PrintDecodingTable(Dictionary<char, bool[]> decodingTable)
        {
            var maxRouteSize = 
                (from KeyValuePair<char, bool[]> pair in decodingTable
                    let route = pair.Value
                    select route.Length)
                .Max();

            var horizontalLine = "----------------";
            for (var i = 0; i < maxRouteSize; i++) horizontalLine += '-';

            Console.WriteLine($@"{horizontalLine}
| Character | {"Byte".PadRight(maxRouteSize)} |
{horizontalLine}");
            
            foreach (var (c, route) in decodingTable)
            {
                var byteStr = new StringBuilder();
                foreach (var bit in route) byteStr.Append(bit ? '1' : '0');
                
                Console.WriteLine($"| {c}         | {byteStr.ToString().PadLeft(maxRouteSize)} |");
                
                Console.WriteLine(horizontalLine);
            }
        }

        private static void PrintTree(TreeRoot root)
        {
            ((TreeNode) root).Left?.PrintTree("");
            // ReSharper disable once TailRecursiveCall
            ((TreeNode) root).Right?.PrintTree("", true);
        }

        private static void PrintTree(this TreeNode node, string indent = "", bool last = false)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└─ ");
            }
            else
            {
                Console.Write("├─ ");
                indent += "│  ";
            }
            
            Console.WriteLine(node.Character.HasValue
                ? $"{node.Character.ToString()} {node.Count}"
                : node.Count);

            node.Left?.PrintTree(indent);
            // ReSharper disable once TailRecursiveCall
            node.Right?.PrintTree(indent, true);
        }
    }
}