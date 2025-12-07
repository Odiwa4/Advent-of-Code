public class Day3
{
	static void Main()
	{
		int part1Answer = 0;
		int part2Answer = 0;
		foreach (string line in File.ReadAllLines("day-4 input.txt"))
		{
			string[] commaSplit = line.Split(",");
			string[] r1Split = commaSplit[0].Split("-");
			string[] r2Split = commaSplit[1].Split("-");

			Range r1 = new Range(int.Parse(r1Split[0]), int.Parse(r1Split[1]));
			Range r2 = new Range(int.Parse(r2Split[0]), int.Parse(r2Split[1]));

			if (Contains(r1, r2) || Contains(r2, r1))
			{
				part1Answer++;
			}
			if (Overlaps(r1, r2) || Overlaps(r2, r1))
			{
				part2Answer++;
			}
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}

	static bool Overlaps(Range r1, Range r2)
	{
		return (r1.min <= r2.min && r1.max >= r2.max) || (r1.min < r2.min && r1.max >= r2.min && r1.max <= r2.max) || (r1.max >= r2.max && r1.min >= r2.min && r1.min <= r2.max);
	}

	static bool Contains(Range r1, Range r2)
	{
		return r1.min <= r2.min && r1.max >= r2.max;
	}
}

public struct Range(int min, int max)
{
	public int min = min;
	public int max = max;
}