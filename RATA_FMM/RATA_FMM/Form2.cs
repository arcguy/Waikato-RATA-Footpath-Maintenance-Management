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
using GMap.NET;
using MetroFramework;
using MetroFramework.Forms;

namespace RATA_FMM
{
    public partial class Form2 : MetroForm
    {
        const string FILTER = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
        List<Road> roadList = new List<Road>(); //The list of all results
        List<Road> filteredFootpaths = new List<Road>(); //The list of filtered results
        List<string[]> qgisData = new List<string[]>();
        List<string> columnHeaders = new List<string>() {"Road Name", "Start", "End", "Length", "Date Added", "Side", "Footpath Surface Material", "Number of Faults",
        "Condition Rating", "Footpath Condition", "Town:", "Fault to Length Ratio", "Fault Information", "Zone Information", "Zone Information", "Zone Information"};

        int window_length = Screen.PrimaryScreen.Bounds.Width;
        int window_height = Screen.PrimaryScreen.Bounds.Height;

        static bool dataProcessed = false; //Stores whether the data has been processed yet

        public Form2()
        {
            InitializeComponent();

            //metro list view data
            metroListViewData.Width = window_length / 3 + 50;
            metroListViewData.Height = window_height - 120;
            metroListViewData.Location = new Point(10, 60);

            metroListViewDataLong.Width = window_length / 3 + 230;
            metroListViewDataLong.Height = window_height / 2 - 100;
            metroListViewDataLong.Location = new Point(metroListViewData.Right + 10, 30);

            //setting locations of filter window components
            metroLabelFilterResults.Location = new Point((metroPanelFilter.Width / 2) - (metroLabelFilterResults.Width / 2), 0);
            metroPanelFilter.Location = new Point(metroListViewDataLong.Right + 10, 30);

            metroButtonUpdateResults.Location = new Point((metroPanelFilter.Width / 2) - (metroButtonUpdateResults.Width / 2), metroPanelFilter.Height - metroButtonShowAll.Height - metroButtonUpdateResults.Height - 20);
            metroButtonShowAll.Location = new Point((metroPanelFilter.Width / 2) - (metroButtonShowAll.Width / 2), metroPanelFilter.Height - metroButtonShowAll.Height - 10);

            //setting location of algorithm weighting components
            metroPanelSort.Width = window_length / 3 - 50;
            metroPanelSort.Location = new Point(metroListViewData.Right + 10, metroListViewDataLong.Bottom + 10);

            metroLabelAlgorithm.Location = new Point((metroPanelSort.Width / 2) - (metroLabelAlgorithm.Width / 2), 0);
            metroLabelZones.Location = new Point((metroPanelSort.Width / 2) - (metroLabelZones.Width / 2), metroLabelAlgorithm.Height + 2);

            metroLabelPathRatings.Location = new Point((metroPanelSort.Width / 2) - (metroLabelPathRatings.Width / 2), metroTextBoxRating2.Location.Y - 30);

            metroButtonUpdateAlgorithm.Location = new Point((metroPanelSort.Width / 2) - 100, metroPanelSort.Height - metroButtonUpdateAlgorithm.Height - 25);
            metroButtonReset.Location = new Point((metroPanelSort.Width / 2) - 75 + metroButtonReset.Width, metroPanelSort.Height - metroButtonUpdateAlgorithm.Height - 25);

            //setting size and position of map control
            gMapControl1.Width = window_length - metroPanelSort.Width - metroListViewData.Width - 50;
            gMapControl1.Height = window_height / 2;
            gMapControl1.Location = new Point(metroPanelSort.Right + 10, metroListViewDataLong.Bottom + 10);

            //addding items to combobox
            metroComboBoxTown.Items.Add("Cambridge");
            metroComboBoxTown.Items.Add("Hamilton");
            metroComboBoxTown.Items.Add("Karapiro");
            metroComboBoxTown.Items.Add("Kihikihi");
            metroComboBoxTown.Items.Add("Ohaupo");
            metroComboBoxTown.Items.Add("Pirongia");
            metroComboBoxTown.Items.Add("Te Awamutu");
            metroComboBoxTown.Items.Add("Other");

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

        private void metroButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = FILTER;

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    OpenExcelFile(openFileDialog1.FileName);
                    DisplayData(roadList);
                    filteredFootpaths = new List<Road>(roadList); //Until filters have been applied, the list of filtered footpaths is just all footpaths
                    dataProcessed = true;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error:" + error.Message);
            }
        }

