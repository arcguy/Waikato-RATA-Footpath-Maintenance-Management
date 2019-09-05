using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RATA_FMM
{
    /// <summary>
    /// WordPrint prints information to a Word template
    /// </summary>
    class WordPrint
    {
        private object templateName; //Name of the template to base the document from
        private object saveAs; //Name of the file to save the completed report to
        private List<Road> filteredFootpaths; //A list of footpaths to print

        /// <summary>
        /// WordPrint constructor
        /// </summary>
        /// <param name="template_name">Template name</param>
        /// <param name="save_as">Report name</param>
        public WordPrint(string template_name, string save_as)
        {
            this.templateName = template_name;
            this.saveAs = save_as;
        }

        /// <summary>
        /// Print information to a report
        /// </summary>
        /// <param name="filtered_footpaths">The footpaths to print</param>
        public void printFromTemplate(List<Road> filtered_footpaths)
        {
            this.filteredFootpaths = new List<Road>(filtered_footpaths); //Create a new list using the list of filtered footpaths
            createWordDocument(); //Craete the word document
        }

        /// <summary>
        /// Creates the word document and prints information to it
        /// </summary>
        private void createWordDocument()
        {
            List<int> processesBeforeGen = getRunningProcesses(); //Get Word processes before word document in created
            object missing = Missing.Value;

            Word.Application wordApp = new Word.Application(); //Create new word application
            Word.Document doc = null; //Create new word document

            if (File.Exists((string)this.templateName)) //The template exists
            {
                //Set document properties
                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;

                doc = wordApp.Documents.Open(ref this.templateName, ref missing, ref missing,
                    ref readOnly, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing); //Open word template

                doc.Activate(); //Activate the word document
                insertDataIntoWordTemplate(doc); //Insert the required data into the word template
            }
            else //The template does not exist
            {
                //Error
                MessageBox.Show("Error: Template path does not exist");
                return;
            }

            doc.SaveAs2(ref this.saveAs, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing); //Save the word document in the specified location

            MessageBox.Show("Document created");
            List<int> processesAfterGen = getRunningProcesses(); //Get Word processes after word document in created
            killProcesses(processesBeforeGen, processesAfterGen); //Kill all word processess so as not to corrupt the template or report
        }

        /// <summary>
        /// Find the table in the Word template to print information to
        /// </summary>
        /// <param name="doc">The Word template</param>
        /// <param name="bookmarkName">The name of the table</param>
        /// <returns>The Word Table to print information to</returns>
        private Word.Table getTableByBookmark(Word.Document doc, string bookmarkName)
        {
            Word.Table table = doc.Bookmarks[bookmarkName].Range.Tables[1]; //Get the desired table
            if (table != null) //The table exists
            {
                return table;
            }
            else //The table does not exist
            {
                return null;
            }
        }

        /// <summary>
        /// Inserts desired footpaths into the table
        /// </summary>
        /// <param name="doc">The Word document to print information to</param>
        private void insertDataIntoWordTemplate(Word.Document doc)
        {
            Word.Table table = getTableByBookmark(doc, "maintenanceTable"); //Obtain the desired table by finding it in the template

            if (table == null) //The table does not exist
            {
                return;
            }

            int i = 1; //Counter that corresponds to the current row
            foreach (Road r in this.filteredFootpaths) //Print each of the footpaths
            {
                i++; 
                table.Rows.Add(); //Add a new row to the table

                //Print desired information in each cell of the current row
                table.Cell(i, 1).Range.Text = r.GetRoadName();
                table.Cell(i, 2).Range.Text = r.GetStart().ToString();
                table.Cell(i, 3).Range.Text = r.GetEnd().ToString();
                table.Cell(i, 4).Range.Text = r.GetNumFaults().ToString();
            }

            Marshal.ReleaseComObject(table); //Release the table
        }

        /// <summary>
        /// Determines which Word processes are currently being run
        /// </summary>
        /// <returns>A list of process IDs that match Word processes</returns>
        private List<int> getRunningProcesses()
        {
            List<int> processIDs = new List<int>();

            foreach (Process clsProcess in Process.GetProcesses()) //For each running process
            {
                if (Process.GetCurrentProcess().Id == clsProcess.Id)
                {
                    continue;
                }
                if (clsProcess.ProcessName.Contains("WINWORD")) //The current process is a Word process
                {
                    processIDs.Add(clsProcess.Id); //Add the Word process to the process ID list
                }
            }

            return processIDs; //Return the process ID list
        }

        /// <summary>
        /// Kills all word processes
        /// </summary>
        /// <param name="processesBeforeGen">Word processes before the document was generated</param>
        /// <param name="processesAfterGen">Word processes after the document was generated</param>
        private void killProcesses(List<int> processesBeforeGen, List<int> processesAfterGen)
        {
            foreach (int pidAfter in processesAfterGen) //For each of the post-generation processes
            {
                bool processFound = false;
                foreach (int pidBefore in processesBeforeGen)
                {
                    if (pidAfter == pidBefore) //Any process before document generation is still running after
                    {
                        processFound = true;
                    }
                }
                if (processFound == false)
                {
                    Process clsProcess = Process.GetProcessById(pidAfter);
                    clsProcess.Kill();
                }
            }
        }
    }
}
