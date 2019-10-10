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
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Point = System.Drawing.Point;
using NetTopologySuite.Features;
using System.Collections;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using Geometry = NetTopologySuite.Geometries.Geometry;

namespace RATA_FMM
{
    public partial class Form2 : MetroForm
    {
        const string FILTER = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
        List<Road> roadList = new List<Road>(); //The list of all results
        List<Road> filteredFootpaths = new List<Road>(); //The list of filtered results
        List<string[]> qgisData = new List<string[]>();
        //List<double> LongList = new List<double>();
        //List<double> LatList = new List<double>();
        //List<List<double>> ListLatList = new List<List<double>>();
        //List<List<double>> ListLongList = new List<List<double>>();
        List<string> columnHeaders = new List<string>() {"Road Name", "Start", "End", "Length", "Survey Date", "Side", "Footpath Surface Material", "Number of Faults",
        "Condition Rating", "Average Footpath Rating", "Town:", "Fault to Length Ratio", "Fault Information", "Zone Information", "Zone Information", "Zone Information"};

        int window_length = Screen.PrimaryScreen.Bounds.Width;
        int window_height = Screen.PrimaryScreen.Bounds.Height;

        static bool dataProcessed = false; //Stores whether the data has been processed yet

        public Form2()
        {
            InitializeComponent();
            
            //setting tooltips
            toolTip1.SetToolTip(metroLabelHelp, "Modify the values used to calculate the condition rating");

            //setting size and locations of list views
            metroListViewData.Width = window_length / 3;
            metroListViewData.Height = window_height - 120;
            metroListViewData.Location = new Point(10, 60);

            metroListViewDataLong.Width = window_length / 3;
            metroListViewDataLong.Height = window_height / 2 - 100;
            metroListViewDataLong.Location = new Point(metroListViewData.Right + 10, metroButtonOpen.Bottom + 10);

            //setting locations of filter window components
            metroPanelFilter.Width = window_length / 6;
            metroPanelFilter.Height = metroLabelFilterResults.Height + (metroPanelFaults.Height * 4) + (metroButtonUpdateResults.Height * 2) + 80;
            metroLabelFilterResults.Location = new Point((metroPanelFilter.Width / 2) - (metroLabelFilterResults.Width / 2), 0);
            metroPanelFilter.Location = new Point(metroListViewDataLong.Right + 10, metroListViewDataLong.Location.Y);
            metroPanelCondition.Location = new Point((metroPanelFilter.Width / 2) - (metroPanelCondition.Width / 2), metroLabelFilterResults.Bottom + 10);
            metroPanelFaults.Location = new Point((metroPanelFilter.Width / 2) - (metroPanelFaults.Width / 2), metroPanelCondition.Bottom + 10);
            metroPanelPathRating.Location = new Point((metroPanelFilter.Width / 2) - (metroPanelFaults.Width / 2), metroPanelFaults.Bottom + 10);
            metroPanelTown.Location = new Point((metroPanelFilter.Width / 2) - (metroPanelTown.Width / 2), metroPanelPathRating.Bottom + 10);

            metroButtonUpdateResults.Location = new Point((metroPanelFilter.Width / 2) - (metroButtonUpdateResults.Width / 2), metroPanelTown.Bottom + 10);
            metroButtonShowAll.Location = new Point((metroPanelFilter.Width / 2) - (metroButtonShowAll.Width / 2), metroButtonUpdateResults.Bottom + 10);

            //setting location of algorithm weighting components
            metroPanelSort.Width = metroListViewDataLong.Width;
            metroPanelSort.Location = new Point(metroListViewData.Right + 10, metroListViewDataLong.Bottom + 10);

            metroLabelHelp.Location = new Point(metroPanelSort.Width - metroLabelHelp.Width - 10);

            metroLabelAlgorithm.Location = new Point((metroPanelSort.Width / 2) - (metroLabelAlgorithm.Width / 2), 0);
            metroLabelZones.Location = new Point((metroPanelSort.Width / 2) - (metroLabelZones.Width / 2), metroLabelAlgorithm.Height + 2);

            metroLabelPathRatings.Location = new Point((metroPanelSort.Width / 2) - (metroLabelPathRatings.Width / 2), metroTextBoxRating2.Location.Y - 30);

            metroTextBoxRating2.Location = new Point((metroPanelSort.Width / 2) - (metroTextBoxRating2.Width / 2), metroLabelPathRatings.Location.Y + metroLabelPathRatings.Height + 5);
            metroLabelRating2.Location = new Point(metroTextBoxRating2.Left - 25, metroLabelPathRatings.Location.Y + metroLabelPathRatings.Height + 5);

            metroTextBoxRating1.Location = new Point(metroLabelRating2.Left - metroTextBoxRating1.Width - 15, metroLabelPathRatings.Location.Y + metroLabelPathRatings.Height + 5);
            metroLabelRating1.Location = new Point(metroTextBoxRating1.Left - 25, metroLabelPathRatings.Location.Y + metroLabelPathRatings.Height + 5);

            metroLabelRating3.Location = new Point(metroTextBoxRating2.Right + 10, metroLabelPathRatings.Location.Y + metroLabelPathRatings.Height + 5);
            metroTextBoxRating3.Location = new Point(metroLabelRating3.Right + 15, metroLabelPathRatings.Location.Y + metroLabelPathRatings.Height + 5);

            metroLabelRating5.Location = new Point((metroPanelSort.Width / 2) - (metroLabelRating5.Width / 2), metroTextBoxRating2.Bottom + 15);
            metroTextBoxRating5.Location = new Point(metroLabelRating5.Right + 15, metroTextBoxRating2.Bottom + 15);

            metroTextBoxRating4.Location = new Point(metroLabelRating5.Left - 15 - metroTextBoxRating4.Width, metroTextBoxRating2.Bottom + 15);
            metroLabelRating4.Location = new Point(metroTextBoxRating4.Left - 25, metroTextBoxRating2.Bottom + 15);            

            metroButtonReset.Location = new Point(metroTextBoxRating5.Location.X - 15, metroPanelSort.Height - metroButtonUpdateAlgorithm.Height - 25);
            metroButtonUpdateAlgorithm.Location = new Point(metroTextBoxRating4.Location.X + 15, metroPanelSort.Height - metroButtonUpdateAlgorithm.Height - 25);

            //setting size and position of map control and intial settings
            gMapControl1.Width = window_length - metroPanelSort.Width - metroListViewData.Width - 50;
            gMapControl1.Height = window_height - metroPanelFilter.Height - 110;
            gMapControl1.Location = new Point(metroPanelSort.Right + 10, metroPanelFilter.Bottom + 10);
            gMapControl1.SetPositionByKeywords("Hamilton, New Zealand");
            gMapControl1.ShowCenter = false;
            gMapControl1.CanDragMap = true;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 50;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Zoom = 10;
            gMapControl1.Refresh();

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

            //reading csv file
            StreamReader reader;
            string line = "";
            string[] csvArray;

            reader = File.OpenText("Final QGIS DATA.csv");
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                csvArray = line.Split(',');

                qgisData.Add(csvArray);
            }
            reader.Close();

