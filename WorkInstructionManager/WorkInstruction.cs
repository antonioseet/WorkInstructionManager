using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Text;

namespace WorkInstructionManager
{

    public class WorkInstruction
    {
        public int Inst_ID { get; set; }
        public string Ins_Title { get; set; }
        public string Ins_Description { get; set; }
        public List<string> InstructionsList { get; set; }

        private int index = 0;
        private int max;

        public WorkInstruction(int inst_ID, string ins_Title, string ins_Description, List<string> instructionsList)
        {
            Inst_ID = inst_ID;
            Ins_Title = ins_Title;
            Ins_Description = ins_Description;
            InstructionsList = instructionsList;
            InstructionsList.Add("COMPLETE");
            max = instructionsList.Count - 1;
        }

        public WorkInstruction(int inst_ID, List<string> instructionsList)
        {
            inst_ID = inst_ID;
            instructionsList = instructionsList;
        }

        public WorkInstruction(string json)
        {
            WorkInstruction temp = JsonConvert.DeserializeObject<WorkInstruction>(json);

            Inst_ID = temp.Inst_ID;
            Ins_Title = temp.Ins_Title;
            Ins_Description = temp.Ins_Description;
            InstructionsList = temp.InstructionsList;
            InstructionsList.Add("COMPLETE");
            max = temp.InstructionsList.Count - 1;

        }

        public WorkInstruction()
        {
        }

        public string start()
        {
            return getStringWithProgress();
        }

        public string getNextInstruction()
        {
            if (index != max)
                index++;

            return getStringWithProgress();
        }

        public string getPrevInstruction()
        {
            if (index != 0)
                index--;

            return getStringWithProgress();
        }

        public string reset()
        {
            index = 0;
            return start();
        }

        private string getStringWithProgress()
        {
            string instruction = instruction = InstructionsList[index] +
                    "\n" + "(" + progress() + ")";
            return instruction;
        }

        private string progress()
        {
            return index + "/" + max;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);

            // Example of what the json would look like.
            /*
             * 
             * {
                  "Inst_ID": 123,
                  "Ins_Title": "Example Title",
                  "Ins_Description": "Example Description",
                  "InstructionsList": [
                    "Instruction 1",
                    "Instruction 2",
                    "Instruction 3"
                  ]
                }
            *
            *
            */

        }
    }
}
