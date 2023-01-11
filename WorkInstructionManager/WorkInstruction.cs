using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace WorkInstructionManager
{

    public class WorkInstruction
    {
        private int inst_ID;
        private string ins_Title;
        private string ins_Description;
        private List<string> instructionsList;

        private int index = 0;
        private int max;

        public WorkInstruction(int inst_ID, string ins_Title, string ins_Description, List<string> instructionsList)
        {
            this.inst_ID = inst_ID;
            this.ins_Title = ins_Title;
            this.ins_Description = ins_Description;
            this.instructionsList = instructionsList;
            this.instructionsList.Add("COMPLETE");
            this.max = instructionsList.Count - 1;
        }
        public WorkInstruction(int inst_ID, List<string> instructionsList)
        {
            this.inst_ID = inst_ID;
            this.instructionsList = instructionsList;
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
            string instruction = instruction = instructionsList[index] +
                    "\n" + "(" + progress() + ")";
            return instruction;
        }

        private string progress()
        {
            return index + "/" + max;
        }
    }
}
