using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Text;

namespace WorkInstructionManager
{
    /// <summary>
    /// Wrapper class that will go around the work instruction in order to achieve parity with the JSON we are
    /// getting back from the REST endpoint.
    /// </summary>
    public class WorkInstructionWrapper
    {
        public WorkInstruction Value { get; set; }
    }

    public class WorkInstruction
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<Instruction> instructionList { get; set; }

        private int index = 0;
        private readonly int max;

        /// <summary>
        /// This constructor can be used in case we want to manually create Work Instructions.
        /// Not required as we may only want to create them using JSON.
        /// </summary>
        /// <param name="inst_ID"></param>
        /// <param name="ins_Title"></param>
        /// <param name="ins_Description"></param>
        /// <param name="instructionsList"></param>
        public WorkInstruction(int inst_ID, string ins_Title, string ins_Description, List<Instruction> instructionsList)
        {
            this.id = inst_ID;
            this.title = ins_Title;
            this.description = ins_Description;
            this.instructionList = instructionsList;

            // Final instruction added for when users reach the end of a Work Instruction.
            this.instructionList.Add(CompleteInstruction());
            this.max = instructionsList.Count - 1;
        }

        /// <summary>
        /// This can still be used if/when we get rid of the WorkInstructionWrapper (Value in the Json)
        /// by simply removing the wrapper form the class library. This works for now.
        /// </summary>
        /// <param name="json"> Json String WITH the "value" wrapper property, this can be removed later if need be.</param>
        public WorkInstruction(string json)
        {
            WorkInstructionWrapper wrapper = JsonSerializer.Deserialize<WorkInstructionWrapper>(json);
            WorkInstruction value = wrapper.Value;

            this.id = value.id;
            this.title = value.title;
            this.description = value.description;
            this.instructionList = value.instructionList;

            // Final instruction added for when users reach the end of a Work Instruction.
            this.instructionList.Add(CompleteInstruction());
            this.max = value.instructionList.Count - 1;

        }

        /// <summary>
        /// Empty constructor for the deserializer.
        /// </summary>
        public WorkInstruction()
        {
        }

        /// <summary>
        /// Turns the Work Instruction into a json string.
        /// </summary>
        /// <returns>JSON representation of the object.</returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize<WorkInstruction>(this);
        }

        /// <summary>
        /// Returns the first instruction.
        /// </summary>
        /// <returns>First Instruction with progress.</returns>
        public string start()
        {
            return getStringWithProgress();
        }

        /// <summary>
        /// Gets the next instruction, else returns current instruction.
        /// </summary>
        /// <returns>Next Instruction string.</returns>
        public string getNextInstruction()
        {
            if (index != max)
                index++;

            return getStringWithProgress();
        }

        /// <summary>
        /// Gets the previous instruction, else returns current instruction.
        /// </summary>
        /// <returns>Previous Instruction string.</returns>
        public string getPrevInstruction()
        {
            if (index != 0)
                index--;

            return getStringWithProgress();
        }

        /// <summary>
        /// Resets the user's progress back to the first instruction. 0/max.
        /// </summary>
        /// <returns>Returns the first instruction.</returns>
        public string reset()
        {
            index = 0;
            return start();
        }

        /// <summary>
        /// Returns a string which is composed of the current Instruction and the progress made so far as a fraction.
        /// </summary>
        /// <returns>Instruction string with progress.</returns>
        private string getStringWithProgress()
        {
            string instruction = instructionList[index].instructionText +
                    "\n" + "(" + progress() + ")";
            return instruction;
        }

        /// <summary>
        /// Creates a progress string in the form of a fraction.
        /// </summary>
        /// <returns></returns>
        private string progress()
        {
            return index + "/" + max;
        }

        /// <summary>
        /// Creates an instruction that lets the User know they have completed the Work Instruction once they reach the end.
        /// </summary>
        /// <returns>Completed indicator Instruction.</returns>
        private Instruction CompleteInstruction()
        {
            return new Instruction()
            { instructionId = -1,
                instructionText = "COMPLETE",
                instructionImage = null, 
                instructionCoordinates = null, 
                instructionForeignKey = -1
            };
        }
    }
}

// TODO:
// - Use the manager to work as the API that navigates between instructions.
// - Instead of inserting a "COMPLETE" instruction at the end, find a more elegant solution.