        private void metroButtonPrint_Click(object sender, EventArgs e)
        {
            string templateName = Directory.GetCurrentDirectory() + "\\" + "reportTemplate.dotx"; //Get the directory of the report template
            string saveAs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "report.docx";

            WordPrint printer = new WordPrint(templateName, saveAs);
            printer.printFromTemplate(filteredFootpaths); //Print to report using the list of filtered footpaths
        }

        private void metroButtonUpdateResults_Click(object sender, EventArgs e)
        {
            if (dataProcessed) //There is data to filter on
            {
                int numFaults = 0;
                int conditionRating = 0;

                if (!int.TryParse(metroTextBoxFilterFaults.Text, out numFaults)) //Error handling - invalid input
                {
                    MetroMessageBox.Show(this, "Please enter a valid number of faults to filter on", "Error");
                    metroTextBoxFilterFaults.Text = "0";
                    return;
                }
                else if (numFaults < 0) //Error handling - number of faults is less than 0
                {
                    MetroMessageBox.Show(this,"Number of faults cannot be less than 0", "Error");
                    metroTextBoxFilterFaults.Text = "0";
                    return;
                }

                if (!int.TryParse(metroTextBoxFilterCondition.Text, out conditionRating)) //Error handling - invalid input
                {
                    MetroMessageBox.Show(this, "Please enter a valid condition rating to filter on", "Error");
                    metroTextBoxFilterCondition.Text = "0";
                    return;
                }
                else if (conditionRating < 0) //Error handling - condition rating is less than 0
                {
                    MetroMessageBox.Show(this, "Condition rating cannot be less than 0", "Error");
                    metroTextBoxFilterCondition.Text = "0";
                    return;
                }

                initializeDataListBox();
                filteredFootpaths = filterResults(numFaults, conditionRating);

                DisplayData(filteredFootpaths);
            }
        }

        private void metroButtonShowAll_Click(object sender, EventArgs e)
        {
            DisplayData(roadList);

            filteredFootpaths = new List<Road>(roadList); //Reset the list storing all the filtered results

            //Set the text box values back to zero
            metroTextBoxFilterCondition.Text = "0";
            metroTextBoxFilterFaults.Text = "0";
            metroComboBoxTown.Text = "";
        }

        private void metroButtonUpdateAlgorithm_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";

                int healthMin = int.Parse(metroTextBoxHealthMin.Text);
                int healthMax = int.Parse(metroTextBoxHealthMax.Text);
                int schoolMin = int.Parse(metroTextBoxSchoolMin.Text);
                int schoolMax = int.Parse(metroTextBoxSchoolMax.Text);
                int serviceMin = int.Parse(metroTextBoxServiceMin.Text);
                int serviceMax = int.Parse(metroTextBoxServiceMax.Text);
                int rating1 = int.Parse(metroTextBoxRating1.Text);
                int rating2 = int.Parse(metroTextBoxRating2.Text);
                int rating3 = int.Parse(metroTextBoxRating3.Text);
                int rating4 = int.Parse(metroTextBoxRating4.Text);
                int rating5 = int.Parse(metroTextBoxRating5.Text);

                if (healthMin >= healthMax)
                    errorMessage += "Health minimum cannot be greater than Health max. \n";
                if (schoolMin >= schoolMax)
                    errorMessage += "School minimum cannot be greater than School max. \n";
                if (serviceMin >= serviceMax)
                    errorMessage += "Service minimum cannot be greater than Service max. \n";

