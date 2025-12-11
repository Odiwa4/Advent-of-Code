/*
   .-,--.             .  .         ,-,-.       .  .     
   ' |   \ ,-. . .    |- |-. ,-.   ` | | . ,-. |- |-.   
   , |   / ,-| | |    |  | | |-'     | | | | | |  | | 
   `-^--'  `-^ `-|    `' ' ' `-'     ' ' ' ' ' '  ' '
   -- -- -- --  /| -- -- -- -- -- -- -- -- -- -- -- --
,-,-,-.        `-'         ,--,--'.           .           
`,| | |   ,-. .  , . ,-.   `- |   |-. ,-. ,-. |- ,-. ,-.  
  | ; | . | | | /  | |-'    , |   | | |-' ,-| |  |   |-'  
  '   `-' `-' `'   ' `-'    `-'   ' ' `-' `-^ `' '   `-'  

this isnt the best solution but it does work with the input we are given
this doesnt not work generally however
*/
using System.Numerics;

public class Day9
{
	static void Main()
	{
		List<Vector2> redTiles = new List<Vector2>();

		foreach (string line in File.ReadAllLines("day-9 input.txt"))
		{
			string[] lineSplit = line.Split(",");
			int x = int.Parse(lineSplit[0]);
			int y = int.Parse(lineSplit[1]);
			redTiles.Add(new Vector2(x, y));
		}

		long part1Answer = 0;
		List<(Vector2 a, Vector2 b)> edges = new List<(Vector2 a, Vector2 b)>();
		foreach (Vector2 a in redTiles)
		{
			foreach (Vector2 b in redTiles)
			{
				if (a == b)
					continue;
				long area = (long)(Math.Abs(a.X - b.X) + 1) * (long)(Math.Abs(a.Y - b.Y) + 1);

				if (area > part1Answer)
					part1Answer = area;
			}

			int index = redTiles.IndexOf(a);
			Vector2 previous;
			if (index > 0)
				previous = redTiles[index - 1];
			else
				previous = redTiles[redTiles.Count - 1];

			edges.Add((a, previous));
		}

		long part2Answer = 0;
		foreach (Vector2 a in redTiles)
		{
			foreach (Vector2 b in redTiles)
			{
				if (a == b)
					continue;

				long area = (long)(Math.Abs(a.X - b.X) + 1) * (long)(Math.Abs(a.Y - b.Y) + 1);
				if ((a.X == b.X || a.Y == b.Y) && area > part2Answer)
					part2Answer = area;
				else
				{
					Vector2 leftPoint = a.X < b.X ? a : b;
					Vector2 rightPoint = a.X > b.X ? a : b;

					int minX = (int)leftPoint.X + 1;
					int maxX = (int)rightPoint.X - 1;

					int minY = leftPoint.Y < rightPoint.Y ? (int)leftPoint.Y + 1 : (int)rightPoint.Y + 1;
					int maxY = leftPoint.Y > rightPoint.Y ? (int)leftPoint.Y - 1 : (int)rightPoint.Y - 1;

					bool count = true;
					foreach ((Vector2 a, Vector2 b) edge in edges)
					{
						if (edge.a.X == edge.b.X)
						{
							int lowY = edge.a.Y < edge.b.Y ? (int)edge.a.Y : (int)edge.b.Y;
							int highY = edge.a.Y > edge.b.Y ? (int)edge.a.Y : (int)edge.b.Y;

							if (edge.a.X >= minX && edge.a.X <= maxX)
							{
								if (lowY <= maxY && highY >= minY)
								{
									count = false;
									break;
								}
							}
						}
						else if (edge.a.Y == edge.b.Y)
						{
							int lowX = edge.a.X < edge.b.X ? (int)edge.a.X : (int)edge.b.X;
							int highX = edge.a.X > edge.b.X ? (int)edge.a.X : (int)edge.b.X;

							if (edge.a.Y >= minY && edge.a.Y <= maxY)
							{
								if (lowX <= maxX && highX >= minX)
								{
									count = false;
									break;
								}
							}
						}
					}
					if (count && area > part2Answer)
						part2Answer = area;
				}
			}
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}
}