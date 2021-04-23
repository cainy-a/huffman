using System.Collections.Generic;
using System.Linq;

namespace HuffmanCoding
{
	internal class PriorityQueue<T>
	{
		private readonly Dictionary<int, T> _store = new();

		public int Count => _store.Count;

		public int HighestPriority
			=> _highestPriorities.Any() ? _highestPriorities[^1] : 0;

		/*
		 * Keeps a running track of the highest priority
		 * when dequeueing, this list means we don't need to
		 * recaculate the highest priority, we simply
		 * return to the last highest priority
		 */
		private readonly List<int> _highestPriorities = new();

		public void Enqueue(int priority, T item)
		{
			_highestPriorities.Add(priority);
			_highestPriorities.Sort();

			_store.Add(priority, item);
		}

		public T Dequeue()
		{
			// get highest priority value
			var value = Peek();
			// remove it from our "queue"
			_store.Remove(HighestPriority);
			// if needed, return to the previous value for highest priority
			_highestPriorities.RemoveAt(_highestPriorities.Count - 1);
		
			return value;
		}

		public T Peek() => _store[HighestPriority];
	}
}