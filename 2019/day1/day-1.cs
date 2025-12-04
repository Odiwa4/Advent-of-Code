public class Day1
{
	static void Main()
	{
		int part1Answer = 0;
		int part2Answer = 0;
		foreach (string line in File.ReadAllLines("day-1 input.txt"))
		{
			int number = int.Parse(line);
			part1Answer += Fuel(number);

			int currentFuel = Fuel(number);
			while (currentFuel > 0)
			{
				part2Answer += currentFuel;
				currentFuel = Fuel(currentFuel);
			}
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}
	
	static int Fuel(int fuel)
	{
		return (int)Math.Floor(fuel / 3f) - 2;
	}
}