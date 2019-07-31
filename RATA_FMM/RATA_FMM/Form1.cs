using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells; //if getting errors install Asopse.cells library

namespace RATA_FMM
{
    public partial class Form1 : Form
    {
        const string FILTER = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
        List<Road> roadList = new List<Road>();

        public Form1()
        {
            InitializeComponent();
            listBoxDisplay.Items.Add("Road".PadRight(10) + "Road Name".PadRight(35) + "Start".PadRight(10) +
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

                for (int j = 0; j <= numRows; j++)  // Numeration starts from 0 to MaxDataColumn
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
            }
        }

        private void DisplayData()
        {
            //displaying in first listbox
            foreach (Road r in roadList)
            {
                listBoxDisplay.Items.Add(r.ToString());
            }

            //displaying in second listbox
            foreach (Road r in roadList)
            {

            }
        }
    }
}
