using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2020
{
	public class Day3
	{
		public int Task1(string inputMap)
		{
			// init map
			var map   = new Map();
			var lines = inputMap.Split("\r\n");
			for (var i = 0; i < lines.Length; i++)
			{
				var line = lines[i];
				List<Cell> list = line.Select((c, j) => c == '#'
					                                        ? new Cell
					                                        {
						                                        Coordinate = new Vector2(i, j),
						                                        CellType   = CellTypes.Tree
					                                        }
					                                        : new Cell
					                                        {
						                                        Coordinate = new Vector2(i, j),
						                                        CellType   = CellTypes.Nothing
					                                        })
				                      .ToList();

				map.Cells.Add(list);
			}

			// travel down map
			var treesEncountered = 0;
			while (!map.AtEnd)
			{
				// three right
				map.GoRightOrWrap(); map.GoRightOrWrap(); map.GoRightOrWrap();
				// one down
				map.GoDown();
				if (map.CurrentCell.CellType == CellTypes.Tree) treesEncountered++; // keep track of trees
			}

			return treesEncountered;
		}

		public long Task2(string inputMap, IEnumerable<Vector2> rightAndDown)
		{
			// init map
			var map   = new Map();
			var lines = inputMap.Split("\r\n");
			for (var i = 0; i < lines.Length; i++)
			{
				var line = lines[i];
				List<Cell> list = line.Select((c, j) => c == '#'
					                                        ? new Cell
					                                        {
						                                        Coordinate = new Vector2(i, j),
						                                        CellType   = CellTypes.Tree
					                                        }
					                                        : new Cell
					                                        {
						                                        Coordinate = new Vector2(i, j),
						                                        CellType   = CellTypes.Nothing
					                                        })
				                      .ToList();

				map.Cells.Add(list);
			}

			var treeEncounteredList           = new List<int>();
			var rightAndDownArray = rightAndDown as Vector2[] ?? rightAndDown.ToArray();
			for (var i = 0; i < rightAndDownArray.Length; i++)
			{
				// travel down map
				var treesEncountered = 0;
				map.ResetPos();
				while (!map.AtEnd)
				{
					// go right
					for (var j = 0; j < rightAndDownArray[i].X; j++)
						map.GoRightOrWrap();
					// go down
					for (var j = 0; j < rightAndDownArray[i].Y; j++)
						map.GoDown();
					if (map.CurrentCell.CellType == CellTypes.Tree) treesEncountered++; // keep track of trees
				}
				treeEncounteredList.Add(treesEncountered);
			}

			var multiplied = (long) treeEncounteredList[0];
			for (var i = 1; i < treeEncounteredList.Count; i++)
				multiplied *= treeEncounteredList[i];

			return multiplied;
		}
	}

	public class Map
	{
		public Vector2          Coordinates { get; private set; } = Vector2.Zero;
		public List<List<Cell>> Cells       { get; set; } = new List<List<Cell>>();

		public void GoRightOrWrap()
		{
			if (Coordinates.X + 1 == Cells[Convert.ToInt32(Coordinates.Y)].Count)
				Coordinates = new Vector2(0, Coordinates.Y); // wrap
			else Coordinates = new Vector2(Coordinates.X + 1, Coordinates.Y);
		}

		public void GoDown() => Coordinates = new Vector2(Coordinates.X, Coordinates.Y + 1);

		public void ResetPos() => Coordinates = default;

		public bool AtEnd => Coordinates.Y + 1 == Cells.Count;

		public Cell CurrentCell => Cells[Convert.ToInt32(Coordinates.Y)][Convert.ToInt32(Coordinates.X)];
	}

	public class Cell
	{
		public CellTypes CellType;
		public Vector2   Coordinate;
	}

	public enum CellTypes
	{
		Tree,
		Nothing
	}
}
