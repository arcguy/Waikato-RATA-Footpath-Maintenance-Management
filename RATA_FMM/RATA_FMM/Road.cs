using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RATA_FMM
{
    class Road
    {
        //instance variables
        private int road;
        private string roadName;
        private int start;
        private string localityName;
        private int localityID;
        private string displacement; //could possibly split into 2 seperate ints
        private int end;
        private int footpath1;
        private char footpath2;
        private string footpathSurfaceMaterial;
        private string inspection; //could possibly split into 2 seperate ints
        private string surveyDescription;
        private int length1;
        private int length2;
        private string side;
        private string survey;
        private DateTime date;
        private string footpathSurfaceMaterial2; //duplicate?
        private int settlement;
        private int bumps;
        private int depressions;
        private int cracked;
        private int scabbing;
        private int patches;
        private int potholes;
        private int extra1;
        private int extra2;
        private int extra3;
        private int extra4;
        private int extra5;
        private int extra6;
        private int footpathRatingID;
        private int calculatedPriority;
        private int enteredPriority;
        private float calculatedCost;
        private float enteredCost;
        private string warning;
        private string priorityNotes;
        private int inspectionStart;
        private int inspectionEnd;
        private int survey2; //probably needs better name
        private char latest;
        private string latest2; //probably needs better name
        private char side2; //probably needs better name
        private string footpathSurfaceMaterial3;
        private string notes;
        private string rater;
        private string surveyMethod;
        private string surveyMethod2;
        private char editSurveyData;
        private string editSurveyData2;
        private string mapDesc1;
        private DateTime dateAdded;
        private string addedBy;
        private string dateChanged; //string due to errors if no date present
        private string changedBy;

        private int numFaults;
        private int conditionRating;

        private string[] qgisData;
        //0 - road id
        //1 - start
        //2 - end
        //5 - length
        //10 - age
        //20 - school buffer area
        //22 - health buffer area

        //contructor function
        public Road(string[] roadData)
        {
            //removing null values to avoid errors
            for (int i = 0; i < roadData.Length; i++)
                if (roadData[i] == null)
                    roadData[i] = "-1";

            road = int.Parse(roadData[0]);
            roadName = roadData[1];
            start = int.Parse(roadData[2]);
            localityName = roadData[3];
            localityID = int.Parse(roadData[4]);
            displacement = roadData[5];
            end = int.Parse(roadData[6]);
            footpath1 = int.Parse(roadData[7]);
            footpath2 = char.Parse(roadData[8]);
            footpathSurfaceMaterial = roadData[9];
            inspection = roadData[10];
            surveyDescription = roadData[11];
            length1 = int.Parse(roadData[12]);
            length2 = int.Parse(roadData[13]);
            side = roadData[14];
            survey = roadData[15];
            date = DateTime.Parse(roadData[16]);
            footpathSurfaceMaterial2 = roadData[17];
            settlement = int.Parse(roadData[18]);
            bumps = int.Parse(roadData[19]);
            depressions = int.Parse(roadData[20]);
            cracked = int.Parse(roadData[21]);
            scabbing = int.Parse(roadData[22]);
            patches = int.Parse(roadData[23]);
            potholes = int.Parse(roadData[24]);
            extra1 = int.Parse(roadData[25]);
            extra2 = int.Parse(roadData[26]);
            extra3 = int.Parse(roadData[27]);
            extra4 = int.Parse(roadData[28]);
            extra5 = int.Parse(roadData[29]);
            extra6 = int.Parse(roadData[30]);
            footpathRatingID = int.Parse(roadData[31]);
            calculatedPriority = int.Parse(roadData[32]);
            enteredPriority = int.Parse(roadData[33]);
            calculatedCost = float.Parse(roadData[34]);
            enteredCost = float.Parse(roadData[35]);
            warning = roadData[36];
            priorityNotes = roadData[37];
            inspectionStart = int.Parse(roadData[38]);
            inspectionEnd = int.Parse(roadData[39]);
            survey2 = int.Parse(roadData[40]);
            latest = char.Parse(roadData[41]);
            latest2 = roadData[42];
            side2 = char.Parse(roadData[43]);
            footpathSurfaceMaterial3 = roadData[44];
            notes = roadData[45];
            rater = roadData[46];
            surveyMethod = roadData[47];
            surveyMethod2 = roadData[48];
            editSurveyData = char.Parse(roadData[49]);
            editSurveyData2 = roadData[50];
            mapDesc1 = roadData[51];
            dateAdded = DateTime.Parse(roadData[52]);
            addedBy = roadData[53];
            dateChanged = roadData[54];//string to avoid errors when no date entered
            changedBy = roadData[55];

            numFaults = CalcFaults();            
        }

        //Returns every field in string format
        public override string ToString()
        {          
                return road.ToString().PadRight(10) + roadName.PadRight(35) + start.ToString().PadRight(10) +
                localityName.PadRight(35) + localityID.ToString().PadRight(15) + displacement.PadRight(15) +
                end.ToString().PadRight(10) + footpath1.ToString().PadRight(12) + footpath2.ToString().PadRight(12) + footpathSurfaceMaterial.PadRight(27) +
                inspection.PadRight(15) + surveyDescription.PadRight(20) + length1.ToString().PadRight(7) + length2.ToString().PadRight(7) +
                side.PadRight(7) + survey.PadRight(25) + date.ToShortDateString().PadRight(15) + settlement.ToString().PadRight(12) +
                bumps.ToString().PadRight(7) + depressions.ToString().PadRight(13) + cracked.ToString().PadRight(10) + scabbing.ToString().PadRight(10) +
                patches.ToString().PadRight(9) + potholes.ToString().PadRight(9) + extra1.ToString().PadRight(8) + extra2.ToString().PadRight(8) +
                extra3.ToString().PadRight(8) + extra4.ToString().PadRight(8) + extra5.ToString().PadRight(8) + extra6.ToString().PadRight(8) +
                footpathRatingID.ToString().PadRight(20) + calculatedPriority.ToString().PadRight(20) + enteredPriority.ToString().PadRight(20) +
                calculatedCost.ToString().PadRight(20) + enteredCost.ToString().PadRight(20) + warning.PadRight(30) +
                priorityNotes.PadRight(20) + inspectionStart.ToString().PadRight(20) + inspectionEnd.ToString().PadRight(20) +
                survey2.ToString().PadRight(10) + latest.ToString().PadRight(10) + latest2.ToString().PadRight(10) + side2.ToString().PadRight(5) +
                footpathSurfaceMaterial3.PadRight(27) + notes.PadRight(150) + rater.PadRight(7) +
                surveyMethod.PadRight(15) + surveyMethod2.PadRight(15) + editSurveyData.ToString().PadRight(20) + editSurveyData2.PadRight(35) +
                mapDesc1.PadRight(30) + dateAdded.ToShortDateString().PadRight(15) + addedBy.PadRight(10) +
                dateChanged.ToString().PadRight(20) + changedBy.PadRight(10);
        }

        public string PrintDataShort()
        {
            conditionRating = calcConditionRating();
            //calc length (longer of 2) at some point
            int tempLength = 0;
            if (length1 > length2)
                tempLength = length1;
            if (length1 < length2)
                tempLength = length2;
            else
                tempLength = length1;

            return roadName.PadRight(35) + start.ToString().PadRight(10) + end.ToString().PadRight(10) +
                tempLength.ToString().PadRight(7) + dateAdded.ToShortDateString().PadRight(15)
                + side.PadRight(7) + footpathSurfaceMaterial.PadRight(27) + numFaults.ToString().PadRight(10) + conditionRating.ToString().PadRight(20);
        }

        public int GetRoad()
        {
            return this.road;
        }

        public string GetRoadName()
        {
            return this.roadName;
        }

        public int GetStart()
        {
            return this.start;
        }

        public string GetLocalityName()
        {
            return this.localityName;
        }

        public int GetLocalityID()
        {
            return this.localityID;
        }

        public string GetDisplacement()
        {
            return this.displacement;
        }

        public int GetEnd()
        {
            return this.end;
        }

        public int GetFootpath1()
        {
            return this.footpath1;
        }

        public char GetFootPath2()
        {
            return this.footpath2;
        }

        public string GetFootpathSurfaceMaterial()
        {
            return this.footpathSurfaceMaterial;
        }

        public string GetInspection()
        {
            return this.inspection;
        }

        public string GetSurveyDescription()
        {
            return this.surveyDescription;
        }

        public int GetLength1()
        {
            return this.length1;
        }

        public int GetLength2()
        {
            return this.length2;
        }

        public string GetSide()
        {
            return this.side;
        }

        public string GetSurvey()
        {
            return this.survey;
        }

        public DateTime GetDate()
        {
            return this.date;
        }

        public string GetFootpathSurfaceMaterial2()
        {
            return this.footpathSurfaceMaterial2;
        }

        public int GetSettlement()
        {
            return this.settlement;
        }

        public int GetBumps()
        {
            return this.bumps;
        }

        public int GetDepressions()
        {
            return this.depressions;
        }

        public int GetCracked()
        {
            return this.cracked;
        }

        public int GetScabbing()
        {
            return this.scabbing;
        }

        public int GetPatches()
        {
            return this.patches;
        }

        public int GetPotholes()
        {
            return this.potholes;
        }

        public int GetExtra1()
        {
            return this.extra1;
        }

        public int GetExtra2()
        {
            return this.extra2;
        }

        public int GetExtra3()
        {
            return this.extra3;
        }

        public int GetExtra4()
        {
            return this.extra4;
        }

        public int GetExtra5()
        {
            return this.extra5;
        }

        public int GetExtra6()
        {
            return this.extra6;
        }

        public int GetFootpahthRatingID()
        {
            return this.footpathRatingID;
        }

        public int GetCalculatedPriority()
        {
            return this.calculatedPriority;
        }

        public int GetEnteredPriority()
        {
            return this.enteredPriority;
        }

        public float GetCalculatedCost()
        {
            return this.calculatedCost;
        }

        public float GetEnteredCost()
        {
            return this.enteredCost;
        }

        public string GetWarning()
        {
            return this.warning;
        }

        public string GetPriorityNotes()
        {
            return this.priorityNotes;
        }

        public int GetInspectionStart()
        {
            return this.inspectionStart;
        }

        public int GetInspectionEnd()
        {
            return this.inspectionEnd;
        }

        public int GetSurvey2()
        {
            return this.survey2;
        }

        public char GetLatest()
        {
            return this.latest;
        }

        public string GetLatest2()
        {
            return this.latest2;
        }

        public char GetSide2()
        {
            return this.side2;
        }

        public string GetFootpathMaterial3()
        {
            return this.footpathSurfaceMaterial3;
        }

        public string GetNotes()
        {
            return this.notes;
        }

        public string GetRater()
        {
            return this.rater;
        }

        public string GetSurveyMethod()
        {
            return this.surveyMethod;
        }

        public string GetSurveyMethod2()
        {
            return surveyMethod2;
        }

        public char GetEditSurveyData()
        {
            return this.editSurveyData;
        }

        public string GetEditSurveyData2()
        {
            return this.editSurveyData2;
        }

        public string GetMapDesc1()
        {
            return this.mapDesc1;
        }

        public DateTime GetDateAdded()
        {
            return this.dateAdded;
        }

        public string GetAddedBy()
        {
            return this.addedBy;
        }

        public string GetDateChanged()
        {
            return this.dateChanged;
        }

        public string GetChangedBy()
        {
            return this.changedBy;
        }

        public int GetNumFaults()
        {
            return this.numFaults;
        }

        public void SetQgisData(string[] data)
        {
            qgisData = data;
            Console.WriteLine(qgisData[0] + " " + qgisData[1] + " " + qgisData[2] + " " + qgisData[5] + " " + qgisData[10] + " " + qgisData[20] + " " + qgisData[22]);
        }

        public string[] GetQgisData()
        {
            return this.qgisData;
        }

        private int CalcFaults()
        {
            int faults = 0;

            if (bumps == -1)
                bumps = 0;
            if (depressions == -1)
                depressions = 0;
            if (potholes == -1)
                potholes = 0;
            if (cracked == -1)
                cracked = 0;
            if (scabbing == -1)
                scabbing = 0;
            if (patches == -1)
                patches = 0;

            faults = bumps + depressions + potholes + cracked + scabbing + patches;

            return faults;
        }

        private int calcConditionRating()
        {
            int rating = 0;
            try
            {
                if (qgisData != null)
                {
                    if (double.Parse(qgisData[20]) > 0)
                    {
                        rating += 20;
                    }
                    if (double.Parse(qgisData[22]) > 0)
                    {
                        rating += 25;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rating;
        }

        public int GetLongLength()
        {
            if (length1 > length2)
                return length1;
            else if (length2 > length1)
                return length2;
            else
                return length1;
        }
    }
}
