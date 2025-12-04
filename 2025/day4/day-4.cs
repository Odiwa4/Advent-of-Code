/*
         .-,--.             .  .        .-,--'            .  .               
         ' |   \ ,-. . .    |- |-. ,-.   \|__ ,-. . . ,-. |- |-.             
         , |   / ,-| | |    |  | | |-'    |   | | | | |   |  | |             
         `-^--'  `-^ `-|    `' ' ' `-'   `'   `-' `-^ '   `' ' '             
         -- -- -- --  /| -- -- -- -- -- -- -- -- -- -- -- -- -- --           
.-,--.           .   `-'         .-,--.                  .                .  
 '|__/ ,-. . ,-. |- . ,-. ,-.    ' |   \ ,-. ,-. ,-. ,-. |- ,-,-. ,-. ,-. |- 
 ,|    |   | | | |  | | | | |    , |   / |-' | | ,-| |   |  | | | |-' | | |  
 `'    '   ' ' ' `' ' ' ' `-|    `-^--'  `-' |-' `-^ '   `' ' ' ' `-' ' ' `' 
                           ,|                |                               
                           `'                '                               
*/
public class Day4
{
	static void Main()
	{
		string fileName = "day-4 input.txt";
		string[] lines = File.ReadAllLines(fileName);
		int width = lines[0].Length;
		int height = lines.Length;
		bool[,] map = new bool[width, height];
		(int x, int y)[] directions = [(-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (1, 1), (-1, 1), (1, -1)];
		for (int y = 0; y < height; y++)
		{
			char[] chars = lines[y].ToCharArray();
			for (int x = 0; x < chars.Length; x++)
			{
				if (chars[x] == '.')
					map[x, y] = false;
				else if (chars[x] == '@')
					map[x, y] = true;
			}
		}

		int part1Answer = 0;
		int papersRemoved = 0;
		int previousPapersRemoved = 0;
		bool part1Done = false;
		while (true)
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					bool paper = map[x, y];

					if (paper)
					{
						int paperCount = 0;
						foreach ((int x, int y) d in directions)
						{
							int newX = x + d.x;
							int newY = y + d.y;
							if (InRange(newX, newY, width, height))
							{
								if (map[newX, newY])
									paperCount++;
							}
						}

						if (paperCount < 4)
						{
							if (!part1Done)
								part1Answer++;
							else
							{
								papersRemoved++;
								map[x, y] = false;
							}
						}
					}
				}
			}
			if (part1Done)
			{
				if (previousPapersRemoved == papersRemoved)
					break;
				previousPapersRemoved = papersRemoved;
			}
			part1Done = true;
		}

		/* for printing final state
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				bool paper = map[x, y];
				Console.Write(paper ? "@" : ".");

			}
			Console.Write("\n");
		}
		*/

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + papersRemoved);
	}

	static bool InRange(int x, int y, int width, int height)
	{
		return x >= 0 && x < width && y >= 0 && y < height;
	}
}