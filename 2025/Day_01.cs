using System.Data;
using System.Data.Common;
using System.Runtime.Serialization.Formatters;

namespace aoc;

public static class Day_01
{
    public static int Part1(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();

        int dialPos = 50;
        int zeroCount = 0;

        timer.StartExecuting();

        foreach (var command in input)
        {
            
            char dir = command[0];
            int steps = int.Parse(command[1..]);
            
            if (dir == 'R')
                dialPos = (dialPos + steps) % 100;
            else if (dir == 'L')
                dialPos = (dialPos - steps + 100) % 100;
            if (dialPos == 0)
                zeroCount++;    
        }
        timer.Stop();

        return zeroCount;

    }
}
    

//    public static int Part2(SolutionTimer timer, string[] input)
//    {
//        timer.StartParsing();
//        
//                int dialPos = 50;
//                int zeroCount = 0;
//        
//                timer.StartExecuting();
//        
//                foreach (var command in input)
//                {
//                    
//                    char dir = command[0];
//                    int steps = int.Parse(command[1..]);
//                    
//                    for (int i = 0; i < steps; i++)
//                    {
//                        int prevPos = dialPos;
//        
//                        if (dir == 'R')
//                        {
//                            dialPos++;
//                            if (dialPos > 99) dialPos = 0;
//                        }
//                        else if (dir == 'L')
//                        {
//                            dialPos--;
//                            if (dialPos < 0) dialPos = 99;
//                        }
//                        if (prevPos == 99 && dialPos == 0)
//                            zeroCount++;
//                        if (prevPos == 0 && dialPos == 99)
//                            zeroCount++;
//                    }
//                }
//                timer.Stop();
//        
//                return zeroCount;
//    }
//}