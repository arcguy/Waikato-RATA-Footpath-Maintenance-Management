using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RATA_FMM
{
    /*
		This class is used to decode strings of text that contain the two code types used in RAMM comments.
	*/
    public class CodeParser
    {

        //fault letters and corresponding fault type (Parallel Arrays), should consider having these in a config file if this version of code was used in the future (These are used for the type 1 codes)
        private static String[] FAULT_CODES_LETTER = { "a", "b", "c", "d", "e", "f" };
        private static String[] FAULT_CODES_NAME = { "Trip Hazard", "Vertical Displacement", "Horizontal Displacement", "Broken", "Hole", "Poor Previous Reinstatement" };


        /*
			This method takes the text as a string, interprets the parts recognised as codes, and returns them as a list of fixed sized arrays, each array is one code(As multiple codes per text is supported)
			Each array output of type 1 is in the format [<code type>, <fault name>, <>]
			Each array output of type 2 is in the format []
		*/
        public static List<String[]> Decode(String input)
        {
            //create the list of string arrays that will be returned
            List<String[]> output = new List<String[]>();

            //split the input text by spaces
            String[] split = input.Split(' ');

            //for each 'word' in the text
            for (int i = 0; i < split.Length; i++)
            {
                //splitting each potential code by commas to account for multiple codes per 'word'
                String[] code = split[i].Split(',');

                //for each individual potential code
                for (int j = 0; j < code.Length; j++)
                {
                    //if the word has ? in then assume its a code of type 1
                    if (code[j].Contains('?'))
                    {
                        //process the code as type 1 and add the result to the output list
                        output.Add(TypeOne(code[j]));
                    }
                    else if (code[j].Contains('^')) //else if the word has a ^ in it then assume its of type 2
                    {
                        //remove all ^ symbols before processing
                        String[] m = code[j].Split('^');
                        for (int h = 0; h < m.Length; h++)
                        {
                            if (m[h].Length > 1)
                            {
                                //process the code as type 2 and add the result to the output list
                                output.Add(TypeTwo(m[h]));
                            }
                        }
                    }
                }
            }
            return output;
        }


        /*
			This method takes a code that is type 1 and returns an array with each part of the code interpreted
		*/
        private static String[] TypeOne(String code)
        {
            //split the code by question marks
            String[] codeParts = code.Split('?');

            //temporary string to hold the fault name
            String faultname = "";

            //match the letter in the code with the corresponding fault name
            for (int m = 0; m < FAULT_CODES_LETTER.Length; m++)
            {
                if (codeParts[1].Equals(FAULT_CODES_LETTER[m]))
                {
                    faultname = FAULT_CODES_NAME[m];
                }
            }

            //create the output string array
            String[] output = new String[5];

            //the type of code
            output[0] = "1";

            //the contents of the code added to the output
            output[1] = faultname;
            output[2] = codeParts[2].Split('(')[0];
            if (codeParts[2].Split('(').Length > 1)
            {
                output[4] = codeParts[2].Split('(')[1].Split(')')[0];
            }
            else
            {
                output[3] = "1x";
            }

            return output;
        }


        /*
			This method takes a code that is type 2 and returns an array with the grade of the footpath only, and the rest of the code unchanged
		*/
        private static String[] TypeTwo(String code)
        {
            //split the code by # characters as that is where the number is located
            String[] codeParts = code.Split('#');

            //convert the first half of the code to a character array
            Char[] codeStartChar = codeParts[0].ToCharArray();

            //make a string array with the same length as the character array
            String[] codeStart = new string[codeStartChar.Length];

            //for every character add to the string array, the final aim of this is to split the string into each character
            for (int i = 0; i < codeStartChar.Length; i++)
            {
                codeStart[i] = codeStartChar[i].ToString();
            }

            //create output string array
            String[] output = new String[5];

            //holds the code before the rating number
            String start = "";
            //holds the code after the rating number
            String end = "";
            //checkmark if the number has been found
            Boolean ended = false;
            //holds the number
            String num = "";

            //for every character in the array
            for (int i = 0; i < codeStart.Length; i++)
            {
                //check if the character is a number
                if (Regex.Match(codeStart[i], @"[1-9]").Success)
                {
                    //number found, set ended and remember the number (will get the last number found in the code if there are multiple)
                    ended = true;
                    num = codeStart[i];
                }
                else if (ended) //if its ended then store the current character in end
                {
                    end += codeStart[i];
                }
                else //if not ended then store the current character in start
                {
                    start += codeStart[i];
                }
            }
            //code type
            output[0] = "2";
            //the start of the code
            output[1] = start;
            //the number found
            output[2] = num;
            //the rest of the code after the number
            output[3] = end;

            //the rest of the code, including the #'s that are used outside of the code
            for (int i = 1; i < codeParts.Length; i++)
            {
                output[4] += ("#" + codeParts[i]);
            }
            return output;
        }

    }
}