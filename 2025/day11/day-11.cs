/*
.-,--.             .  .        .-,--. .                   .  .     
' |   \ ,-. . .    |- |-. ,-.   `\__  |  ,-. .  , ,-. ,-. |- |-.   
, |   / ,-| | |    |  | | |-'    /    |  |-' | /  |-' | | |  | |   
`-^--'  `-^ `-|    `' ' ' `-'   '`--' `' `-' `'   `-' ' ' `' ' '   
-- -- -- --  /|  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
            `-'   .-,--.             .                             
                   `|__/ ,-. ,-. ,-. |- ,-. ,-.                    
                   )| \  |-' ,-| |   |  | | |                      
                   `'  ` `-' `-^ `-' `' `-' '                      
*/
public class Day11()
{
	static void Main()
	{
		Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
		foreach (string line in File.ReadAllLines("day-11 input.txt"))
		{
			List<string> outputs = new List<string>();
			string[] commaSplit = line.Split(": ");

			foreach (string s in commaSplit[1].Split(" "))
			{
				outputs.Add(s);
			}

			connections.Add(commaSplit[0], outputs);
		}

		Console.WriteLine("Part 1: " + Run("you", "out", 1, connections));
		long fftReached = Run("svr", "fft", 1, connections);
		long dacReached = Run("fft", "dac", fftReached, connections);

		Console.WriteLine("Part 2: " + Run("dac", "out", dacReached, connections));

	}

	static long Run(string start, string end, long startVisited, Dictionary<string, List<string>> connections)
	{
		Dictionary<string, long> visited = new Dictionary<string, long>();

		Queue<string> queue = new Queue<string>();
		queue.Enqueue(start);
		long answer = 0;
		while (queue.Count > 0)
		{
			string name = queue.Dequeue();

			long timesVisited = startVisited;

			if (visited.ContainsKey(name))
			{
				timesVisited = visited[name];
				visited.Remove(name);
			}
			
			if (name == end){
				answer += timesVisited;
			}

			if (!connections.ContainsKey(name))
				continue;

			foreach (string next in connections[name])
			{
				if (visited.ContainsKey(next))
					visited[next] += timesVisited;
				else
				{
					queue.Enqueue(next);
					visited.Add(next, timesVisited);
				}
			}
		}
		return answer;
	}
}