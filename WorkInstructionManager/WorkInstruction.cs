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
        private int ins_Count;

        public WorkInstruction(int inst_ID, string ins_Title, string ins_Description, List<string> instructionsList)
        {
            this.inst_ID = inst_ID;
            this.ins_Title = ins_Title;
            this.ins_Description = ins_Description;
            this.instructionsList = instructionsList;
            this.ins_Count = instructionsList.Count;
        }
        public WorkInstruction(int inst_ID, List<string> instructionsList)
        {
            this.inst_ID = inst_ID;
            this.instructionsList = instructionsList;
        }

        public int instructionsLeft()
        {
            return ins_Count - index;
        }

        public string nextInstruction()
        {
            string instruction = instructionsList[index];
            index++;
            return instruction;
        }

    }
}
