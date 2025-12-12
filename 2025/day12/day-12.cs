/*
       .-,--.             .  .         ,--,--'        .     .  .               
       ' |   \ ,-. . .    |- |-. ,-.   `- | . , , ,-. |  ," |- |-.             
       , |   / ,-| | |    |  | | |-'    , | |/|/  |-' |  |- |  | |             
       `-^--'  `-^ `-|    `' ' ' `-'    `-' ' '   `-' `' |  `' ' '             
       -- -- -- --  /| -- -- -- -- -- -- -- -- -- -- --  '  -- -- --           
                   `-'                                                         
 ,--. .             .                  ,--,--'           .-,--'                
| `-' |-. ,-. . ,-. |- ,-,-. ,-. ,-.   `- | ,-. ,-. ,-.   \|__ ,-. ,-. ,-,-.   
|   . | | |   | `-. |  | | | ,-| `-.    , | |   |-' |-'    |   ,-| |   | | |   
`--'  ' ' '   ' `-' `' ' ' ' `-^ `-'    `-' '   `-' `-'   `'   `-^ '   ' ' '   
*/
public class Day12
{
	static void Main()
	{
		int i = 0;
		int part1Answer = 0;
		foreach (string line in File.ReadAllLines("day-12 input.txt"))
		{
			i++;
			// set this to the line number before the trees are listed
			if (i > 30)
			{
				string[] lineSplit = line.Split(": ");
				string[] numSplit = lineSplit[1].Split(" ");
				int sum = 0;

				foreach (string s in numSplit)
				{
					sum += int.Parse(s);
				}

				string[] xSplit = lineSplit[0].Split("x");
				int width = int.Parse(xSplit[0]);
				int height = int.Parse(xSplit[1]);
				if (width * height >= sum * 9)
				{
					part1Answer++;
				}
			}
		}

		Console.WriteLine("Part 1: " + part1Answer);
	}
}