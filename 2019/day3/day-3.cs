/*
This place is not a place of honour.
No highly esteemed deed is commemorated here.
*/

using System.Numerics;

public class Day3
{
	static void Main()
	{
		Dictionary<Vector2, int> line1Pos = new Dictionary<Vector2, int>();
		List<Vector2> intersections = new List<Vector2>();
		List<(int, int)> intersectionsTwoElectricBoogaloo = new List<(int, int)>();

		Vector2 currentPos = Vector2.Zero;
		int currentSum = 0;
		foreach (string direction in File.ReadAllLines("day-3 input.txt")[0].Split(","))
		{
			if (direction.Contains("U"))
			{
				int moveAmount = int.Parse(direction.Split("U")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(0, 1);
					if (!line1Pos.ContainsKey(currentPos))
						line1Pos.Add(currentPos, currentSum);
				}
			}
			else if (direction.Contains("D"))
			{
				int moveAmount = int.Parse(direction.Split("D")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(0, -1);
					if (!line1Pos.ContainsKey(currentPos))
						line1Pos.Add(currentPos, currentSum);
				}
			}
			else if (direction.Contains("L"))
			{
				int moveAmount = int.Parse(direction.Split("L")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(-1, 0);
					if (!line1Pos.ContainsKey(currentPos))
						line1Pos.Add(currentPos, currentSum);
				}
			}
			else if (direction.Contains("R"))
			{
				int moveAmount = int.Parse(direction.Split("R")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(1, 0);
					if (!line1Pos.ContainsKey(currentPos))
						line1Pos.Add(currentPos, currentSum);
				}
			}
		}

		currentPos = Vector2.Zero;
		currentSum = 0;
		foreach (string direction in File.ReadAllLines("day-3 input.txt")[1].Split(","))
		{
			if (direction.Contains("U"))
			{
				int moveAmount = int.Parse(direction.Split("U")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(0, 1);
					if (line1Pos.ContainsKey(currentPos))
					{
						intersections.Add(currentPos);
						intersectionsTwoElectricBoogaloo.Add((line1Pos[currentPos], currentSum));
					}
				}
			}
			else if (direction.Contains("D"))
			{
				int moveAmount = int.Parse(direction.Split("D")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(0, -1);
					if (line1Pos.ContainsKey(currentPos))
					{
						intersections.Add(currentPos);
						intersectionsTwoElectricBoogaloo.Add((line1Pos[currentPos], currentSum));
					}
				}
			}
			else if (direction.Contains("L"))
			{
				int moveAmount = int.Parse(direction.Split("L")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(-1, 0);
					if (line1Pos.ContainsKey(currentPos))
					{
						intersections.Add(currentPos);
						intersectionsTwoElectricBoogaloo.Add((line1Pos[currentPos], currentSum));
					}
				}
			}
			else if (direction.Contains("R"))
			{
				int moveAmount = int.Parse(direction.Split("R")[1]);
				for (int i = 0; i < moveAmount; i++)
				{
					currentSum++;
					currentPos += new Vector2(1, 0);
					if (line1Pos.ContainsKey(currentPos))
					{
						intersections.Add(currentPos);
						intersectionsTwoElectricBoogaloo.Add((line1Pos[currentPos], currentSum));
					}
				
				}
			}
		}

		float shortestDistance = 50000000;
		foreach (Vector2 intersection in intersections)
		{
			if (ManhattenDistance(Vector2.Zero, intersection) < shortestDistance)
			{
				shortestDistance = ManhattenDistance(Vector2.Zero, intersection);
			}
		}

		float shortestSum = 5000000;
		foreach ((int, int) intIntPairsYippee in intersectionsTwoElectricBoogaloo)
		{
			if (intIntPairsYippee.Item1 + intIntPairsYippee.Item2 < shortestSum)
			{
				shortestSum = intIntPairsYippee.Item1 + intIntPairsYippee.Item2;
			}
		}

		Console.WriteLine("Part 1: " + shortestDistance);
		Console.WriteLine("Part 2: " + shortestSum);
	}

	static float ManhattenDistance(Vector2 pos1, Vector2 pos2)
	{
		return Math.Abs(pos1.X - pos2.X) + Math.Abs(pos1.Y - pos2.Y);
	}
}