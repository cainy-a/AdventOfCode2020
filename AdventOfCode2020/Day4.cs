using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day4
	{
		public int Task1(IEnumerable<string> passports)
		{
			var validPassports = 0;
			foreach (var passport in passports)
			{
				// parse passport
				var fields = passport.Split(new[] {" ", Environment.NewLine}, StringSplitOptions.TrimEntries);
				var parsedFields = fields.Select(f =>
				{
					var field = new Field {Value = f.Split(':')[0]};
					if (!Enum.TryParse(f.Split(':')[0], out field.FieldType))
						throw new DataException($"Field {f} does not match any valid types.");
					return field;
				}).ToArray();

				// validate passport
				if (parsedFields.Any(f => f.FieldType == PassportFields.byr) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.ecl) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.eyr) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.hcl) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.hgt) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.iyr) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.pid))
					validPassports++;
			}

			return validPassports;
		}

		public int Task2(IEnumerable<string> passports)
		{
			var validPassports = 0;
			foreach (var passport in passports)
			{
				// parse passport
				var fields = passport.Split(new[] {" ", Environment.NewLine}, StringSplitOptions.TrimEntries);
				var parsedFields = fields.Select(f =>
				{
					var field = new Field {Value = f.Split(':')[1]};
					if (!Enum.TryParse(f.Split(':')[0], out field.FieldType))
						throw new DataException($"Field {f} does not match any valid types.");
					return field;
				}).ToArray();

				// validate passport
				var valid = parsedFields.Any(f => f.FieldType == PassportFields.byr) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.ecl) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.eyr) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.hcl) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.hgt) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.iyr) &&
				            parsedFields.Any(f => f.FieldType == PassportFields.pid);
				var byr      = int.Parse(parsedFields.Where(f => f.FieldType == PassportFields.byr).DefaultIfEmpty(new Field{Value = "0"}).First().Value);
				var iyr      = int.Parse(parsedFields.Where(f => f.FieldType == PassportFields.iyr).DefaultIfEmpty(new Field{Value = "0"}).First().Value);
				var eyr      = int.Parse(parsedFields.Where(f => f.FieldType == PassportFields.eyr).DefaultIfEmpty(new Field{Value = "0"}).First().Value);
				var hgt      = parsedFields.Where(f => f.FieldType == PassportFields.hgt).DefaultIfEmpty(new Field{Value = "0"}).First().Value;
				if (hgt.Length <= 3) continue;
				var hgtUnit  = hgt.Substring(hgt.Length - 2, 2);
				if (!hgtUnit.All(char.IsLetter)) continue;
				var hgtValue = int.Parse(hgt.Substring(0, hgt.Length - 2));
				var hcl      = parsedFields.Where(f => f.FieldType == PassportFields.hcl).DefaultIfEmpty(new Field{Value = "0"}).First().Value;
				var ecl      = parsedFields.Where(f => f.FieldType == PassportFields.ecl).DefaultIfEmpty(new Field{Value = "0"}).First().Value;
				var pid      = parsedFields.Where(f => f.FieldType == PassportFields.pid).DefaultIfEmpty(new Field{Value = "0"}).First().Value;

				if (byr.ToString().Length != 4 || byr < 1920 || byr > 2002) continue;
				if (iyr.ToString().Length != 4 || iyr < 2010 || iyr > 2020) continue;
				if (eyr.ToString().Length != 4 || eyr < 2020 || eyr > 2030) continue;
				if ((hgtUnit != "cm" || hgtValue < 150 || hgtValue > 193) && (hgtUnit != "in" || hgtValue < 59) &&
				    hgtValue > 59) continue;
				if (hcl[0] != '#' || hcl.Length != 7 || !hcl[Range.StartAt(1)]
					   .All(c => new[]
							        {'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'}
						       .Contains(c))) continue;
				if (!new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(ecl)) continue;
				if (pid.Length != 9 && !int.TryParse(pid, out _)) continue;

				if (valid) validPassports++;
			}

			return validPassports;
		}
	}

	public class Field
	{
		public PassportFields FieldType;
		public string         Value;
	}

	public enum PassportFields
	{
		// ReSharper disable InconsistentNaming
		byr,
		iyr,
		eyr,
		hgt,
		hcl,
		ecl,
		pid,
		cid
		// ReSharper restore InconsistentNaming
	}
}