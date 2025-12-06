/*
     .-,--.             .  .         .---.       .  .         
     ' |   \ ,-. . .    |- |-. ,-.   \___  . . , |- |-.       
     , |   / ,-| | |    |  | | |-'       \ |  X  |  | |       
     `-^--'  `-^ `-|    `' ' ' `-'   `---' ' ' ` `' ' '       
     -- -- -- --  /| -- -- -- -- -- -- -- -- -- -- -- --      
                 `-'                                          
,--,--'          .      ,--.                       .          
`- | ,-. ,-. ,-. |-.   | `-' ,-. ,-,-. ,-. ,-. ,-. |- ,-. ,-. 
 , | |   ,-| `-. | |   |   . | | | | | | | ,-| |   |  | | |   
 `-' '   `-^ `-' ' '   `--'  `-' ' ' ' |-' `-^ `-' `' `-' '   
                                       |                      
                                       '                      
*/
using System.Text.RegularExpressions;

public class Day6
{
	static void Main()
	{
		string[] lines = File.ReadAllLines("day-6 input.txt");

		List<Problem> problems = new List<Problem>();

		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];
			line = Regex.Replace(line, @"\s+", " ").TrimStart(' ');
			string[] lineSplit = line.Split(" ");
			for (int n = 0; n < lineSplit.Length; n++)
			{
				if (lineSplit[n] == "")
					continue;
				if (i == 0)
				{
					problems.Add(new Problem());
				}
				if (line.Contains("+"))
				{
					if (lineSplit[n].Contains("+"))
						problems[n].type = Problem.Type.add;
					else if (lineSplit[n].Contains("*"))
						problems[n].type = Problem.Type.multiply;
				}
				else
				{
					problems[n].ints.Add(int.Parse(lineSplit[n]));
				}
			}
		}

		Problem currentProblem = new Problem() { partOne = false };
		for (int c = 0; c < lines[0].Length; c++)
		{
			string digit = "";
			for (int l = 0; l < lines.Length; l++)
			{
				char currentChar = lines[l][c];
				if (currentChar.ToString() == "*")
				{
					currentProblem.type = Problem.Type.multiply;
				}
				else if (currentChar.ToString() == "+")
				{
					currentProblem.type = Problem.Type.add;
				}
				else if (currentChar.ToString() != " ")
				{
					digit += currentChar;
				}
			}

			if (digit == "")
			{
				problems.Add(currentProblem);
				currentProblem = new Problem() { partOne = false };
			}
			else
			{
				currentProblem.ints.Add(int.Parse(digit));
			}
		}
		problems.Add(currentProblem);

		long part1Answer = 0;
		long part2Answer = 0;
		foreach (Problem p in problems)
		{
			if (!p.partOne)
				p.ints.Reverse();
			if (p.type == Problem.Type.add)
			{
				// part1Answer += p.ints.Sum();
				int answer = 0;
				foreach (int n in p.ints)
				{
					answer += n;
				}
				if (p.partOne)
					part1Answer += answer;
				else
					part2Answer += answer;
			}
			else if (p.type == Problem.Type.multiply)
			{
				long answer = 1;
				foreach (int n in p.ints)
				{
					answer *= n;
				}
				if (p.partOne)
					part1Answer += answer;
				else
					part2Answer += answer;
			}
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}
}

public class Problem()
{
	public enum Type
	{
		none,
		add,
		multiply,
	}
	public List<int> ints = new List<int>();
	public Type type = Type.none;
	public bool partOne = true;
}