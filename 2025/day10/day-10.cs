/*
.-,--.             .  .         ,--,--'      .  .   
' |   \ ,-. . .    |- |-. ,-.   `- | ,-. ,-. |- |-. 
, |   / ,-| | |    |  | | |-'    , | |-' | | |  | | 
`-^--'  `-^ `-|    `' ' ' `-'    `-' `-' ' ' `' ' ' 
-- -- -- --  /| -- -- -- -- -- -- -- -- -- -- -- -- 
            `-'                                     
           .-,--'        .                           
            \|__ ,-. ,-. |- ,-. ,-. . .              
             |   ,-| |   |  | | |   | |              
            `'   `-^ `-' `' `-' '   `-|              
                                     /|              
                                    `-'              

part 1 is my own solution
part 2 is not
i knew z3 could do it, i just didnt know how z3 worked
*/
using Microsoft.Z3;
public class Day10
{
	static void Main()
	{
		List<Machine> machines = new List<Machine>();
		foreach (string line in File.ReadAllLines("day-10 input.txt"))
		{
			Machine machine = new Machine();
			string goal = line.Split("] ")[0].TrimStart('[');
			string[] leftStuff = line.Split("]")[1].Split(" {")[0].Split(" (");
			string[] rightBracketSplit = line.Split(") ");
			string[] joltageRequirements = rightBracketSplit[rightBracketSplit.Length - 1].TrimStart('{').TrimEnd('}').Split(",");
			for (int c = 0; c < goal.Length; c++)
			{
				if (goal[c] == '#')
					machine.goal = SetBit(machine.goal, goal.Length, c);
			}

			machine.goalLength = goal.Length;

			int[] joltageCost = new int[joltageRequirements.Length];
			machine.joltageCost = joltageCost;
			for (int j = 0; j < joltageRequirements.Length; j++)
			{
				machine.joltageCost[j] = int.Parse(joltageRequirements[j]);
			}

			foreach (string button in leftStuff[1..])
			{
				string[] numbers = button.TrimEnd(')').Split(",");

				if (numbers.Length > 0)
				{
					int[] buttons = new int[numbers.Length];
					for (int n = 0; n < numbers.Length; n++)
					{
						buttons[n] = int.Parse(numbers[n]);
					}

					machine.buttons.Add(buttons);
				}
				else
				{
					int[] buttons = [int.Parse(button.TrimEnd(')'))];
					machine.buttons.Add(buttons);
				}
			}
			machines.Add(machine);
		}

		int part1Answer = 0;
		long part2Answer = 0;

		for (int m = 0; m < machines.Count; m++)
		{
			Machine machine = machines[m];

			machine.states.Add(0, 0);
			int shortestTime = 50000000;
			int previousStates = 0;

			Console.WriteLine(m+1 + "/" + machines.Count);
			while (true)
			{
				Dictionary<int, int> tempDict = new Dictionary<int, int>(machine.states);
				foreach (int state in machine.states.Keys)
				{
					int visitedTimes = machine.states[state] + 1;
					foreach (int[] b in machine.buttons)
					{
						int newState = state;
						foreach (int n in b)
						{
							newState = SetBit(newState, machine.goalLength, n);
						}

						if (newState == machine.goal)
						{
							if (visitedTimes < shortestTime)
								shortestTime = visitedTimes;
						}
						if (tempDict.ContainsKey(newState))
						{
							if (visitedTimes < tempDict[newState])
								tempDict[newState] = visitedTimes;
						}
						else
						{
							tempDict.Add(newState, visitedTimes);
						}
					}
				}
				machine.states = tempDict;
				if (machine.states.Count == previousStates)
					break;
				previousStates = machine.states.Count;
			}

			part1Answer += shortestTime;

			Context ctx = new Context();
			var opt = ctx.MkOptimize();
			IntExpr[] buttonVars = new IntExpr[machine.buttons.Count];
			
			for (int b = 0; b < machine.buttons.Count; b++)
			{
				buttonVars[b] = ctx.MkIntConst($"button_{b}");
				opt.Add(ctx.MkGe(buttonVars[b], ctx.MkInt(0)));
			}

			for (int j = 0; j < machine.joltageCost.Length; j++)
			{
				List<ArithExpr> terms = new List<ArithExpr>();
				for (int b = 0; b < machine.buttons.Count; b++)
				{
					if (machine.buttons[b].Contains(j))
					{
						terms.Add(buttonVars[b]);
					}
				}

				ArithExpr sumExpr = ctx.MkAdd(terms.ToArray());
				ArithExpr tarExpr = ctx.MkInt(machine.joltageCost[j]);
				opt.Add(ctx.MkEq(sumExpr, tarExpr));
			}

			opt.MkMinimize(ctx.MkAdd(buttonVars.Cast<ArithExpr>().ToArray()));
			Status status = opt.Check();
			if (status != Status.SATISFIABLE)
				throw new Exception($"Z3 could not find solution [status: {status}]");

			part2Answer += Enumerable.Range(0, machine.buttons.Count).Select(i => ((IntNum) opt.Model.Evaluate(buttonVars[i])).Int).Sum();
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}

	static int SetBit(int n, int l, int bit)
	{
		return n ^ 1 << l - 1 - bit;
	}
}

public class Machine
{
	public int goal = 0;
	public int goalLength = 0;
	public int start = 0;
	public List<int[]> buttons = new List<int[]>();
	public int[] joltageCost = new List<int>().ToArray();
	public Dictionary<int, int> states = new Dictionary<int, int>();
}