            //reading shapefile
            ArrayList feats = ReadSHP("Footpath_Polygon.shp", new GeometryFactory());
            AddOverlay(feats);
        }

        /// <summary>
        /// Uses an openfiledialog to allow user to open an excel file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Uses the values provided by the user to recalculate the condition ratings for each footpath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButtonUpdateResults_Click(object sender, EventArgs e)
        {
            if (dataProcessed) //There is data to filter on
            {
                int numFaults = 0;
                int conditionRating = 0;
                int footpathRating = 0;

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

                if (!int.TryParse(metroTextBoxPathRating.Text, out footpathRating)) //Error handling - invalid input
                {
                    MetroMessageBox.Show(this, "Please enter a valid footpath rating to filter on", "Error");
                    metroTextBoxPathRating.Text = "0";
                    return;
                }
                else if (conditionRating < 0) //Error handling - condition rating is less than 0
                {
                    MetroMessageBox.Show(this, "Condition rating cannot be less than 0", "Error");
                    metroTextBoxPathRating.Text = "0";
                    return;
                }

                initializeDataListBox();
                filteredFootpaths = filterResults(numFaults, conditionRating, footpathRating);

                DisplayData(filteredFootpaths);
            }
        }

        /// <summary>
        /// Showing full, unfiltered list of footpaths
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButtonShowAll_Click(object sender, EventArgs e)
        {
            DisplayData(roadList);

            filteredFootpaths = new List<Road>(roadList); //Reset the list storing all the filtered results

            //Set the text box values back to zero
            metroTextBoxFilterCondition.Text = "0";
            metroTextBoxFilterFaults.Text = "0";
            metroComboBoxTown.Text = "";
        }

        /// <summary>
        /// Recalculates the condition ratingfor each footpath with the values provided by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButtonUpdateAlgorithm_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";

