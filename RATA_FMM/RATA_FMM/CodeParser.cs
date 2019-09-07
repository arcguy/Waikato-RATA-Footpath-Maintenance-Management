using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RATA_FMM
{
    class CodeParser
    { 
        //fault letters and corresponding fault type (Parallel Arrays), could consider having these in a config file
        private static String[] FAULT_CODES_LETTER = { "a", "b", "c", "d", "e", "f" };
        private static String[] FAULT_CODES_NAME = { "Trip Hazard", "Vertical Displacement", "Horizontal Displacement", "Broken", "Hole", "Poor Previous Reinstatement" };

        //takes a comment as a string and returns the contents of the code as an array
        public static String[] Decode(String input)
        {
            //split the string by spaces
            String[] split = input.Split(' ');

            //for each word in the comment
            for (int i = 0; i < split.Length; i++)
            {
                Console.WriteLine("Processing Line : " + split[i]);

                //splitting each potential code by commas to account for multiple codes per 'word'
                String[] code = split[i].Split(',');

                //for each individual potential code
                for (int j = 0; j < code.Length; j++)
                {
                    //if the word has ? in then assume its a code
                    if (code[j].Contains('?'))
                    {
                        return TypeOne(code[j]);
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            return null;
        }

        private static String[] TypeOne(String code)
        {
            Console.WriteLine("*******************");
            Console.WriteLine("Processing code : " + code);
            Console.WriteLine("Code Type : 1");

            //split the code by question marks
            String[] codeParts = code.Split('?');
            Console.WriteLine("Location : " + codeParts[0]);
            String faultname = "";
            for (int m = 0; m < FAULT_CODES_LETTER.Length; m++)
            {
                if (codeParts[1].Equals(FAULT_CODES_LETTER[m]))
                {
                    faultname = FAULT_CODES_NAME[m];
                }
            }

            String[] output = new String[4];

            output[0] = faultname;
            Console.WriteLine("Fault Type : " + faultname);

            output[1] = codeParts[2].Split('(')[0];
            Console.WriteLine("Length : " + codeParts[2].Split('(')[0]);


            if (codeParts[2].Split('(').Length > 1)
            {
                Console.WriteLine("Multiple Fault : " + codeParts[2].Split('(')[1].Split(')')[0]);
                output[2] = codeParts[2].Split('(')[1].Split(')')[0];
            }
            else
            {
                Console.WriteLine("Multiple Fault : None");
                output[2] = "1x";
            }

            return output;
        }
    }
}
