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
        List<Road> roadList = new List<Road>(); //The list of all results
        List<Road> filteredFootpaths = new List<Road>(); //The list of filtered results
        List<string[]> qgisData = new List<string[]>();

        int window_length = Screen.PrimaryScreen.Bounds.Width;
        int window_height = Screen.PrimaryScreen.Bounds.Height;

        static bool dataProcessed = false; //Stores whether the data has been processed yet

        public Form1()
        {
            InitializeComponent();

            //setting sizes and positions of listboxes
            listBoxData.Width = window_length / 3 + 50;
            listBoxData.Height = window_height - 100;
            listBoxData.Location = new Point(10, 30);

            listBoxDataLong.Width = window_length / 3 - 50;
            listBoxDataLong.Height = window_height / 2 - 100;
            listBoxDataLong.Location = new Point(window_length / 3 + 70, 30);

            //setting locations of filter window components
            labelFilterResults.Location = new Point(window_length / 3 * 2 + 50, 35);
            pictureBoxFilter.Location = new Point(window_length / 3 * 2 + 30, 30);
            pictureBoxFaults.Location = new Point((window_length / 3 * 2) + 50, 55);
            pictureBoxCondition.Location = new Point(window_length / 3 * 2 + 50, 135);
            pictureBoxTown.Location = new Point(window_length / 3 * 2 + 50, 215);

            filterLabel1.Location = new Point(window_length / 3 * 2 + 55, 60);
            filterLabel2.Location = new Point(window_length / 3 * 2 + 55, 80);
            textBoxFilterCondition.Location = new Point(window_length / 3 * 2 + 105, 77);
            filterLabel3.Location = new Point(window_length / 3 * 2 + 150, 80);            
            filterLabel4.Location = new Point(window_length / 3 * 2 + 55, 140);
            textBoxFilterFaults.Location = new Point(window_length / 3 * 2 + 162, 139);
            filterLabel5.Location = new Point(window_length / 3 * 2 + 55, 160);
            filterLabel6.Location = new Point(window_length / 3 * 2 + 55, 220);
            comboBoxTown.Location = new Point(window_length / 3 * 2 + 60, 240);

            buttonUpdateResults.Location = new Point(window_length / 3 * 2 + 50, 280);
            buttonShowAll.Location = new Point(window_length / 3  * 2 + 135, 280);

            //setting location of algorithm weighting components
            pictureBoxSort.Location = new Point(window_length / 3 + 70, window_height / 2 - 70);
            pictureBoxZones.Location = new Point(window_length / 3 + 78, window_height /2 - 45);
            pictureBoxPathRatings.Location = new Point(window_length / 3 + 145, window_height / 2 + 80);

            labelAlgorithm.Location = new Point(window_length / 3 + 235, window_height / 2 - 65);
            labelZones.Location = new Point(window_length / 3 + 270, window_height / 2 - 42);
            labelHealthMin.Location = new Point(window_length / 3 + 85, window_height / 2 - 25);
            labelSchoolMin.Location = new Point(window_length / 3 + 85, window_height / 2 + 5);
            labelServiceMin.Location = new Point(window_length / 3 + 85, window_height / 2 + 35);
            labelHealthMax.Location = new Point(window_length / 3 + 295, window_height / 2 - 25);
            labelSchoolMax.Location = new Point(window_length / 3 + 295, window_height / 2 + 5);
            labelServiceMax.Location = new Point(window_length / 3 + 295, window_height / 2 + 35);

            textBoxHealthMin.Location = new Point(window_length / 3 + 200, window_height / 2 - 27);
            textBoxSchoolMin.Location = new Point(window_length / 3 + 200, window_height / 2 + 3);
            textBoxServiceMin.Location = new Point(window_length / 3 + 200, window_height / 2 + 33);
            textBoxHealthMax.Location = new Point(window_length / 3 + 415, window_height / 2 - 27);
            textBoxSchoolMax.Location = new Point(window_length / 3 + 415, window_height / 2 + 3);
            textBoxServiceMax.Location = new Point(window_length / 3 + 415, window_height / 2 + 33);

            labelPathRatings.Location = new Point(window_length / 3 + 235, window_height / 2 + 85);
            labelRating1.Location = new Point(window_length / 3 + 155, window_height / 2 + 105);
            labelRating2.Location = new Point(window_length / 3 + 235, window_height / 2 + 105);
            labelRating3.Location = new Point(window_length / 3 + 315, window_height / 2 + 105);
            labelRating4.Location = new Point(window_length / 3 + 200, window_height / 2 + 135);
            labelRating5.Location = new Point(window_length / 3 + 280, window_height / 2 + 135);

            textBoxRating1.Location = new Point(window_length / 3 + 170, window_height / 2 + 103);
            textBoxRating2.Location = new Point(window_length / 3 + 250, window_height / 2 + 103);
            textBoxRating3.Location = new Point(window_length / 3 + 330, window_height / 2 + 103);
            textBoxRating4.Location = new Point(window_length / 3 + 215, window_height / 2 + 133);
            textBoxRating5.Location = new Point(window_length / 3 + 295, window_height / 2 + 133);

            buttonUpdateAlgorithm.Location = new Point(window_length / 3 + 218, window_height / 2 + 175);
            buttonReset.Location = new Point(window_length / 3 + 298, window_height / 2 + 175);

            //addding items to combobox
            comboBoxTown.Items.Add("Cambridge");
            comboBoxTown.Items.Add("Hamilton");
            comboBoxTown.Items.Add("Karapiro");
            comboBoxTown.Items.Add("Kihikihi");
            comboBoxTown.Items.Add("Ohaupo");
            comboBoxTown.Items.Add("Pirongia");
            comboBoxTown.Items.Add("Te Awamutu");
            comboBoxTown.Items.Add("Other");

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

            initializeDataListBox();

            //reading file
            StreamReader reader;
            string line = "";
            string[] csvArray;

            reader = File.OpenText("Final QGIS DATA.csv");
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
                    //potentially add date check
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
            /*roadList.Sort((x, y) => 
            {
                var ret = y.GetConditionRating().CompareTo(x.GetConditionRating());
                if (ret == 0)
                    ret = y.GetFootpathCondition().CompareTo(x.GetFootpathCondition());
                return ret;
            });*/
            SortList();
            //displaying in first listbox
            foreach (Road r in roadList)
            {
                listBoxData.Items.Add(r.PrintDataShort());
            }
            filteredFootpaths = new List<Road>(roadList); //Until filters have been applied, the list of filtered footpaths is just all footpaths
        }

        /// <summary>
        /// Prints report to file
        /// </summary>
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string templateName = Directory.GetCurrentDirectory() + "\\" + "reportTemplate.dotx"; //Get the directory of the report template

            SaveFileDialog savePrintedDocument = new SaveFileDialog();
            savePrintedDocument.Title = "Save report";
            savePrintedDocument.Filter = "Word Document|*.docx";

            if (savePrintedDocument.ShowDialog() == DialogResult.OK)
            {
                string saveAs = savePrintedDocument.FileName;

                WordPrint printer = new WordPrint(templateName, saveAs);
                printer.printFromTemplate(filteredFootpaths); //Print to report using the list of filtered footpaths
            }
            else
            {
                MessageBox.Show("Error: Invalid file selected");
            }
        }

        /// <summary>
        /// Updates the results to display only data that match certain user-specified filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdateResults_Click(object sender, EventArgs e)
        {
            if (dataProcessed) //There is data to filter on
            {
                int numFaults = 0;
                int conditionRating = 0;

                if (!int.TryParse(textBoxFilterFaults.Text, out numFaults)) //Error handling - invalid input
                {
                    MessageBox.Show("Error: Please enter a valid number of faults to filter on");
                    textBoxFilterFaults.Text = "0";
                    return;
                }
                else if (numFaults < 0) //Error handling - number of faults is less than 0
                {
                    MessageBox.Show("Error: Number of faults cannot be less than 0");
                    textBoxFilterFaults.Text = "0";
                    return;
                }

                if (!int.TryParse(textBoxFilterCondition.Text, out conditionRating)) //Error handling - invalid input
                {
                    MessageBox.Show("Error: Please enter a valid condition rating to filter on");
                    textBoxFilterCondition.Text = "0";
                    return;
                }
                else if (conditionRating < 0) //Error handling - condition rating is less than 0
                {
                    MessageBox.Show("Error: Condition rating cannot be less than 0");
                    textBoxFilterCondition.Text = "0";
                    return;
                }

                initializeDataListBox();
                filteredFootpaths = filterResults(numFaults, conditionRating);

                foreach (Road r in filteredFootpaths)
                {
                    listBoxData.Items.Add(r.PrintDataShort());
                }

            }
        }

        /// <summary>
        /// Initializes maintenance list box
        /// </summary>
        private void initializeDataListBox()
        {
            listBoxData.Items.Clear();
            listBoxData.Items.Add("Road Name".PadRight(35) + "Length".PadRight(10) + "Faults".PadRight(10) + "Condition Rating".PadRight(20) + "Footpath Rating".PadRight(15));
        }

        /// <summary>
        /// Applies filters to the whole road list
        /// </summary>
        /// <param name="numFaults">The number of faults to filter on</param>
        /// /// <param name="conditionRating">The condition rating to filter on</param>
        /// <returns>The new road list that conforms to the applied filters</returns>
        private List<Road> filterResults (int numFaults, int conditionRating)
        {
            bool filterOnFaults = numFaults != 0; //Check if the user specified a number of faults
            bool filterOnCondition = conditionRating != 0; //Check if the user specified a condition rating

            List<Road> filteredFootpaths = new List<Road>(roadList); //The list of filtered footpaths to be returned
            List<Road> temporaryList; //Temporary list which allows for cumulative processing

            if (numFaults > 0) //The user has specified a filter for the number of faults
            {
                temporaryList = new List<Road>();

                foreach (Road checkFaultFilter in filteredFootpaths)
                {
                    if (checkFaultFilter.GetNumFaults() >= numFaults) //The current footpath exceeds or meets the number of faults
                    {
                        temporaryList.Add(checkFaultFilter); 
                    }
                }

                filteredFootpaths = new List<Road>(temporaryList); //Set the new filtered list to the list of footpaths meeting the current condition
                temporaryList = null; //Set the temporary list to null
            }
            
            if (conditionRating > 0) //The user has specified a filter for the condition rating
            {
                temporaryList = new List<Road>();

                foreach (Road checkConditionFilter in filteredFootpaths)
                {  
                    if (checkConditionFilter.GetConditionRating() >= conditionRating) //The current footpath exceeds or meets the condition rating
                    {
                        temporaryList.Add(checkConditionFilter);
                    }
                }

                filteredFootpaths = new List<Road>(temporaryList); //Set the new filtered list to the list of footpaths meeting the current condition
                temporaryList = null; //Set the temporary list to null
            }

            if (comboBoxTown.Text != "") //The user has specified a filter for the town name
            {
                temporaryList = new List<Road>();

                foreach (Road checkTown in filteredFootpaths)
                {
                    string town = checkTown.GetTown();
                    
                    if (checkTown.GetTown().Equals(comboBoxTown.Text))  //The current footpath matches the selected town name
                    {
                        temporaryList.Add(checkTown);
                    }
                }

                filteredFootpaths = new List<Road>(temporaryList); //Set the new filtered list to the list of footpaths meeting the current condition
                temporaryList = null; //Set the temporary list to null
            }

            return filteredFootpaths; //Return the list that matches the required filter(s)
        }

        /// <summary>
        /// Shows all of the results in the maintenance list box. Used after user has applied filters to the results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowAll_Click(object sender, EventArgs e)
        {
            initializeDataListBox(); //Clear the list box and show the headers

            foreach (Road r in roadList) //Add back each of the footpaths
            {
                listBoxData.Items.Add(r.PrintDataShort());
            }

            filteredFootpaths = new List<Road>(roadList); //Reset the list storing all the filtered results

            //Set the text box values back to zero
            textBoxFilterCondition.Text = "0"; 
            textBoxFilterFaults.Text = "0";
            comboBoxTown.Text = "";
        }

        private void listBoxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxData.SelectedIndex;
            if (index > 0)
            {
                listBoxDataLong.DataSource = filteredFootpaths[index - 1].GetRoadDataAsList();
            }
        }

        private void buttonUpdateAlgorithm_Click(object sender, EventArgs e)
        {
            int healthMin = int.Parse(textBoxHealthMin.Text);
            int healthMax = int.Parse(textBoxHealthMax.Text);
            int schoolMin = int.Parse(textBoxSchoolMin.Text);
            int schoolMax = int.Parse(textBoxSchoolMax.Text);
            int serviceMin = int.Parse(textBoxServiceMin.Text);
            int serviceMax = int.Parse(textBoxServiceMax.Text);
            int rating1 = int.Parse(textBoxRating1.Text);
            int rating2 = int.Parse(textBoxRating2.Text);
            int rating3 = int.Parse(textBoxRating3.Text);
            int rating4 = int.Parse(textBoxRating4.Text);
            int rating5 = int.Parse(textBoxRating5.Text);

            initializeDataListBox();
            listBoxData.ClearSelected();
            foreach (Road r in roadList)
                r.CalcConditionRating(healthMin, healthMax, schoolMin, schoolMax, serviceMin, serviceMax, rating1, rating2, rating3, rating4, rating5);                               
            SortList();
            foreach(Road r in roadList)
                listBoxData.Items.Add(r.PrintDataShort());
            ClearTextBoxes();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            initializeDataListBox();
            listBoxData.ClearSelected();
            foreach (Road r in roadList)
                r.CalcConditionRating(30, 40, 15, 30, 10, 25, 5, 15, 30, 45, 60);
            SortList();
            foreach (Road r in roadList)
                listBoxData.Items.Add(r.PrintDataShort());
            ClearTextBoxes();
        }

        public void SortList()
        {
            roadList.Sort((x, y) =>
            {
                var ret = y.GetConditionRating().CompareTo(x.GetConditionRating());
                if (ret == 0)
                    ret = y.GetFootpathCondition().CompareTo(x.GetFootpathCondition());
                return ret;
            });
        }

        public void ClearTextBoxes()
        {
            textBoxHealthMin.Clear();
            textBoxHealthMax.Clear();
            textBoxSchoolMin.Clear();
            textBoxSchoolMax.Clear();
            textBoxServiceMin.Clear();
            textBoxServiceMax.Clear();
            textBoxRating1.Clear();
            textBoxRating2.Clear();
            textBoxRating3.Clear();
            textBoxRating4.Clear();
            textBoxRating5.Clear();
        }
    }
}
