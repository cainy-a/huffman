using System;
using System.Collections.Generic;
using System.Linq;

namespace HuffmanCoding
{
	internal static partial class Program
	{
		public static void Encode(string input,
			ref Dictionary<char, bool[]> decodingTable,
			ref TreeRoot tree)
		{
			// build priority queue
			var priorityQueue = new PriorityQueue<TreeNode>();
			// first item in tuple is count, second is priority
			var chars = new Dictionary<char, (int, int)>();

			foreach (var c in input)
				if (chars.ContainsKey(c))
					chars[c] = (chars[c].Item1 + 1, chars[c].Item2 + 1);
				else
					chars[c] = (1, 1);
			
			// conflicts :P
			while (chars.Any(p => chars.Count(o => o.Value == p.Value) > 1))
				foreach (var (c, p) in chars)
					if (chars.Count(o => o.Value.Item2 == p.Item2) > 1)
						chars[c] = (chars[c].Item1, chars[c].Item2 + 1);

			foreach (var (c, i) in chars)
				priorityQueue.Enqueue((input.Length - i.Item2) * 2,
					new TreeNode
					{ 
						Character = c,
						Count = i.Item1
					});

			// convert flat queue into tree
			while (priorityQueue.Count > 1)
			{
				var low1 = priorityQueue.Dequeue();
				var low2 = priorityQueue.Dequeue();
				var node = new TreeNode
				{
					Left = low1,
					Right = low2,
					Count = low1.Count + low2.Count
				};

				priorityQueue.Enqueue(input.Length*input.Length - (low1.Count + low2.Count), node);
			}
		
			// extract tree
			tree = (TreeRoot) priorityQueue.Dequeue();

			// build decoding table
			var route = new List<bool>();
			EncodeTraverseTree(tree, ref decodingTable, ref route);
		}

		private static void EncodeTraverseTree(TreeNode node,
			ref Dictionary<char, bool[]> decodingTable,
			ref List<bool> route)
		{
			if (node.Left == null && node.Right == null)
			{
				// ReSharper disable once PossibleInvalidOperationException
				decodingTable.Add(node.Character.Value, route.ToArray());
				return;
			}
		
			// traverse right node
			route.Add(true);
			EncodeTraverseTree(node.Right, ref decodingTable, ref route);

			// traverse left node
			route[^1] = false;
			EncodeTraverseTree(node.Left, ref decodingTable, ref route);

			// clean up route after us
			route.RemoveAt(route.Count - 1);
		}
	}
}