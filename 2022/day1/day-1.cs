public class Day1
{
	static void Main()
	{
		List<int> elves = new List<int>();

		int sum = 0;
		foreach (string line in File.ReadAllLines("day-1 input.txt"))
		{
			if (line.Trim() != "")
			{
				sum += int.Parse(line);
			}
			else
			{
				elves.Add(sum);
				sum = 0;
			}
		}

		Console.WriteLine("Part 1: " + elves.Max());

		int[] top = new int[3];
		for (int i = 0; i < top.Length; i++)
		{
			int max = elves.Max();
			top[i] = max;
			elves.Remove(max);
		}
		
		Console.WriteLine("Part 2: " + top.Sum());
	}
}