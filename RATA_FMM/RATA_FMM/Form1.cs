using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;
using System.IO;

namespace RATA_FMM
{


    public partial class Form1 : Form
    {
        const string FILTER = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
        List<Road> roadList = new List<Road>();
        List<string[]> qgisData = new List<string[]>();

        static string[] MAINTENANCE_CODES = { "a", "b", "c", "d", "e", "f" };
        static string[] MAINTENANCE_FAULTS = { "Trip Hazard", "Vertical Displacement", "Horizontal Displacement", "Broken", "Hole", "Poor Previous Reinstatement" };

        int window_length = Screen.PrimaryScreen.Bounds.Width;
        int window_height = Screen.PrimaryScreen.Bounds.Height;

        public Form1()
        {
            InitializeComponent();
            //setting sizes and positions of listboxes
            listBoxData.Width = window_length / 3 - 50;
            listBoxData.Height = window_height - 100;
            listBoxData.Location = new System.Drawing.Point(10, 30);

            labelReplacement.Location = new System.Drawing.Point((window_length / 3 - 30), 30);
            listBoxReplacement.Height = window_height / 2 - 90;
            listBoxReplacement.Width = window_length / 3 - 50;
            listBoxReplacement.Location = new System.Drawing.Point((window_length / 3 - 30), 45);

            labelMaintenance.Location = new System.Drawing.Point((window_length / 3 - 30), (window_height / 2 - 45));
            listBoxMaintenance.Height = window_height / 2 - 30;
            listBoxMaintenance.Width = window_length / 3 - 50;
            listBoxMaintenance.Location = new System.Drawing.Point((window_length / 3 - 30), (window_height / 2 - 30));

            //column headers for first listbox
            /*listBoxData.Items.Add("Road".PadRight(10) + "Road Name".PadRight(35) + "Start".PadRight(10) +
                "Locality Name".PadRight(35) + "Locality ID".PadRight(15) + "Displacement".PadRight(15) +
                "End".PadRight(10) + "Footpath".PadRight(12) + "Footpath".PadRight(12) + "Footpath Surface Material".PadRight(27) +
                "Inspection".PadRight(15) + "Survey Description".PadRight(20) + "Length".PadRight(7) + "Length".PadRight(7) +
                "Side".PadRight(7) + "Survey".PadRight(25) + "Date".PadRight(15) + "Settlement".PadRight(12) +
                "Bumps".PadRight(7) + "Depressions".PadRight(13) + "Cracked".PadRight(10) + "Scabbing".PadRight(10) +
                "Patches".PadRight(9) + "Potholes".PadRight(9) + "Extra1".PadRight(8) + "Extra2".PadRight(8) +
                "Extra3".PadRight(8) + "Extra4".PadRight(8) + "Extra5".PadRight(8) + "Extra6".PadRight(8) +
                "Footpath Rating ID".PadRight(20) + "Calculated Priority".PadRight(20) + "Entered Priority".PadRight(20) +
                "Calculated Cost".PadRight(20) + "Entered Cost".PadRight(20) + "Warning".PadRight(30) +
                "Priority Notes".PadRight(20) + "Inspection Start".PadRight(20) + "Inspection End".PadRight(20) +
                "Survey".PadRight(10) + "Latest".PadRight(10) + "Latest".PadRight(10) + "Side".PadRight(5) +
                "Footpath Surface Material".PadRight(27) + "Notes".PadRight(150) + "Rater".PadRight(7) +
                "Survey Method".PadRight(15) + "Survey Method".PadRight(15) + "Edit Survey Data".PadRight(20) + "Edit Survey Data".PadRight(35) +
                "Map Desc 1".PadRight(30) + "Date Added".PadRight(15) + "Added By".PadRight(10) +
                "Date Changed".PadRight(15) + "Changed By".PadRight(10));*/

            listBoxData.Items.Add("Road Name".PadRight(35) + "Start".PadRight(10) + "End".PadRight(10) + "Length".PadRight(7) +
                "Date Added".PadRight(15) + "Side".PadRight(7) + "Footpath Surface Material".PadRight(27) + "Faults".PadRight(10) + "Condition Rating".PadRight(20));

            //reading file
            StreamReader reader;
            string line = "";
            string[] csvArray;

            reader = File.OpenText("Zone Data (QGIS).csv");
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                csvArray = line.Split(',');
                //0 - road id
                //1 - start
                //2 - end
                //5 - length
                //10 - age
                //19 - service area
                //21 - school area
                //23 - health area
                qgisData.Add(csvArray);
            }
            reader.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = FILTER;

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    OpenExcelFile(openFileDialog1.FileName);
                    DisplayData();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error:" + error.Message);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenExcelFile(string filename)
        {
            try
            {
                //Open file and get first workbook
                Workbook wb = new Workbook(filename);
                Worksheet ws = wb.Worksheets[0];

                //Get cells from worksheet and number of rows and columns from worksheet
                Cells cells = ws.Cells;
                int numRows = cells.MaxDataRow;
                int numColumns = cells.MaxDataColumn;

                // Current cell value
                string cellContents = "";

                for (int i = 1; i <= numRows; i++) // Numeration starts from 0 to MaxDataRow
                {
                    string[] dataArray = new string[56];

                    for (int j = 0; j <= numColumns; j++)  // Numeration starts from 0 to MaxDataColumn
                    {
                        cellContents = "";
                        cellContents = Convert.ToString(cells[i, j].Value);
                        if (String.IsNullOrEmpty(cellContents))
                        {
                            continue;
                        }
                        else
                        {
                            dataArray[j] = cellContents;
                        }
                    }
                    Road r = new Road(dataArray);
                    roadList.Add(r);

                    //find matching data in qgis data
                    //potentially add date check
                    for (int k = 0; k <= qgisData.Count - 1; k++)
                    {
                        string[] data = qgisData[k];
                        if (r.GetRoadName() == data[0])
                        {
                            if (r.GetStart() == int.Parse(data[1]) && r.GetEnd() == int.Parse(data[2]))
                            {
                                r.SetQgisData(data);                                
                                break;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DisplayData()
        {         
            roadList.Sort((x, y) => 
            {
                var ret = y.GetConditionRating().CompareTo(x.GetConditionRating());
                if (ret == 0)
                    ret = y.GetNumFaults().CompareTo(x.GetNumFaults());
                return ret;
            });
            //displaying in first listbox
            foreach (Road r in roadList)
            {
                listBoxData.Items.Add(r.PrintDataShort());
            }
        }

        /// <summary>
        /// Takes the maintenance code and returns the maintenance fault as a string
        /// </summary>
        /// <param name="c">The maintenance code</param>
        /// <returns>The maintenance fault</returns>
        private string getMaintenanceFault(string c)
        {
            string fault = "";

            for (int i = 0; i < MAINTENANCE_CODES.Length; i++)
            {
                if (c == MAINTENANCE_CODES[i]) //If the code being searched on matches the code at the current position
                {
                    fault = MAINTENANCE_FAULTS[i]; //Assign it the appropriate maintenance fault
                }
            }

            return fault; //Return the fault
        }

        /// <summary>
        /// Ranks data entries based on severity, with 5 being prioritized first and 1 last
        /// Very crude ranking implementation at present
        /// </summary>
        private void rankOnSeverity()
        {
            List<string[]> footpaths = new List<string[]>(); //A list to hold all footpaths

            //Some temporary dummy entries
            string[] dummyEntryOne = { "Albert Street", "5" };
            string[] dummyEntryTwo = { "Anzac Street", "4" };
            string[] dummyEntryThree = { "Hurley Place", "6" };
            string[] dummyEntryFour = { "Queen Street", "2" };
            string[] dummyEntryFive = { "Shakespeare Street", "1" };
            string[] dummyEntrySix = { "Taylor Street", "1" };
            string[] dummyEntrySeven = { "Thornton Road", "2" };
            string[] dummyEntryEight = { "Victoria Street", "3" };
            string[] dummyEntryNine = { "Cotter Place", "4" };
            string[] dummyEntryTen = { "Wallace Terrace", "5" };

            //Adding dummy entries to footpath list
            footpaths.Add(dummyEntryOne);
            footpaths.Add(dummyEntryTwo);
            footpaths.Add(dummyEntryThree);
            footpaths.Add(dummyEntryFour);
            footpaths.Add(dummyEntryFive);
            footpaths.Add(dummyEntrySix);
            footpaths.Add(dummyEntrySeven);
            footpaths.Add(dummyEntryEight);
            footpaths.Add(dummyEntryNine);
            footpaths.Add(dummyEntryTen);

            footpaths = footpaths.OrderByDescending(arr => arr[1]).ToList(); //Sort the footpath list from highest severity to lowest

            //Output the footpath report to the console window
            Console.WriteLine("Footpath Severity Report:");
            for (int i = 0; i < footpaths.Count; i++)
            {
                string[] currFootpath = footpaths[i];
                Console.WriteLine(currFootpath[0] + " with severity " + currFootpath[1]); //Output street name and severity
            }
        }

        /// <summary>
        /// Prints report to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = " ";

            text += "Replacements\n";
            text += "\n<Insert footpaths to be replaced, ranked on severity>\n\n";
            text += "Maintenance\n";
            text += "\n<Insert footpaths to maintain, ranked on severity>\n";

            PCPrint printer = new PCPrint(text);
            printer.PrinterFont = new System.Drawing.Font("Times New Roman", 14);
            printer.PrinterSettings.PrintToFile = true;
            printer.Print();
        }
    }
}
