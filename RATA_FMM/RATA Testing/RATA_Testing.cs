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
        /// Test calculation of total faults on a footpath
        /// </summary>
        [TestMethod]
        public void testCalcFaults()
        {
            string[] roadTestData = {"2","ALBERT ST","94","ALBERT ST","2","94-304m","304","1019","B","Concrete",
                "94-304m", "RATA 2018", "210", "210", "Right", "RATA 2018", "10/06/2018", "Concrete", "1", "2" , "0", "12", "17",
                "0", "0", "183", "3", "15", "4", "5" , "0", "22948", null, null, null, null,
                null, null, "94", "304" , "9", "L", "Latest", "R", "C", "^FVG4LF^ ^FVG4LF^ ^FTG4LF^ ^FVG5LF^ ^FVG5LF^ ^FAG5LF^ ^FAG4LF^ ^FVG5LF^", 
                "AB", "WALK", "Walk Over","Y" , "Yes, survey data can be edited", 
                "10/6/2018 (100% of 210)", "5/09/2018", "sid", null, null};
            //Create test road, taken from .xlxs file
            Road testRoad = new Road(roadTestData);

            int expectedFaults = 31;
            int actualFaults = testRoad.GetNumFaults();
            Assert.AreEqual(expectedFaults, actualFaults);
        }

        /// <summary>
        /// Test calculation of condition rating, as well as setting QGIS data for health, school areas etc
        /// </summary>
        [TestMethod]
        public void testCalcConditionRating()
        {
            string[] roadTestData = {"2","ALBERT ST","94","ALBERT ST","2","94-304m","304","1019","B","Concrete",
                "94-304m", "RATA 2018", "210", "210", "Right", "RATA 2018", "10/06/2018", "Concrete", "1", "2" , "0", "12", "17",
                "0", "0", "183", "3", "15", "4", "5" , "0", "22948", null, null, null, null,
                null, null, "94", "304" , "9", "L", "Latest", "R", "C", "^FVG4LF^ ^FVG4LF^ ^FTG4LF^ ^FVG5LF^ ^FVG5LF^ ^FAG5LF^ ^FAG4LF^ ^FVG5LF^",
                "AB", "WALK", "Walk Over","Y" , "Yes, survey data can be edited",
                "10/6/2018 (100% of 210)", "5/09/2018", "sid", null, null};
            //Create test road, taken from .xlxs file
            Road testRoad = new Road(roadTestData);

            //Using QGIS data for this particular footpath
            string[] qgisTestData = { "ALBERT ST","94","304","Right","Boundary","210","1.2","252","252","Concrete",
                "39", "Footpath", "Low", "", "", "1019", "09023ac9-98e6-4d08-bf7c-1dbea029f206", "0", "0", "0", "77.62274834", "30.13063655", "257.620672",
                "100", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

            testRoad.SetQgisData(qgisTestData); //Set QGIS data

            double expectedConditionRating = 3.745;
            double actualConditionRating = testRoad.GetConditionRating();
            Assert.AreEqual(expectedConditionRating, actualConditionRating);
        }

        /// <summary>
        /// Test calculation of footpath zone rating
        /// </summary>
        [TestMethod]
        public void testCalcZonesRating()
        {
            string[] roadTestData = {"2","ALBERT ST","94","ALBERT ST","2","94-304m","304","1019","B","Concrete",
                "94-304m", "RATA 2018", "210", "210", "Right", "RATA 2018", "10/06/2018", "Concrete", "1", "2" , "0", "12", "17",
                "0", "0", "183", "3", "15", "4", "5" , "0", "22948", null, null, null, null,
                null, null, "94", "304" , "9", "L", "Latest", "R", "C", "^FVG4LF^ ^FVG4LF^ ^FTG4LF^ ^FVG5LF^ ^FVG5LF^ ^FAG5LF^ ^FAG4LF^ ^FVG5LF^",
                "AB", "WALK", "Walk Over","Y" , "Yes, survey data can be edited",
                "10/6/2018 (100% of 210)", "5/09/2018", "sid", null, null};
            //Create test road, taken from .xlxs file
            Road testRoad = new Road(roadTestData);

            //Using QGIS data for this particular footpath
            string[] qgisTestData = { "ALBERT ST","94","304","Right","Boundary","210","1.2","252","252","Concrete",
                "39", "Footpath", "Low", "", "", "1019", "09023ac9-98e6-4d08-bf7c-1dbea029f206", "0", "0", "0", "77.62274834", "30.13063655", "257.620672",
                "100", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

            testRoad.SetQgisData(qgisTestData); //Set QGIS data

            string expectedConditionRating = "Falls within health zones(s)";
            string actualConditionRating = testRoad.GetZonesString();
            Assert.AreEqual(expectedConditionRating, actualConditionRating);
        }

        /// <summary>
        /// Test calculation of which town the footpath is in
        /// </summary>
        [TestMethod]
        public void testCalcTown()
        {
            string[] roadTestData = {"2","ALBERT ST","94","ALBERT ST","2","94-304m","304","1019","B","Concrete",
                "94-304m", "RATA 2018", "210", "210", "Right", "RATA 2018", "10/06/2018", "Concrete", "1", "2" , "0", "12", "17",
                "0", "0", "183", "3", "15", "4", "5" , "0", "22948", null, null, null, null,
                null, null, "94", "304" , "9", "L", "Latest", "R", "C", "^FVG4LF^ ^FVG4LF^ ^FTG4LF^ ^FVG5LF^ ^FVG5LF^ ^FAG5LF^ ^FAG4LF^ ^FVG5LF^",
                "AB", "WALK", "Walk Over","Y" , "Yes, survey data can be edited",
                "10/6/2018 (100% of 210)", "5/09/2018", "sid", null, null};
            //Create test road, taken from .xlxs file
            Road testRoad = new Road(roadTestData);

            //Using QGIS data for this particular footpath
            string[] qgisTestData = { "ALBERT ST","94","304","Right","Boundary","210","1.2","252","252","Concrete",
                "39", "Footpath", "Low", "", "", "1019", "09023ac9-98e6-4d08-bf7c-1dbea029f206", "0", "0", "0", "77.62274834", "30.13063655", "257.620672",
                "100", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

            testRoad.SetQgisData(qgisTestData); //Set QGIS data

            string expectedTown = "Cambridge";
            string actualTown = testRoad.GetTown();
            Assert.AreEqual(expectedTown, actualTown);
        }

        /// <summary>
        /// Test calculation of which length is the correct length to use
        /// </summary>
        [TestMethod]
        public void testGetLongLength()
        {
            string[] roadTestData = {"2","ALBERT ST","94","ALBERT ST","2","94-304m","304","1019","B","Concrete",
                "94-304m", "RATA 2018", "210", "210", "Right", "RATA 2018", "10/06/2018", "Concrete", "1", "2" , "0", "12", "17",
                "0", "0", "183", "3", "15", "4", "5" , "0", "22948", null, null, null, null,
                null, null, "94", "304" , "9", "L", "Latest", "R", "C", "^FVG4LF^ ^FVG4LF^ ^FTG4LF^ ^FVG5LF^ ^FVG5LF^ ^FAG5LF^ ^FAG4LF^ ^FVG5LF^",
                "AB", "WALK", "Walk Over","Y" , "Yes, survey data can be edited",
                "10/6/2018 (100% of 210)", "5/09/2018", "sid", null, null};
            //Create test road, taken from .xlxs file
            Road testRoad = new Road(roadTestData);

            int expectedLongLength = 210;
            int actualLongLength = testRoad.GetLongLength();
            Assert.AreEqual(expectedLongLength, actualLongLength);
        }

        /// <summary>
        /// Tests Road.cs methods that are only a single line, returning class level variables
        /// </summary>
        [TestMethod]
        public void returnClassVariables()
        {
            string[] roadTestData = {"68","HURLEY PL","9","HURLEY PL","68","9-94m","94","1967","K","Concrete",
                "9-94m", "WLASS 2015 (Y1)", "85", "85", "Right", "WLASS 2015 (Y1)", "24/07/2015", "Concrete", null, null , null, null, null,
                null, null, "2", null, null, null, null , null, "21330", null, null, null, null,
                null, null, "9", "94" , "8", "L", "Latest", "R", "C", "#4?f?4", "WF", "WALK", "Walk Over",
                "Y" , "Yes, survey data can be edited", "24/7/2015 (100% of 85)", "12/08/2015", "yen", null, null};
            //Create test road, taken from .xlxs file
            Road testRoad = new Road(roadTestData);

            //PrintDataShort
            string expectedPrintDataShort = "HURLEY PL                          85        0         0                   0              ";
            string actualPrintDataShort = testRoad.PrintDataShort();
            Assert.AreEqual(expectedPrintDataShort, actualPrintDataShort);

            //GetRoadName
            string expectedRoadName = "HURLEY PL";
            string actualRoadName = testRoad.GetRoadName();
            Assert.AreEqual(expectedRoadName, actualRoadName);

            //GetStart
            int expectedStart = 9;
            int actualStart = testRoad.GetStart();
            Assert.AreEqual(expectedStart, actualStart);

            //GetEnd
            int expectedEnd = 94;
            int actualEnd = testRoad.GetEnd();
            Assert.AreEqual(expectedEnd, actualEnd);
        }
    }

    [TestClass]
    public class Form1Test
    {
        [TestMethod]
        public void testMethod1()
        {
            
        }
    }
}
