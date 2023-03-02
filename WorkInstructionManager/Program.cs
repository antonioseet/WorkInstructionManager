using System;
using System.Collections.Generic;

namespace WorkInstructionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<string> instructionsList = new List<string> { "Instruction 1", "Instruction 2", "Instruction 3" };
            WorkInstruction workInstruction = new WorkInstruction(123, "Example Title", "Example Description", instructionsList);

            string json = workInstruction.ToJson();
            Console.WriteLine(json);

            WorkInstruction test = new WorkInstruction(json);
            Console.WriteLine(test);
        }
    }
}
