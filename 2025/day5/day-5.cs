/*
.-,--.             .  .        .-,--'     .  .   
' |   \ ,-. . .    |- |-. ,-.   \|__ . ," |- |-. 
, |   / ,-| | |    |  | | |-'    |   | |- |  | | 
`-^--'  `-^ `-|    `' ' ' `-'   `'   ' |  `' ' ' 
-- -- -- --  /| -- -- -- -- -- -- -- --'-- -- -- 
        ,--.`-'         .                        
       | `-' ,-. ," ,-. |- ,-. ,-. . ,-.         
       |   . ,-| |- |-' |  |-' |   | ,-|         
       `--'  `-^ |  `-' `' `-' '   ' `-^         
                 '                               
*/
public class Day5
{
	static void Main()
	{
		List<FreshRange> ranges = new List<FreshRange>();
		bool listingRanges = true;
		long part1Answer = 0;
		long part2Answer = 0;

		foreach (string line in File.ReadAllLines("day-5 input.txt"))
		{
			if (line.Trim() == "")
			{
				listingRanges = false;
				continue;
			}

			if (listingRanges)
			{
				string[] splitLine = line.Split("-");
				ranges.Add(new FreshRange(long.Parse(splitLine[0]), long.Parse(splitLine[1])));
			}
			else
			{
				long number = long.Parse(line);
				foreach (FreshRange range in ranges)
				{
					if (number >= range.min && number <= range.max)
					{
						part1Answer++;
						break;
					}
				}
			}
		}

		int i = 0;

		while (i < 500)
		{
			List<FreshRange> newRanges = [.. ranges];

			//the dreaded fail safe
			i++;
			
			for (int x = 0; x < ranges.Count; x++)
			{
				for (int y = 0; y < ranges.Count; y++)
				{
					FreshRange r1 = ranges[x];
					FreshRange r2 = ranges[y];
					if (x == y)
						continue;

					//if r1 is fully inside r2
					if (r1.min > r2.min && r1.max < r2.max)
					{
						newRanges.Remove(r1);
					}

					//if r1 is overlapping on the left
					else if (r1.max >= r2.min && r1.max <= r2.max && r1.min <= r2.min)
					{
						newRanges.Remove(r1);
						newRanges.Remove(r2);
						newRanges.Add(new FreshRange(r1.min, r2.max));
					}
					//if r1 is overlapping on the right
					else if (r1.min >= r2.min && r1.min <= r2.max && r1.max >= r2.max)
					{
						newRanges.Remove(r1);
						newRanges.Remove(r2);
						newRanges.Add(new FreshRange(r2.min, r1.max));
					}
				}
			}
			newRanges = newRanges.Distinct().ToList();

			if (ranges.Count == newRanges.Count)
				break;

			ranges = [.. newRanges];
		}

		foreach (FreshRange range in ranges)
		{
			part2Answer += range.max - range.min + 1;
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}
}

public struct FreshRange(long min, long max)
{
	public long min = min;
	public long max = max;
}