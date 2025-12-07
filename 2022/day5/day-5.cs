public class Day5
{
	static void Main()
	{
		List<List<string>> pillars = new List<List<string>>();
		List<List<string>> pillarsTwo = new List<List<string>>();
		foreach (string line in File.ReadAllLines("day-5 input.txt"))
		{
			if (line.Contains("["))
			{
				for (int i = 1; i < line.Length; i++)
				{
					if ((i - 1) % 4 == 0)
					{
						int boxNumber = (i - 1) / 4;
						if (pillars.Count < boxNumber + 1)
							pillars.Add(new List<string>());
						if (!Char.IsWhiteSpace(line[i]))
						{
							pillars[boxNumber].Add(line[i].ToString());
						}
					}
				}
			}
			else if (line.Contains("move"))
			{
				if (pillarsTwo.Count == 0)
				{
					pillarsTwo = [.. pillars];
					for (int x = 0; x < pillarsTwo.Count; x++)
					{
						pillarsTwo[x] = [.. pillarsTwo[x]];
					}
				}
				int amount = int.Parse(line.Split(" from")[0].TrimStart("move "));
				int from = int.Parse(line.Split("from ")[1].Split(" to")[0]) - 1;
				int to = int.Parse(line.Split("from ")[1].Split("to ")[1]) - 1;

				for (int a = 0; a < amount; a++)
				{
					if (pillars[from].Count > 0)
					{
						pillars[to].Insert(0, pillars[from][0]);
						pillars[from].RemoveAt(0);
					}
				}

				for (int a = amount - 1; a >= 0; a--)
				{
					if (pillarsTwo[from].Count > 0)
					{
						pillarsTwo[to].Insert(0, pillarsTwo[from][a]);
						pillarsTwo[from].RemoveAt(a);
					}
				}
			}
		}
		string part1Answer = "";
		string part2Answer = "";
		foreach (List<string> pillar in pillars)
		{
			part1Answer += pillar[0];
		}

		foreach (List<string> pillar in pillarsTwo)
		{
			part2Answer += pillar[0];
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}
}