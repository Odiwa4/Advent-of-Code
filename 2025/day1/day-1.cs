/*
     .-,--.             .  .        .-,--'          .       
     ' |   \ ,-. . .    |- |-. ,-.   \|__ . ,-. ,-. |-      
     , |   / ,-| | |    |  | | |-'    |   | |   `-. |       
     `-^--'  `-^ `-|    `' ' ' `-'   `'   ' '   `-' `'      
-- -- -- -- -- -- /| -- -- -- -- -- -- -- -- -- -- -- -- --  
.---.            `-'  .   .-,--.     .                      
\___  ,-. ,-. ,-. ,-. |-   `\__  ,-. |- ,-. ,-. ,-. ,-. ,-. 
    \ |-' |   |   |-' |     /    | | |  |   ,-| | | |   |-' 
`---' `-' `-' '   `-' `'   '`--' ' ' `' '   `-^ ' ' `-' `-'           
*/


public class Day1
{
	static void Main()
	{
		int number = 50;
		int partOneAnswer = 0;
		int partTwoAnswer = 0;
		int previousNumber = 50;
		foreach (string line in File.ReadAllLines("day-1 input.txt"))
		{
			if (line.Contains("L"))
			{
				int change = int.Parse(line.Split("L")[1]);
				number -= change % 100;
				if (number < 0)
				{
					if (previousNumber != 0)
					{
						partTwoAnswer++;
					}
					number = 100 + number;
				}
				partTwoAnswer += (int)Math.Floor(change / 100f);
			}
			else if (line.Contains("R")){
				int change = int.Parse(line.Split("R")[1]);
				number += change % 100;
				if (number > 99)
				{
					number = number - 100;
					if (previousNumber != 0 && number != 0)
					{
						partTwoAnswer++;
					}
				}
				partTwoAnswer += (int)Math.Floor(change / 100f);
			}
			
			previousNumber = number;
			if (number == 0){
				partOneAnswer++;
				partTwoAnswer++;
			}
		}
		Console.WriteLine("Part 1: partOneAnswer");
		Console.WriteLine("Part 2: partTwoAnswer");
	}
}