using System;
using System.Collections.Generic;
using System.Linq;

namespace HuffmanCoding
{
	public static class ExtMethods
	{
		public static byte ToByte(this IEnumerable<bool> source)
		{
			var arraySource = source as bool[] ?? source.ToArray();
			if (arraySource.Length > 8)
				throw new ArgumentException("More than 8 elements", nameof(source));
			
			byte result = 0;
			// This assumes the array never contains more than 8 elements!
			var index = 8 - arraySource.Length;

			// Loop through the array
			foreach (var b in arraySource)
			{
				// if the element is 'true' set the bit at that position
				if (b)
					result |= (byte)(1 << (7 - index));

				index++;
			}

			return result;
		}
		
		public static bool[] ToBools(this byte b)
		{
			// prepare the return result
			var result = new bool[8];

			// check each bit in the byte. if 1 set to true, if 0 set to false
			for (int i = 0; i < 8; i++)
				result[i] = (b & (1 << i)) != 0;

			// reverse the array
			Array.Reverse(result);

			return result;
		}
	}
}