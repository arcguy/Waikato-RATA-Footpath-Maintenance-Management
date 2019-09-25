using Microsoft.VisualStudio.TestTools.UnitTesting;
using RATA_FMM;
using System.Collections.Generic;

namespace RATA_Testing
{
    [TestClass]
    public class ParserTest
    {
        /// <summary>
        /// Tests the parser for type one codes
        /// </summary>
        [TestMethod]
        public void typeOneParser()
        {
            //Set up input and expected output
            string input = "#4?f?4";
            string[] expectedResult = { "1", "Poor Previous Reinstatement", "4", null, "1x" };
            List<string[]> expectedOutput = new List<string[]> { expectedResult };

            //Obtain acutal output of method
            List<string[]> actualOutput = new List<string[]>();
            actualOutput = CodeParser.Decode(input);

            for (int i = 0; i < expectedOutput.Count; i++) //Check every string array in expectedOutput
            {
                for (int j = 0; j < actualOutput.Count; j++) //Check every string array in actualOutput
                {
                    string[] expect = expectedOutput[i];
                    string[] actual = actualOutput[j];

                    for (int k = 0; k < expect.Length; k++) //Check that each string in the same positions match
                    {
                        string e = expect[k];
                        string a = actual[k];

                        Assert.AreEqual(e, a);
                    }
                }
            }
        }

        /// <summary>
        /// Tests the parser for type two codes
        /// </summary>
        [TestMethod]
        public void typeTwoParser()
        {
            //Set up input and expected output
            string input = "FVG4LF#56";
            string[] expectedResult = { "2", "FVG", "4", "LF", "#56" };
            List<string[]> expectedOutput = new List<string[]> { expectedResult };

            //Obtain acutal output of method
            List<string[]> actualOutput = new List<string[]>();
            actualOutput = CodeParser.Decode(input);

            for (int i = 0; i < expectedOutput.Count; i++) //Check every string array in expectedOutput
            {
                for (int j = 0; j < actualOutput.Count; j++) //Check every string array in actualOutput
                {
                    string[] expect = expectedOutput[i];
                    string[] actual = actualOutput[j];

                    for (int k = 0; k < expect.Length; k++) //Check that each string in the same positions match
                    {
                        string e = expect[k];
                        string a = actual[k];

                        Assert.AreEqual(e, a);
                    }
                }
            }
        }
    }

    [TestClass]
    public class RoadTest
    {
        /// <summary>
        /// Tests Road.cs methods that are only a single line, returning class level variables
        /// </summary>
        [TestMethod]
        public void returnClassVariables()
        {
            //PrintDataShort
            //GetRoadName
            //GetStart
            //GetEnd
            //GetNumFaults
            //GetConditionRating
            //GetFootpathCondition
            //GetFaultLengthRatio
        }
    }
}
