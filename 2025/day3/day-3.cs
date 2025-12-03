/*
.-,--.             .  .         ,--,--'.           .   
' |   \ ,-. . .    |- |-. ,-.   `- |   |-. . ,-. ,-|   
, |   / ,-| | |    |  | | |-'    , |   | | | |   | |   
`-^--'  `-^ `-|    `' ' ' `-'    `-'   ' ' ' '   `-^   
-- -- -- --  /| -- -- -- -- -- -- -- -- -- -- -- -- -- 
            `-'|        .   .                          
               |    ,-. |-. |-. . .                    
               |    | | | | | | | |                    
               `--' `-' ^-' ^-' `-|                    
                                 /|                    
                                `-'                    
*/
public class Day3
{
	static void Main()
	{
		long part1Answer = 0;
		long part2Answer = 0;
		foreach (string line in File.ReadAllLines("day-3 input.txt"))
		{
			part1Answer += Jolt(2, line);

			part2Answer += Jolt(12, line);
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}

	static long Jolt(int digitLength, string line)
	{
		string jolt = "";
		int lastIndex = -1;
		for (int i = digitLength-1; i >= 0; i--)
		{

			int highest = 0;
			for (int c = lastIndex + 1; c < line.Length - i; c++)
			{
				int number = int.Parse(line[c].ToString());
				if (number > highest)
				{
					highest = number;
					lastIndex = c;
				}
			}
			jolt += highest.ToString();
		}
		return long.Parse(jolt);
	}
}