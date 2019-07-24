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
    }
}