                if (errorMessage == "")
                {
                    initializeDataListBox();
                    foreach (Road r in roadList)
                    {
                        r.CalcConditionRating(healthMin, healthMax, schoolMin, schoolMax, serviceMin, serviceMax, rating1, rating2, rating3, rating4, rating5);
                    }
                    SortList();
                    DisplayData(roadList);
                }
                else
                {
                    MetroMessageBox.Show(this, errorMessage, "Error");
                }
            }
            catch (FormatException) //if a textbox is empty or contains non numerical values
            {
                MetroMessageBox.Show(this, "Please ensure each textbox contains a numerical value", "Error");
            }
        }

        private void metroButtonReset_Click(object sender, EventArgs e)
        {
            initializeDataListBox();
            metroListViewData.Clear();
            foreach (Road r in roadList)
                r.CalcConditionRating(30, 40, 15, 30, 10, 25, 5, 15, 30, 45, 60);
            SortList();
            DisplayData(roadList);
            ClearAlgorithmTextBoxes();
        }

        /// <summary>
        /// opens an excel file and creates a road object with the footpath data from each row
        /// </summary>
        /// <param name="filename"></param>
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Sorts the list and then displays a condensed version of the data in the first listbox
        /// </summary>
        private void DisplayData(List<Road> rList)
        {
            SortList();
            metroListViewData.BeginUpdate();
            metroListViewData.Clear();
            metroListViewData.View = View.Details;

            metroListViewData.Columns.Add("Road Name");
            metroListViewData.Columns.Add("Length");
            metroListViewData.Columns.Add("Faults");
            metroListViewData.Columns.Add("Condition Rating");
            metroListViewData.Columns.Add("Footpath Rating");

            foreach (Road r in rList)
            {
                string PathName = r.GetRoadName();
                int pathLength = r.GetLongLength();
                int pathFaults = r.GetNumFaults();
                double pathCondition = r.GetConditionRating();
                int pathRating = r.GetFootpathCondition();
                ListViewItem lvi = new ListViewItem(new string[] { PathName, pathLength.ToString(), pathFaults.ToString(), pathCondition.ToString(), pathRating.ToString() });
                metroListViewData.Items.Add(lvi);
            }
            metroListViewData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            metroListViewData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            metroListViewData.EndUpdate();
        }

        /// <summary>
        /// Initializes maintenance list box
        /// </summary>
        private void initializeDataListBox()
        {
            metroListViewData.Clear();
        }

        /// <summary>
        /// Applies filters to the whole road list
        /// </summary>
        /// <param name="numFaults">The number of faults to filter on</param>
        /// <param name="conditionRating">The condition rating to filter on</param>
        /// <returns>The new road list that conforms to the applied filters</returns>
        private List<Road> filterResults(int numFaults, int conditionRating)
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

            if (metroComboBoxTown.Text != "") //The user has specified a filter for the town name
            {
                temporaryList = new List<Road>();

                foreach (Road checkTown in filteredFootpaths)
                {
                    string town = checkTown.GetTown();

                    if (checkTown.GetTown().Equals(metroComboBoxTown.Text))  //The current footpath matches the selected town name
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
        /// Implementation of cascading lists to show more detailed information of a footpath in second listbox when one is selected in first listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroListViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            foreach (ListViewItem item in metroListViewData.SelectedItems)
                index = item.Index;
            if (index > -1)
            {
                List<string> tempList = filteredFootpaths[index].GetRoadDataAsList();                
                metroListViewDataLong.BeginUpdate();                
                metroListViewDataLong.Clear();
                metroListViewDataLong.Columns.Add("Footpath Information", 340);
                metroListViewDataLong.Columns.Add("", 510);
                metroListViewDataLong.View = View.Details;
                
                for (int i = 0; i < tempList.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem(new string[] {columnHeaders[i], tempList[i]});
                    metroListViewDataLong.Items.Add(lvi);
                }
                //metroListViewDataLong.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //metroListViewDataLong.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                metroListViewDataLong.EndUpdate();
            }
        }        

        /// <summary>
        /// sorts the list on condition rating, then footpath condition
        /// </summary>
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

        /// <summary>
        /// clears contents of all textboxes in the custom algorithm weighting section of the form
        /// </summary>
        public void ClearAlgorithmTextBoxes()
        {
            metroTextBoxHealthMin.Clear();
            metroTextBoxHealthMax.Clear();
            metroTextBoxSchoolMin.Clear();
            metroTextBoxSchoolMax.Clear();
            metroTextBoxServiceMin.Clear();
            metroTextBoxServiceMax.Clear();
            metroTextBoxRating1.Clear();
            metroTextBoxRating2.Clear();
            metroTextBoxRating3.Clear();
            metroTextBoxRating4.Clear();
            metroTextBoxRating5.Clear();
        }
    }
}
