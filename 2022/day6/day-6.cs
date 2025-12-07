public class Day6
{
	static void Main()
	{
		string line = File.ReadAllLines("day-6 input.txt")[0];

		Console.WriteLine("Part 1: " + Distinct(line, 4));
		Console.WriteLine("Part 2: " + Distinct(line, 14));
	}

	static int Distinct(string line, int markerLength)
	{
		for (int i = 0; i < line.Length - markerLength-1; i++)
		{
			string marker = line[i..(i + markerLength)];

			bool repeating = false;
			foreach (char c in marker)
			{
				int count = marker.Split(c).Length - 1;
				if (count > 1)
				{
					repeating = true;
					Console.WriteLine(c + " | " + count);
					break;
				}
			}

			if (!repeating)
			{
				return i + markerLength;
			}
		}
		return -1;
	}
}