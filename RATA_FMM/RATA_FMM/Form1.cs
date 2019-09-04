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
using System.Globalization;

namespace RATA_FMM
{


    public partial class Form1 : Form
    {
        const string FILTER = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
        List<Road> roadList = new List<Road>();
        List<Road> filteredFootpaths = new List<Road>();
        List<string[]> qgisData = new List<string[]>();

        static string[] MAINTENANCE_CODES = { "a", "b", "c", "d", "e", "f" };
        static string[] MAINTENANCE_FAULTS = { "Trip Hazard", "Vertical Displacement", "Horizontal Displacement", "Broken", "Hole", "Poor Previous Reinstatement" };

        int window_length = Screen.PrimaryScreen.Bounds.Width;
        int window_height = Screen.PrimaryScreen.Bounds.Height;

        static bool dataProcessed = false;

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
            listBoxData.Items.Add("Road".PadRight(10) + "Road Name".PadRight(35) + "Start".PadRight(10) +
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
                "Date Changed".PadRight(15) + "Changed By".PadRight(10));

            initializeMaintenanceListBox();

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
                //20 - school buffer area
                //22 - health buffer area
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
                    dataProcessed = true;
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
                Aspose.Cells.Cells cells = ws.Cells;
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
                    for (int k = 0; k <= qgisData.Count - 1; k++)
                    {
                        string[] data = qgisData[k];
                        if (r.GetRoadName() == data[0])
                        {
                            string startString = data[1];
                            string endString = data[2];

                            startString = new string(startString.Where(c => char.IsDigit(c)).ToArray());
                            endString = new string(endString.Where(c => char.IsDigit(c)).ToArray());

                            int startInt = int.Parse(startString);
                            int endInt = int.Parse(endString);

                            if (r.GetStart() == startInt && r.GetEnd() == endInt)
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
            //displaying in first listbox
            foreach (Road r in roadList)
            {
                listBoxData.Items.Add(r.ToString());                

                //rankOnSeverity();
            }

            //roadList.Sort((x, y) => y.GetNumFaults().CompareTo(x.GetNumFaults()));
            roadList.Sort((x, y) => 
            {
                var ret = y.GetNumFaults().CompareTo(x.GetNumFaults());
                if (ret == 0) ret = y.GetLongLength().CompareTo(x.GetLongLength());
                return ret;
            });

            foreach (Road r in roadList)
            {
                listBoxMaintenance.Items.Add(r.PrintDataShort());
                //printToFile += r.GetRoadName() + ", " + r.GetStart().ToString() + ", " + r.GetEnd().ToString() + "\n";
            }
            filteredFootpaths = new List<Road>(roadList);
        }

        /// <summary>
        /// Prints report to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string templateName = Directory.GetCurrentDirectory() + "\\" + "reportTemplate.dotx";
            string saveAs = Directory.GetCurrentDirectory() + "\\" + "Report.docx";
            WordPrint printer = new WordPrint(templateName, saveAs);

            printer.printFromTemplate(filteredFootpaths);
        }

        /// <summary>
        /// Updates the results to display only data that match certain user-specified filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdateResults_Click(object sender, EventArgs e)
        {
            if (dataProcessed)
            {
                int numFaults = 0;

                if (!int.TryParse(textBoxFilterDefects.Text, out numFaults))
                {
                    MessageBox.Show("Error: Please enter a valid number of defects to filter on");
                }
                else if (numFaults < 0)
                {
                    MessageBox.Show("Error: Number of defects can not be less than 0");
                }
                else
                {
                    MessageBox.Show("Results have been updated on " + numFaults.ToString() + " defects.");

                    initializeMaintenanceListBox();
                    filteredFootpaths = filterResults(numFaults);

                    foreach (Road r in filteredFootpaths)
                    {
                        listBoxMaintenance.Items.Add(r.PrintDataShort());
                    }
                }
            }
        }

        /// <summary>
        /// Initializes maintenance list box
        /// </summary>
        private void initializeMaintenanceListBox()
        {
            listBoxMaintenance.Items.Clear();

            listBoxMaintenance.Items.Add("Road Name".PadRight(35) + "Start".PadRight(10) + "End".PadRight(10) + "Length".PadRight(7) +
                "Date Added".PadRight(15) + "Side".PadRight(7) + "Footpath Surface Material".PadRight(27) + "Faults".PadRight(10) + "Condition Rating".PadRight(20));
        }

        /// <summary>
        /// Applies filters to the whole road list
        /// </summary>
        /// <param name="numDefects">The number of defects to filter on</param>
        /// <returns>The new road list that conforms to the applied filters</returns>
        private List<Road> filterResults (int numDefects)
        {
            List<Road> filteredFootpaths = new List<Road>();

            if (numDefects != -1)
            {
                foreach (Road r in roadList)
                {
                    if (r.GetNumFaults() >= numDefects)
                    {
                        filteredFootpaths.Add(r);
                    }
                }
            }

            return filteredFootpaths;
        }

        /// <summary>
        /// Shows all of the results in the maintenance list box. Used after user has applied filters to the results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowAll_Click(object sender, EventArgs e)
        {
            initializeMaintenanceListBox();

            foreach (Road r in roadList)
            {
                listBoxMaintenance.Items.Add(r.PrintDataShort());
            }
        }
    }
}
