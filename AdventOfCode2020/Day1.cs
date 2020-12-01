using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day1
	{
		public int Part1(IEnumerable<int> input)
		{
			var enumerable = input as int[] ?? input.ToArray();
			for (var i = 0; i < enumerable.Count(); i++)
				foreach (var value in enumerable)
					if (enumerable[i] != value && enumerable[i] + value == 2020)
						return enumerable[i] * value;

			throw new InvalidDataException("No data matched the criteria.");
		}

		public int Part2(IEnumerable<int> input)
		{
			var enumerable = input as int[] ?? input.ToArray();
			foreach (var i in enumerable)
			foreach (var j in enumerable)
			foreach (var k in enumerable)
				if (i != j && j != k && i != k && i + j + k == 2020)
					return i * j * k;
			throw new InvalidDataException("No data matched the criteria.");
		}
	}
}
