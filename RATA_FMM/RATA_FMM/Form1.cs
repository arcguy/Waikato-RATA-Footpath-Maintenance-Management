using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RATA_FMM
{
    

    public partial class Form1 : Form
    {

        static string[] MAINTENANCE_CODES = { "a" , "b" , "c" , "d" , "e" , "f" };
        static string[] MAINTENANCE_FAULTS = { "Trip Hazard" , "Vertical Displacement" , "Horizontal Displacement" , "Broken" , "Hole" , "Poor Previous Reinstatement"};

        public Form1()
        {
            InitializeComponent();
            rankOnSeverity();
        }

        /// <summary>
        /// Takes the maintenance code and returns the maintenance fault as a string
        /// </summary>
        /// <param name="c">The maintenance code</param>
        /// <returns>The maintenance fault</returns>
        public string getMaintenanceFault (string c)
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
        public void rankOnSeverity ()
        {
            List<string[]> footpaths = new List<string[]>(); //A list to hold all footpaths

            //Some temporary dummy entries
            string[] dummyEntryOne = {"Albert Street" , "5"};
            string[] dummyEntryTwo = { "Anzac Street" , "4"};
            string[] dummyEntryThree = { "Hurley Place" , "6"};
            string[] dummyEntryFour = { "Queen Street" , "2"};
            string[] dummyEntryFive = { "Shakespeare Street" , "1"};
            string[] dummyEntrySix = { "Taylor Street" , "1"};
            string[] dummyEntrySeven = { "Thornton Road" , "2"};
            string[] dummyEntryEight = { "Victoria Street" , "3"};
            string[] dummyEntryNine = { "Cotter Place" , "4"};
            string[] dummyEntryTen = { "Wallace Terrace" , "5"};

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
    }
}
