/*
.-,--.             .  .        .-,--.       .   .  .    
' |   \ ,-. . .    |- |-. ,-.   `\__  . ,-. |-. |- |-.  
, |   / ,-| | |    |  | | |-'    /    | | | | | |  | |  
`-^--'  `-^ `-|    `' ' ' `-'   '`--' ' `-| ' ' `' ' '  
-- -- -- --  /| -- -- -- -- -- -- -- --  ,| -- -- -- -- 
            `-'                          `'             
       .-,--. .                                .         
        '|__/ |  ,-. . . ,-. ,-. ,-. . . ,-. ,-|         
        ,|    |  ,-| | | | | |   | | | | | | | |         
        `'    `' `-^ `-| `-| '   `-' `-^ ' ' `-^         
                      /|  ,|                             
                     `-'  `'                             

i know this solution is slow
but it works
*/


using System.Numerics;
public class Day8
{
	static void Main()
	{
		List<Vector3> junctions = new List<Vector3>();
		foreach (string line in File.ReadAllLines("day-8 input.txt"))
		{
			string[] lineSplit = line.Split(",");
			junctions.Add(new Vector3(int.Parse(lineSplit[0]), int.Parse(lineSplit[1]), int.Parse(lineSplit[2])));
		}

		List<List<Vector3>> circuits = new List<List<Vector3>>();
		HashSet<(Vector3 a, Vector3 b)> connectedSets = new HashSet<(Vector3 a, Vector3 b)>();
		SortedSet<(Vector3 a, Vector3 b)> shortestList = new SortedSet<(Vector3 a, Vector3 b)>(
			Comparer<(Vector3 a, Vector3 b)>.Create((x, y) => Math.Abs(Vector3.Distance(x.a, x.b)).CompareTo(Math.Abs(Vector3.Distance(y.a, y.b))))
		);

		foreach (Vector3 a in junctions)
		{
			foreach (Vector3 b in junctions)
			{
				if (a == b)
					continue;

				if (connectedSets.Contains((a, b)) || connectedSets.Contains((b, a)))
					continue;

				shortestList.Add((a, b));
				connectedSets.Add((a, b));

				//if part 2 is zero, increase below number until it isnt
				//it will make the code slower
				if (shortestList.Count > 10000)
				{
					connectedSets.Remove(shortestList.Max);
					shortestList.Remove(shortestList.Max);
				}
			}
		}

		int limit = 0;
		long part1Answer = 1;
		long part2Answer = 0;
		List<int> circuitsPartOne = new List<int>();
		int previousCircuitCount = 0;
		HashSet<Vector3> connectedJunctions = new HashSet<Vector3>();
		foreach ((Vector3 a, Vector3 b) connection in shortestList)
		{
			if (limit == junctions.Count-1)
			{
				circuits.Sort((a, b) => b.Count.CompareTo(a.Count));
				circuitsPartOne.Add(circuits[0].Count);
				circuitsPartOne.Add(circuits[1].Count);
				circuitsPartOne.Add(circuits[2].Count);
			}
			limit++;
			List<Vector3> circuitA = GetCircuit(connection.a, circuits);
			List<Vector3> circuitB = GetCircuit(connection.b, circuits);
			if (circuitA.Count > 0)
			{
				if (circuitA != circuitB)
				{
					if (circuitB.Count > 0)
					{
						circuitA.AddRange(circuitB);
						circuits.Remove(circuitB);
					}
					else
					{
						connectedJunctions.Add(connection.b);
						circuitA.Add(connection.b);
					}
				}
			}
			else if (circuitB.Count > 0)
			{
				if (circuitA != circuitB)
				{
					if (circuitA.Count > 0)
					{
						circuitB.AddRange(circuitA);
						circuits.Remove(circuitA);
					}
					else
					{
						connectedJunctions.Add(connection.a);
						circuitB.Add(connection.a);
					}
				}
			}
			else
			{
				connectedJunctions.Add(connection.a);
				connectedJunctions.Add(connection.b);
				circuits.Add(new List<Vector3>() { connection.a, connection.b });
			}

			if (circuits.Count == 1 && connectedJunctions.Count == junctions.Count)
			{
				part2Answer = (long)connection.a.X * (long)connection.b.X;
				break;
			}
			previousCircuitCount = circuits.Count;
		}

		foreach (int circuitLength in circuitsPartOne)
		{
			part1Answer *= circuitLength;
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);

	}

	static List<Vector3> GetCircuit(Vector3 j, List<List<Vector3>> circuits)
	{
		foreach (List<Vector3> circuit in circuits)
		{
			foreach (Vector3 junction in circuit)
			{
				if (j == junction)
					return circuit;
			}
		}
		return new List<Vector3>();
	}
}