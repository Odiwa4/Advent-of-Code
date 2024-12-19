using System.Diagnostics;

namespace AOC2019.Day9;
class Day9
{
    public static List<long> RunProgram(Dictionary<long, long> instructions, int input)
    {
        Dictionary<int, int> opcodeParamCount = new Dictionary<int, int>{[1]=3, [2]=3, [3]=1, [4]=1, [5]=2, [6]=2, [7]=3, [8]=3, [9]=1};
        Dictionary<long, long> newInstructions = new Dictionary<long, long>(instructions);
        List<long> output = new List<long>();
        long relativeBase = 0;
        int i = 0;
        while (i < newInstructions.Count)
        {
            Console.WriteLine("------------------");
            long instruction = newInstructions[i];
            List<char> digitStrings = instruction.ToString().ToCharArray().ToList();
            List<long> digits = new List<long>();
            digitStrings.Reverse();
            foreach (char digit in digitStrings){
                digits.Add(Convert.ToInt64(digit.ToString()));
            }
            long opcode;
            if (digits.Count > 1){
                opcode = Convert.ToInt64(digits[1].ToString() + digits[0].ToString());
                digits.RemoveAt(0);
                digits.RemoveAt(0);
            }
            else{
                opcode = digits[0];
                digits.RemoveAt(0);
            }
            if (opcode == 99){
                break;
            }
            Console.WriteLine(i + ", " + opcodeParamCount[(int)opcode]);
            List<long> parameters;
            if (opcodeParamCount[(int)opcode] == 2){
                parameters = [newInstructions[i+1], newInstructions[i+2]];
            }else if (opcodeParamCount[(int)opcode] == 1){
                parameters = [newInstructions[i+1]];
            }else
                parameters = [newInstructions[i+1], newInstructions[i+2], newInstructions[i+3]];
            Console.WriteLine(string.Join(", ", parameters));
            for (int p = 0; p < parameters.Count; p++){
                if (p > digits.Count - 1){
                    Console.WriteLine("out of digits");
                    if (newInstructions.ContainsKey(parameters[p]))
                        parameters[p] = newInstructions[(int)parameters[p]];
                    else{
                        newInstructions.Add((int) parameters[p], 0);
                        parameters[p] = 0;
                    }
                    continue;
                }
                if ((parameters[p] < 0 && digits[p] != 2) || (((int)relativeBase+(int)parameters[p]) < 0)){
                    Console.WriteLine("negative value????");
                    continue;
                }
                if (parameters[p] > newInstructions.Count && digits[p] == 0){
                    if (newInstructions.ContainsKey(parameters[p]))
                        parameters[p] = newInstructions[(int)parameters[p]];
                    else{
                        newInstructions.Add((int) parameters[p], 0);
                        parameters[p] = 0;
                    }
                    continue;
                }
                Console.WriteLine("HFHWEJGNWIEFNHFK" + digits[p] + ", " + digits.Count + ", " + p);
                if (digits[p] == 0){
                    if (newInstructions.ContainsKey(parameters[p]))
                        parameters[p] = newInstructions[(int)parameters[p]];
                    else{
                        newInstructions.Add((int) parameters[p], 0);
                        parameters[p] = 0;
                    }
                }else if (digits[p] == 2){
                    if (newInstructions.ContainsKey((int)relativeBase+(int)parameters[p]))
                        parameters[p] = newInstructions[(int)relativeBase+(int)parameters[p]];
                    else{
                        newInstructions.Add((int)relativeBase+(int)parameters[p], 0);
                        parameters[p] = 0;
                    }
                }
            }
            Console.WriteLine(string.Join(", ", parameters));
            Console.WriteLine(string.Join("---, ", digits));
            if (opcode == 1)
            {
                Console.WriteLine(i + ", " + output);
                Console.WriteLine(string.Join(", ", parameters));
                Console.WriteLine(newInstructions[i+3]);
                newInstructions[(int)newInstructions[i+3]] = parameters[0] + parameters[1];
                i += 4;
            }
            else if (opcode == 2)
            {
                newInstructions[(int)newInstructions[i+3]] = parameters[0] * parameters[1];
                i += 4;
            }else if (opcode == 3)
            {
                newInstructions[(int)newInstructions[i+1]] = input;
                i += 2;
            }else if (opcode == 4)
            {
                output.Add(parameters[0]);
                i += 2;
            }else if (opcode == 5){
                if (parameters[0] != 0){
                    i = (int)parameters[1];
                }else
                    i += 3;
            }else if (opcode == 6){
                if (parameters[0] == 0){
                    i = (int)parameters[1];
                }else
                    i += 3;
            }else if (opcode == 7){
                if (parameters[0] < parameters[1])
                    newInstructions[(int)newInstructions[i+3]] = 1;
                else
                    newInstructions[(int)newInstructions[i+3]] = 0;
                i += 4;
            }else if (opcode == 8){
                if (parameters[0] == parameters[1])
                    newInstructions[(int)newInstructions[i+3]] = 1;
                else
                    newInstructions[(int)newInstructions[i+3]] = 0;
                i += 4;
            }else if (opcode == 9){
                relativeBase += parameters[0];
                i+=2;
            }else if (opcode == 99)
            {
                break;
            }
        }
        return output;
    }
    
    public static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        string[] numStrings = File.ReadAllLines(filePath)[0].Split(",");
        Dictionary<long, long> instructions = new Dictionary<long, long>();

        for (int i = 0; i < numStrings.Length; i++){
            instructions.Add(i, Convert.ToInt64(numStrings[i]));
        }

        List<long> partOneOutput = RunProgram(instructions,1);

        Console.WriteLine($"Part 1: {string.Join(", ", partOneOutput)}");
        //Console.WriteLine($"Part 2: {RunProgram(instructions,5)}");
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine($"Time: {elapsed.Minutes}:{elapsed.Seconds}.{elapsed.Milliseconds}:{elapsed.Nanoseconds}");
    }
}