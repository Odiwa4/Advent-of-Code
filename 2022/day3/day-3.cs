public class Day3
{
	static void Main()
	{
		int part1Answer = 0;
		int part2Answer = 0;
		string[] lines = File.ReadAllLines("day-3 input.txt");

		Dictionary<char, int> groupDict = new Dictionary<char, int>();
		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];
			if (i % 3 == 0)
			{
				foreach (char c in groupDict.Keys)
				{
					if (groupDict[c] == 111)
					{
						part2Answer += Priority(c);
					}
				}
				groupDict = new Dictionary<char, int>();
			}

			int id = (int)Math.Pow(10, i % 3);
			Dictionary<char, int> dict = new Dictionary<char, int>();
			string left = line[..(line.Length / 2)];
			string right = line[(line.Length / 2)..];

			foreach (char c in left)
			{
				dict.TryAdd(c, 1);

				if (groupDict.ContainsKey(c))
				{
					if (groupDict[c] < id)
						groupDict[c] += id;
				}
				else
					groupDict[c] = id;
			}

			foreach (char c in right)
			{
				if (dict.ContainsKey(c))
					dict[c] = 2;

				if (groupDict.ContainsKey(c))
				{
					if (groupDict[c] < id)
						groupDict[c] += id;
				}
				else
					groupDict[c] = id;
			}

			foreach (char c in dict.Keys)
			{
				if (dict[c] == 2)
					part1Answer += Priority(c);
			}
		}
		foreach (char c in groupDict.Keys)
		{
			if (groupDict[c] == 111)
			{
				part2Answer += Priority(c);
			}
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}

	static int Priority(char c)
	{
		return Char.IsUpper(c) ? c - 'A' + 27 : c - 'a' + 1;
	}
}