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
    class WordPrint
    {
        private string templateName;
        private string saveAs;
        private List<Road> filteredFootpaths;

        public WordPrint(string template_name, string save_as)
        {
            this.templateName = template_name;
            this.saveAs = save_as;
            this.filteredFootpaths = new List<Road>();
        }

        public void printFromTemplate(List<Road> filtered_footpaths)
        {
            this.filteredFootpaths = new List<Road>(filtered_footpaths);
            createWordDocument(this.templateName, this.saveAs);
        }

        private void createWordDocument(object templateName, object saveAs)
        {
            List<int> processesBeforeGen = getRunningProcesses();
            object missing = Missing.Value;

            Word.Application wordApp = new Word.Application();
            Word.Document doc = null;

            if (File.Exists((string)templateName))
            {
                DateTime today = DateTime.Now;

                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;

                doc = wordApp.Documents.Open(ref templateName, ref missing, ref missing,
                    ref readOnly, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                doc.Activate();
                insertDataIntoWordTemplate(doc);
            }
            else
            {
                //Error
                MessageBox.Show("Error: Template path does not exist");
                return;
            }

            doc.SaveAs2(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing);

            MessageBox.Show("Document created");
            List<int> processesAfterGen = getRunningProcesses();
            killProcesses(processesBeforeGen, processesAfterGen);
        }

        private Word.Table getTableByBookmark(Word.Document doc, string bookmarkName)
        {
            Word.Table table = doc.Bookmarks[bookmarkName].Range.Tables[1];
            if (table != null)
            {
                return table;
            }
            else
            {
                return null;
            }
        }

        private void insertDataIntoWordTemplate(Word.Document doc)
        {
            Word.Table table = getTableByBookmark(doc, "maintenanceTable");

            if (table == null)
            {
                return;
            }

            int i = 1;
            foreach (Road r in this.filteredFootpaths)
            {
                i++;
                table.Rows.Add();
                table.Cell(i, 1).Range.Text = r.GetRoadName();
                table.Cell(i, 2).Range.Text = r.GetStart().ToString();
                table.Cell(i, 3).Range.Text = r.GetEnd().ToString();
                table.Cell(i, 4).Range.Text = r.GetNumFaults().ToString();
            }

            Marshal.ReleaseComObject(table);
        }

        private List<int> getRunningProcesses()
        {
            List<int> processIDs = new List<int>();

            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (Process.GetCurrentProcess().Id == clsProcess.Id)
                {
                    continue;
                }
                if (clsProcess.ProcessName.Contains("WINWORD"))
                {
                    processIDs.Add(clsProcess.Id);
                }
            }

            return processIDs;
        }

        private void killProcesses(List<int> processesBeforeGen, List<int> processesAfterGen)
        {
            foreach (int pidAfter in processesBeforeGen)
            {
                bool processFound = false;
                foreach (int pidBefore in processesBeforeGen)
                {
                    if (pidAfter == pidBefore)
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
