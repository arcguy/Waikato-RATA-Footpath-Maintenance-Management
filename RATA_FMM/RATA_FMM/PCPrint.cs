using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RATA_FMM
{
    public partial class PCPrint : System.Drawing.Printing.PrintDocument
    {
        private Font font; //Variable for the font the user wants
        private string text; //The text to be printed in the report
        static int currChar; //The current character to be printed

        /// <summary>
        /// Property to hold the text to be printed
        /// </summary>
        public string TextToPrint
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// Property to hold the font that the user wants
        /// </summary>
        public Font PrinterFont
        {
            get { return font; }
            set { font = value; }
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public PCPrint() : base()
        {
            text = ""; //Set the text to print as an empty string
        }

        /// <summary>
        /// Contructor to create PCPrint object with the desired text to print
        /// </summary>
        /// <param name="s"></param>
        public PCPrint(string s) : base()
        {
            text = s; //Set the text to print as the desired argument
        }

        /// <summary>
        /// Override default OnBeginPrint method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeginPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            base.OnBeginPrint(e); //Execute base code

            if (font == null)
            {
                font = new Font("Times New Roman", 10); //If font is null, set it to TNR size 10
            }
        }

        /// <summary>
        /// Override default OnPrintPage method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e); //Execute base code

            //Method variables
            int printHeight;
            int printWidth;
            int leftMargin;
            int rightMargin;
            Int32 lines;
            Int32 chars;

            //Set print size and margin size
            {
                printHeight = base.DefaultPageSettings.PaperSize.Height - base.DefaultPageSettings.Margins.Top - base.DefaultPageSettings.Margins.Bottom;
                printWidth = base.DefaultPageSettings.PaperSize.Width - base.DefaultPageSettings.Margins.Right - base.DefaultPageSettings.Margins.Left;
                leftMargin = base.DefaultPageSettings.Margins.Left;
                rightMargin = base.DefaultPageSettings.Margins.Top;
            }

            //If landscape has been chosen, switch width and height parameters
            if (base.DefaultPageSettings.Landscape)
            {
                int temp = printHeight;
                printHeight = printWidth;
                printWidth = temp;

            }

            Int32 numLines = (int)printHeight / PrinterFont.Height; //Determine the number of lines to be printed

            RectangleF area = new RectangleF(leftMargin, rightMargin, printWidth, printHeight); //Create printing area

            StringFormat format = new StringFormat(StringFormatFlags.LineLimit); //Create object to manage page layout

            e.Graphics.MeasureString(text.Substring(RemoveZeros(currChar)), PrinterFont, new SizeF(printWidth, printHeight), format, out chars, out lines); //Fit as many characters as possible into the print area

            e.Graphics.DrawString(text.Substring(RemoveZeros(currChar)), PrinterFont, Brushes.Black, area, format); //Print the page

            currChar += chars; //Increase the current character count

            //Check if there are more characters to print
            if (currChar < text.Length)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                currChar = 0;
            }
        }

        /// <summary>
        /// Removes any 0s in the size with 1s as 0s can cause printing errors
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns></returns>
        public int RemoveZeros(int value)
        {
            if (value != 0)
            {
                return value;
            }
            else
            {
                return 1;
            }
        }
    }
}
