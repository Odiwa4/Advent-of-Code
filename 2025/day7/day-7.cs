/*
.-,--.             .  .         .---.                  .  .     
' |   \ ,-. . .    |- |-. ,-.   \___  ,-. .  , ,-. ,-. |- |-.   
, |   / ,-| | |    |  | | |-'       \ |-' | /  |-' | | |  | |   
`-^--'  `-^ `-|    `' ' ' `-'   `---' `-' `'   `-' ' ' `' ' '   
-- -- -- --  /| -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
         |  `-'   .               .                             
         |    ,-. |-. ,-. ,-. ,-. |- ,-. ,-. . ,-. ,-.          
         |    ,-| | | | | |   ,-| |  | | |   | |-' `-.          
         `--' `-^ ^-' `-' '   `-^ `' `-' '   ' `-' `-'          
*/
public class Day7
{
	static void Main()
	{
		string[] lines = File.ReadAllLines("day-7 input.txt");
		int width = lines[0].Length;
		int height = lines.Length;
		string[,] map = new string[width, height];

		Dictionary<IVector2, long> splitters = new Dictionary<IVector2, long>();
		for (int l = 0; l < lines.Length; l++)
		{
			for (int c = 0; c < lines[0].Length; c++)
			{
				map[c, l] = lines[l][c].ToString();
				if (map[c, l] == "^")
					splitters.Add(new IVector2(c, l), new long());
			}
		}

		long part2Answer = 0;

		int accessableSplitters = 0;
		foreach (IVector2 splitterPos in splitters.Keys)
		{
			long splitter = splitters[splitterPos];
			IVector2 newPos = new IVector2(splitterPos.x, splitterPos.y - 1);
			while (true)
			{
				string upState = map[newPos.x, newPos.y];
				string ULState = map[newPos.x - 1, newPos.y];
				string URState = map[newPos.x + 1, newPos.y];

				if (ULState == "^" || URState == "^" || upState == "S")
				{
					accessableSplitters++;
					break;
				}

				if (upState == "^")
					break;

				newPos = new IVector2(newPos.x, newPos.y - 1);
			}

		}

		for (int y = 0; y < height; y++)
		{
			List<IVector2> currentSplitters = new List<IVector2>();
			for (int x = 0; x < width; x++)
			{
				if (splitters.ContainsKey(new IVector2(x, y)))
					currentSplitters.Add(new IVector2(x, y));
			}

			if (currentSplitters.Count == 0)
				continue;

			if (y == 2)
				splitters[currentSplitters[0]] = 1;

			foreach (IVector2 sPos in currentSplitters)
			{
				IVector2 left = new IVector2(sPos.x - 1, sPos.y);
				IVector2 right = new IVector2(sPos.x + 1, sPos.y);

				IVector2 leftSplitter = NextSplitter(left, map, width, height);
				IVector2 rightSplitter = NextSplitter(right, map, width, height);

				if (leftSplitter.x != -1)
					splitters[leftSplitter] += splitters[sPos];
				else
					part2Answer += splitters[sPos];

				if (rightSplitter.x != -1)
					splitters[rightSplitter] += splitters[sPos];
				else
					part2Answer += splitters[sPos];
			}
		}

		Console.WriteLine("Part 1: " + accessableSplitters);
		Console.WriteLine("Part 2: " + part2Answer);
	}

	static bool InRange(IVector2 pos, int width, int height)
	{
		return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
	}

	static IVector2 NextSplitter(IVector2 startPos, string[,] map, int width, int height)
	{
		IVector2 newPos = new IVector2(startPos.x, startPos.y + 1);
		while (true)
		{
			if (!InRange(newPos, width, height))
				return new IVector2(-1, -1);
			if (map[newPos.x, newPos.y] == "^")
				return newPos;
			newPos = new IVector2(newPos.x, newPos.y + 1);
		}
	}
}

public struct IVector2(int x, int y)
{
	public int x = x;
	public int y = y;
}