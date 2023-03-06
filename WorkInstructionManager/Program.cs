using System.Text.Json;
using System;
using System.Collections.Generic;

namespace WorkInstructionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Work Instruction creation with the empty contructors
            WorkInstruction workInstruction = new WorkInstruction
                {
                    id = 1,
                    title = "Example title",
                    description = "Example description",
                    instructionList = new List<Instruction>
                {
                    new Instruction
                    {
                        instructionId = 1,
                        instructionText = "First instruction",
                        instructionImage = "example.png",
                        instructionCoordinates = "10,20",
                        instructionForeignKey = 123
                    },
                    new Instruction
                    {
                        instructionId = 2,
                        instructionText = "Second instruction",
                        instructionImage = "example2.png",
                        instructionCoordinates = "10,20",
                        instructionForeignKey = 123
                    }
                }
            };

            // Create a wrapper and set value to be the newly created WorkInstruction
            WorkInstructionWrapper wrapper = new WorkInstructionWrapper { workInstruction = workInstruction };
            string json = JsonSerializer.Serialize(wrapper);

            // Print the JSON to check what it looks like and if it's what we expect.
            Console.WriteLine(json);

            /*
             * we get back a value that looks like what we get from the Rest Endpoint. 
             {"Value":{"id":1,"title":"Example title","description":"Example description","instructionList":[{"instructionId":1,"instructionText":"Example instruction","instructionImage":"example.png","instructionCoordinates":"10,20","instructionForeignKey":123},{"instructionId":2,"instructionText":"Example instruction 2","instructionImage":"example.png","instructionCoordinates":"10,20","instructionForeignKey":123}]}}
             */

            // From this point, we can assume that the json is coming from the server.
            // Because it's formated the same.

            // Instead of bothering with the wrapper object when deserializing, we can simply pass in the JSON and the app creates a WorkInstruction
            //no need for --> WorkInstructionWrapper wrapper2 = JsonSerializer.Deserialize<WorkInstructionWrapper>(json);

            // Call the WorkInstruction constructor directly and it will take care of the wrapper. (can be removed later)
            // Note: The 'complete' instruction gets added in the constructor
            workInstruction = new WorkInstruction(WorkInstruction.sampleWorkInstruction1());

            Console.WriteLine("title: " + workInstruction.title);
            Console.WriteLine("id: " + workInstruction.id);
            Console.WriteLine("description: " + workInstruction.description);
            Console.WriteLine("start(0): " + workInstruction.start());
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction()+"\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");
            Console.WriteLine("next(1): " + workInstruction.getNextInstruction() + "\n");

            Console.WriteLine("done"); // Add a breakpoint here to inspect the output and element for verification. 
        }
    }
}
