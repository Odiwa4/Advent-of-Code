/*
   .-,--.             .  .         .---.                   . 
   ' |   \ ,-. . .    |- |-. ,-.   \___  ,-. ,-. ,-. ,-. ,-| 
   , |   / ,-| | |    |  | | |-'       \ |-' |   | | | | | | 
   `-^--'  `-^ `-|    `' ' ' `-'   `---' `-' `-' `-' ' ' `-^ 
   -- -- -- --  /| -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
               `-'                                           
               ,---.       .    .---. .                      
               |  -'  . ," |-   \___  |-. ,-. ,-.            
               |  ,-' | |- |        \ | | | | | |            
               `---|  ' |  `'   `---' ' ' `-' |-'            
                ,-.|    '                     |              
                `-+'                          '              
*/
using System.Numerics;

public class Day2
{
	static void Main()
	{
		string[] line = File.ReadAllLines("day-2 input.txt")[0].Split(",");
		BigInteger part1Answer = 0;
		BigInteger part2Answer = 0;
		foreach (string idRange in line)
		{
			BigInteger left = BigInteger.Parse(idRange.Split("-")[0]);
			BigInteger right = BigInteger.Parse(idRange.Split("-")[1]);
			for (BigInteger i = left; i <= right; i++)
			{
				bool solvedTwo = false;
				string iString = i.ToString();
				for (int x = 1; x <= iString.Length / 2; x++)
				{
					if (iString.Length % x != 0)
						continue;

					List<string> previous = new List<string>();
					for (int j = 0; j < iString.Length / x; j++)
					{
						int start = j * x;
						int end = (j + 1) * x;
						
						previous.Add(iString[start..end]);
					}

					string first = previous[0];
					bool valid = false;
					foreach (string last in previous)
					{
						if (last != first)
						{
							valid = true;
							break;
						}
					}
					if (!valid){
						if (x == iString.Length / 2f){
							part1Answer += i;
							if (solvedTwo)
								break;
						}
						if (!solvedTwo){
							part2Answer += i;
							solvedTwo = true;
						}
					}
				}
			}
		}

		Console.WriteLine("Part 1: " + part1Answer);
		Console.WriteLine("Part 2: " + part2Answer);
	}
}