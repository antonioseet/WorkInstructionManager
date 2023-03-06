namespace WorkInstructionTests;
using WorkInstructionManager;
using System.Text.Json;
using NUnit.Framework;

public class Tests
{
	WorkInstruction w;
    private string json;

    // Work Instruction attributes:
    private int wi_id = 12345;
    private string wi_title = "title";
    private string wi_desc = "Example Description";

	// Instruction 1 Attributes
	private int ins_id1 = 1;
	private string ins_text1 = "step1";
	private string ins_img1 = "img.jpg";
	private string ins_coor1 = "10,10";

	// Instruction 2 Attributes
	private int ins_id2 = 2;
	private string ins_text2 = "step2";
	private string ins_img2 = "img.png";
	private string ins_coor2 = "20,20";


	[SetUp]
    public void Setup()
    {
		// Work Instruction creation with the empty contructors
		// We are creating this object to serialize it into json
		// that will look just like the response from the REST endpoint.
		WorkInstruction workInstruction = new WorkInstruction
		{
			id = 12345,
			title = wi_title,
            description = wi_desc,
            instructionList = new List<Instruction>
                {
                    new Instruction
                    {
                        instructionId = ins_id1,
                        instructionText = ins_text1,
                        instructionImage = ins_img1,
                        instructionCoordinates = ins_coor1,
                        instructionForeignKey = 123
                    },
                    new Instruction
                    {
						instructionId = ins_id2,
						instructionText = ins_text2,
						instructionImage = ins_img2,
						instructionCoordinates = ins_coor2,
						instructionForeignKey = 321
                    }
                }
        };

        // Create a wrapper and set value to be the newly created WorkInstruction
        // The wrapper is necessary because we want to make sure the ouptuted JSON
        // matches the JSON we get back from the REST Endpoint.
        WorkInstructionWrapper wrapper = new WorkInstructionWrapper { workInstruction = workInstruction };
        json = JsonSerializer.Serialize(wrapper);
		w = new WorkInstruction(json);
    }

	[Test]
	public void Test_id()
	{
		int expected = 12345;
		int actual = w.id;
		Assert.That(actual, Is.EqualTo(expected));
	}

	[Test]
	public void Test_Title()
	{
		string expectedTitle = "title";
		string actualTitle = w.title;
		Assert.That(actualTitle, Is.EqualTo(expectedTitle));
	}


	[Test]
	public void Test_Description()
	{
		string expected = "title";
		string actual = w.title;
		Assert.That(actual, Is.EqualTo(expected));
	}

	[Test]
	public void Test_InstructionCount()
	{
        int expectedNum = 3;
		int actualNum = w.instructionList.Count;
		Assert.That(actualNum, Is.EqualTo(expectedNum));
	}


	[Test]
	public void Test_Instructions()
    {
        Instruction ins_1 = w.instructionList[0];
		Instruction ins_2 = w.instructionList[1];
        Assert.Multiple(() =>
		{
            // Instruction Id numbers
            Assert.That(ins_1.instructionId, Is.EqualTo(1));
            Assert.That(ins_2.instructionId, Is.EqualTo(2));

			// Instruction texts
			Assert.That(ins_1.instructionText, Is.EqualTo("step1"));
			Assert.That(ins_2.instructionText, Is.EqualTo("step2"));

		});
    }

    [Test]
	// Test starting the instruction and moving forward and backward.
    public void Test_InstructionFlow()
    {
		// Work Instruction 'w' already created above

		string actual = w.start();
		string expected = "step1\n(0/2)";
        Assert.That(actual, Is.EqualTo(expected));
		
		actual = w.getNextInstruction();
		expected = "step2\n(1/2)";
        Assert.That(actual, Is.EqualTo(expected));

		actual = w.getNextInstruction();
		expected = "COMPLETE\n(2/2)";
        Assert.That(actual, Is.EqualTo(expected));

        actual = w.getPrevInstruction();
        expected = "step2\n(1/2)";
        Assert.That(actual, Is.EqualTo(expected));
    }


	// TODO:
	// - What to do when json is not formatted properly?
	// - 
}
