public class Day2
{
	public enum Move
	{
		none = 0,
		rock = 1,
		paper = 2,
		scissors = 3,
	}

	public static Dictionary<Move, Move> winDict = new Dictionary<Move, Move>{{Move.rock, Move.paper}, {Move.paper, Move.scissors}, {Move.scissors, Move.rock}};
	public static Dictionary<Move, Move> loseDict = new Dictionary<Move, Move>{{Move.paper, Move.rock}, {Move.scissors, Move.paper}, {Move.rock, Move.scissors}};

	static void Main()
	{
		int part1Answer = 0;
		int part2Answer = 0;
		foreach (string line in File.ReadAllLines("day-2 input.txt"))
		{
			string[] lineSplit = line.Split(" ");
			Move opp = Move.none;
			Move our = Move.none;

			switch (lineSplit[0])
			{
				case "A":
					opp = Move.rock;
					break;
				case "B":
					opp = Move.paper;
					break;
				case "C":
					opp = Move.scissors;
					break;
			}

			switch (lineSplit[1])
			{
				case "X":
					our = Move.rock;
					break;
				case "Y":
					our = Move.paper;
					break;
				case "Z":
					our = Move.scissors;
					break;
			}

			part1Answer += Score(our, opp);

			//rock = lose
			//paper = draw
			//scissors = win
			if (our == Move.rock)
			{
				part2Answer += Score(loseDict[opp], opp);
			}else if (our == Move.paper)
			{
				part2Answer += Score(opp, opp);
			}else if (our == Move.scissors)
			{
				part2Answer += Score(winDict[opp], opp);
			}

		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
		
	}

	static int Score(Move our, Move opp)
	{
		int score = 0;

		score += (int) our;
		if (our == opp)
			score += 3;
		else if (our == Move.scissors && opp == Move.paper)
			score += 6;
		else if (our == Move.paper && opp == Move.rock)
			score += 6;
		else if (our == Move.rock && opp == Move.scissors)
			score += 6;
		return score;
	}
}