                double healthMin = double.Parse(metroTextBoxHealthMin.Text);
                double healthMax = double.Parse(metroTextBoxHealthMax.Text);
                double schoolMin = double.Parse(metroTextBoxSchoolMin.Text);
                double schoolMax = double.Parse(metroTextBoxSchoolMax.Text);
                double serviceMin = double.Parse(metroTextBoxServiceMin.Text);
                double serviceMax = double.Parse(metroTextBoxServiceMax.Text);
                double rating1 = double.Parse(metroTextBoxRating1.Text);
                double rating2 = double.Parse(metroTextBoxRating2.Text);
                double rating3 = double.Parse(metroTextBoxRating3.Text);
                double rating4 = double.Parse(metroTextBoxRating4.Text);
                double rating5 = double.Parse(metroTextBoxRating5.Text);

                if (healthMin >= healthMax)
                    errorMessage += "Health minimum cannot be greater than Health max. \n";
                if (schoolMin >= schoolMax)
                    errorMessage += "School minimum cannot be greater than School max. \n";
                if (serviceMin >= serviceMax)
                    errorMessage += "Service minimum cannot be greater than Service max. \n";

                //if no user input errors
                if (errorMessage == "")
                {
                    initializeDataListBox();
                    foreach (Road r in filteredFootpaths)
                    {
                        r.CalcConditionRating(healthMin, healthMax, schoolMin, schoolMax, serviceMin, serviceMax, rating1, rating2, rating3, rating4, rating5);
                    }
                    SortList();
                    DisplayData(filteredFootpaths);
                }
                else //if there are user input errors
                {
                    MetroMessageBox.Show(this, errorMessage, "Error");
                }
            }
            catch (FormatException) //if a textbox is empty or contains non numerical values
            {
                MetroMessageBox.Show(this, "Please ensure each textbox contains a numerical value", "Error");
            }
        }

        /// <summary>
        /// Recalculates the condition ratings for each footpath using the defaults values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="filename">Name of the excel file to open</param>
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
                            Console.WriteLine(dataArray[j]);
                        }
                    }
                    Road r = new Road(dataArray);
                    //Console.WriteLine("Number of rows in excel file" + numRows);
                    //Console.WriteLine("Number of entries in List of Lists" + ListLatList.Count());
                    //r.SetLat(ListLatList[i]);
                    //r.SetLong(ListLongList[i]);
                    roadList.Add(r);

                    //find matching data in qgis data
                    //0 - road name
                    //1 - start
                    //2 - end
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
            metroListViewData.Columns.Add("Average Footpath Rating");

            foreach (Road r in rList)
            {
                string PathName = r.GetRoadName();
                int pathLength = r.GetLongLength();
                int pathFaults = r.GetNumFaults();
                double pathCondition = r.GetConditionRating();
                double pathRating = r.GetFootpathCondition();
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
        private List<Road> filterResults(int numFaults, int conditionRating, int footpathRating)
        {
            bool filterOnFaults = numFaults != 0; //Check if the user specified a number of faults
            bool filterOnCondition = conditionRating != 0; //Check if the user specified a condition rating
            bool filterOnRating = footpathRating != 0; //Check if the user specified a footpath rating

            List<Road> filteredFootpaths = new List<Road>(roadList); //The list of filtered footpaths to be returned
            List<Road> temporaryList; //Temporary list which allows for cumulative processing

            if (filterOnFaults) //The user has specified a filter for the number of faults
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

            if (filterOnCondition) //The user has specified a filter for the condition rating
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

            if (filterOnRating) //The user has specified a filter for the footpath rating
            {
                temporaryList = new List<Road>();

                foreach (Road checkFootpathFilter in filteredFootpaths)
                {
                    if (checkFootpathFilter.GetFootpathCondition() >= footpathRating) //The current footpath exceeds or meets the footpath rating
                    {
                        temporaryList.Add(checkFootpathFilter);
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

            //List<double> FndLat = roadList[index].GetLat();
            //List<double> FndLong = roadList[index].GetLong();
            //Console.WriteLine("Index of selected path in view box: " + index);
            //ChangeOverlay(FndLat, FndLong);
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
            filteredFootpaths.Sort((x, y) =>
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
        /// <summary>
        /// Reads a shapefile into a arraylist of features that need converting from x,y coordinates to Long and Lat coordinates
        /// </summary>
        /// <param name="filename">name of the shapefile (the file that has all the polygons for the footpaths)</param>
        /// <param name="fact">the class that generates the structure of the points</param>
        /// <returns></returns>
        public ArrayList ReadSHP(string filename,GeometryFactory fact)
        {
            ArrayList features = new ArrayList(); //Array list for all the coordinates from the shapefile

            ShapefileDataReader sfDataReader = new ShapefileDataReader(filename, fact); //takes a file and a factory to build the geometries
            ShapefileHeader shpHeader = sfDataReader.ShapeHeader; //reads the headers of the file for checking and looping purposes
            DbaseFileHeader DHeader = sfDataReader.DbaseHeader;

            while (sfDataReader.Read() == true) //reading through all the data in the shapefile
            {
                Feature feature = new Feature(); //setting up a feature for each set of points
                AttributesTable atTable = new AttributesTable(); //table for the set of points
                string[] keys = new string[DHeader.NumFields];
                Geometry geometry = sfDataReader.Geometry;
                for (int i = 0; i < DHeader.NumFields; i++)
                {
                    DbaseFieldDescriptor fldDescriptor = DHeader.Fields[i];
                    keys[i] = fldDescriptor.Name;
                    atTable.Add(fldDescriptor.Name, sfDataReader.GetValue(i));
                }
                feature.Geometry = geometry;
                feature.Attributes = atTable; //setting the variables for the feature
                features.Add(feature);
            }
            sfDataReader.Close();//closing the reader 
            sfDataReader.Dispose();
            return features;
        }

        /// <summary>
        /// Adds the overlay to the Google map by taking the lat and long coordinates 
        /// </summary>
        /// <param name="features">List of features that contains x and y coordinates</param>
        public void AddOverlay(ArrayList features)
        {
            for (int i = 0; i < features.Count; i++) //process all features in the list
            {
                Feature feat = (Feature)features[i]; //extracts a feature from the list
                Geometry Geo = feat.Geometry;        //Creates a geometry of that feature
                GMapOverlay polygons = new GMapOverlay("Polygons"); //initalises the polygon overlay
                List<PointLatLng> PLL = new List<PointLatLng>(); //initialises the lists needed to store the polygon points
                List<double> LatLong = new List<double>();
                for (int k = 0; k < Geo.Coordinates.Length; k++) //runs through all the points associated with one feature
                {
                    LatLong = LongLatCalculation(Geo.Coordinates[k].X, Geo.Coordinates[k].Y); //calculates the conversion from the x, y coordinates to the lat and long coordinates
                    PLL.Add(new PointLatLng(LatLong[0], LatLong[1])); //Adds the points to a list which is used pass to the polygon constructor
                }
                GMapPolygon poly = new GMapPolygon(PLL, "Polygon"); //polygon constructor
                poly.Fill = new SolidBrush(Color.Orange);
                poly.Stroke = new Pen(Color.Black);
                polygons.Polygons.Add(poly);
                gMapControl1.Overlays.Add(polygons); //adds the polygons to the map in the form on an overlay
                gMapControl1.Refresh();
            }
        }
        /// <summary>
        /// Converts the x,y coordinates of the shapefile to Long and Lat coordinates using the ESPG2913 area offsets
        /// </summary>
        /// <param name="E">Easting coordinate</param>
        /// <param name="N">Northing coordinate</param>
        /// <returns></returns>
        public List<double> LongLatCalculation(double E, double N)
        {
            List<double> coordList = new List<double>();
            //Common variables for NZTM2000 / ESPG2913
            double a = 6378137;
            double f = 1 / 298.257222101;
            double lambdazero = 173;
            double Nzero = 10000000;
            double Ezero = 1600000;
            double kzero = 0.9996;

            //Calculation: From NZTM to lat/Long

            double b = a * (1 - f);
            double esq = 2 * f - Math.Pow(f, 2);
            double Z0 = 1 - esq / 4 - 3 * Math.Pow(esq, 2) / 64 - 5 * Math.Pow(esq, 3) / 256;
            double A2 = 0.375 * (esq + Math.Pow(esq, 2) / 4 + 15 * Math.Pow(esq, 3) / 128);
            double A4 = 15 * (Math.Pow(esq, 2) + 3 * Math.Pow(esq, 3) / 4) / 256;
            double A6 = 35 * Math.Pow(esq, 3) / 3072;

            double Nprime = N - Nzero;
            double mprime = Nprime / kzero;
            double smn = (a - b) / (a + b);
            double G = a * (1 - smn) * (1 - Math.Pow(smn, 2)) * (1 + 9 * Math.Pow(smn, 2) / 4 + 225 * Math.Pow(smn, 4) / 64) * Math.PI / 180.0;
            double sigma = mprime * Math.PI / (180 * G);
            double phiprime = sigma + (3 * smn / 2 - 27 * Math.Pow(smn, 3) / 32) * Math.Sin(2 * sigma) + (21 * Math.Pow(smn, 2) / 16 - 55 * Math.Pow(smn, 4) / 32) * Math.Sin(4 * sigma) + (151 * Math.Pow(smn, 3) / 96) * Math.Sin(6 * sigma) + (1097 * Math.Pow(smn, 4) / 512) * Math.Sin(8 * sigma);
            double rhoprime = a * (1 - esq) / Math.Pow((1 - esq * Math.Pow((Math.Sin(phiprime)), 2)), 1.5);
            double upsilonprime = a / Math.Sqrt(1 - esq * Math.Pow((Math.Sin(phiprime)), 2));

            double psiprime = upsilonprime / rhoprime;
            double tprime = Math.Tan(phiprime);
            double Eprime = E - Ezero;
            double chi = Eprime / (kzero * upsilonprime);
            double term_1 = tprime * Eprime * chi / (kzero * rhoprime * 2);
            double term_2 = term_1 * Math.Pow(chi, 2) / 12 * (-4 * Math.Pow(psiprime, 2) + 9 * psiprime * (1 - Math.Pow(tprime, 2)) + 12 * Math.Pow(tprime, 2));
            double term_3 = tprime * Eprime * Math.Pow(chi, 5) / (kzero * rhoprime * 720) * (8 * Math.Pow(psiprime, 4) * (11 - 24 * Math.Pow(tprime, 2)) - 12 * Math.Pow(psiprime, 3) * (21 - 71 * Math.Pow(tprime, 2)) + 15 * Math.Pow(psiprime, 2) * (15 - 98 * Math.Pow(tprime, 2) + 15 * Math.Pow(tprime, 4)) + 180 * psiprime * (5 * Math.Pow(tprime, 2) - 3 * Math.Pow(tprime, 4)) + 360 * Math.Pow(tprime, 4));
            double term_4 = tprime * Eprime * Math.Pow(chi, 7) / (kzero * rhoprime * 40320) * (1385 + 3633 * Math.Pow(tprime, 2) + 4095 * Math.Pow(tprime, 4) + 1575 * Math.Pow(tprime, 6));
            double term1 = chi * (1 / Math.Cos(phiprime));
            double term2 = Math.Pow(chi, 3) * (1 / Math.Cos(phiprime)) / 6 * (psiprime + 2 * Math.Pow(tprime, 2));
            double term3 = Math.Pow(chi, 5) * (1 / Math.Cos(phiprime)) / 120 * (-4 * Math.Pow(psiprime, 3) * (1 - 6 * Math.Pow(tprime, 2)) + Math.Pow(psiprime, 2) * (9 - 68 * Math.Pow(tprime, 2)) + 72 * psiprime * Math.Pow(tprime, 2) + 24 * Math.Pow(tprime, 4));
            double term4 = Math.Pow(chi, 7) * (1 / Math.Cos(phiprime)) / 5040 * (61 + 662 * Math.Pow(tprime, 2) + 1320 * Math.Pow(tprime, 4) + 720 * Math.Pow(tprime, 6));

            double latitude = (phiprime - term_1 + term_2 - term_3 + term_4) * 180 / Math.PI;
            double longitude = lambdazero + 180 / Math.PI * (term1 - term2 + term3 - term4);


            coordList.Add(latitude);
            coordList.Add(longitude);
            return coordList;
        }
        /*
        **Note** - Method for updating the polygons when a footpath is clicked, was attempted by trying to link polygons to roads but there was mismatch
        **Note** - Other methods such as centering map by keywords also did not work as intended
        public void ChangeOverlay(List<double> Long, List<double> Lat)
        {
            GMapOverlay SelectedOverlay = new GMapOverlay("Selected Polygons");
            List<PointLatLng> SelectedPLL = new List<PointLatLng>();
            for (int i = 0; i < Long.Count(); i++)
            {
                Console.WriteLine("Adding lat coords: " + Lat[i] + ", Adding long " + Long[i]);
                SelectedPLL.Add(new PointLatLng(Long[i], Lat[i]));
                Console.WriteLine("Amount of lat coords: " + Lat.Count() +", Amount of long coords: " +Long.Count());
            }
            GMapPolygon SelectedPoly = new GMapPolygon(SelectedPLL, "Selected Polygon");
            SelectedPoly.Fill = new SolidBrush(Color.Green);
            SelectedPoly.Stroke = new Pen(Color.Yellow);
            SelectedOverlay.Polygons.Add(SelectedPoly);
            //gMapControl1.Overlays.Clear();
            gMapControl1.Overlays.Add(SelectedOverlay);
            gMapControl1.Refresh();
            Console.WriteLine("Added a selected footpath to the map");
        }
        */
    }
}
