using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkInstructionManager
{
    public class Instruction
    {

        public int instructionId { get; set; }
        public string instructionText { get; set; }
        public string instructionImage { get; set; }
        public string instructionCoordinates { get; set; }
        public int instructionForeignKey { get; set; }

        public Instruction(int instructionId, string instructionText, string instructionImage, string instructionCoordinates, int foreignKey)
        {
            this.instructionId = instructionId;
            this.instructionText = instructionText;
            this.instructionImage = instructionImage;
            this.instructionCoordinates = instructionCoordinates;
            this.instructionForeignKey = foreignKey;
        }

        // Empty constructor for the deserializer.
        public Instruction() { }

    }
}
