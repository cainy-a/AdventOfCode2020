using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day2
	{
		public int Part1(IEnumerable<string> input)
		{
			var validPasswords = 0;
			foreach (var item in input)
			{
				var split    = item.Split(' ');
				var amount   = split[0].Split('-');
				var letter   = split[1][0];
				var password = split[2];

				if (!int.TryParse(amount[0], out var lowerBound) || !int.TryParse(amount[1], out var upperBound))
					throw new
						DataException($"Variable {nameof(amount)} could not be parsed into {nameof(lowerBound)} and {nameof(upperBound)}");

				var letterCount = password.Count(character => character.Equals(letter));

				if (letterCount >= lowerBound && letterCount <= upperBound) validPasswords++;
			}

			return validPasswords;
		}

		public int Part2(IEnumerable<string> input)
		{
			var validPasswords = 0;
			foreach (var item in input)
			{
				var split     = item.Split(' ');
				var positions = split[0].Split('-');
				var letter    = split[1][0];
				var password  = split[2];

				if (!int.TryParse(positions[0], out var pos1) || !int.TryParse(positions[1], out var pos2))
					throw new
						DataException($"Variable {nameof(positions)} could not be parsed into {nameof(pos1)} and {nameof(pos2)}");
				pos1--; // Array indexes start at 0!
				pos2--;

				if (password[pos1] == letter && password[pos2] != letter
				 || password[pos1] != letter && password[pos2] == letter) validPasswords++;
			}

			return validPasswords;
		}
	}
}