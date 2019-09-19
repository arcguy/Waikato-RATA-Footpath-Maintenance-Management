using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RATA_FMM
{
    class CodeParser
    {
        
        //fault letters and corresponding fault type (Parallel Arrays), could consider having these in a config file
        private static String[] FAULT_CODES_LETTER = { "a", "b", "c", "d", "e", "f" };
        private static String[] FAULT_CODES_NAME = {"Trip Hazard","Vertical Displacement","Horizontal Displacement","Broken","Hole", "Poor Previous Reinstatement" };


        //takes a comment as a string and returns the contents of the code as an array
        public static List<String[]> Decode(String input)
        {
			
			List<String[]> output = new List<String[]>();
			
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
						output.Add(TypeOne(code[j]));                  
                    }
					else if (code[j].Contains('^'))
					{
						String[] m = code[j].Split('^');
						for(int h = 0; h < m.Length; h++)
						{
                            if (m[h].Length > 1)
                            {
                                output.Add(TypeTwo(m[h]));
                            }
						}     
					}
                }
            }
            return output;
        }

        private static String[] TypeOne(String code)
        {
            Console.WriteLine("*******************");
            Console.WriteLine("Processing code : " + code);
            Console.WriteLine("Code Type : 1");

            //split the code by question marks
            String[] codeParts = code.Split('?');
            //Console.WriteLine("Location : " + codeParts[0]);
            String faultname = "";
            for (int m = 0; m < FAULT_CODES_LETTER.Length; m++)
            {
                if (codeParts[1].Equals(FAULT_CODES_LETTER[m]))
                {
                    faultname = FAULT_CODES_NAME[m];
                }
            }

            String[] output = new String[5];
			output[0] = "1";
            output[1] = faultname;
            //Console.WriteLine("Fault Type : " + faultname);

            output[2] = codeParts[2].Split('(')[0];
            //Console.WriteLine("Length : " + codeParts[2].Split('(')[0]);


            if (codeParts[2].Split('(').Length > 1)
            {
               // Console.WriteLine("Multiple Fault : " + codeParts[2].Split('(')[1].Split(')')[0]);
                output[3] = codeParts[2].Split('(')[1].Split(')')[0];
            }
            else
            {
               // Console.WriteLine("Multiple Fault : None");
                output[4] = "1x";
            }
            
            return output;
        }
		
		private static String[] TypeTwo(String code)
		{
			Console.WriteLine("*******************");
            Console.WriteLine("Processing code : " + code);
            Console.WriteLine("Code Type : 2");
			
			String[] codeParts = code.Split('#');
            Char[] codeStartChar = codeParts[0].ToCharArray();
            String[] codeStart = new string[codeStartChar.Length];
            for (int i = 0; i < codeStartChar.Length; i++)
            {
                codeStart[i] = codeStartChar[i].ToString();
                //Console.WriteLine(codeStartChar[i].ToString());
            }
			
			String[] output = new String[5];
			
			String start = "";
			String end = "";
			Boolean ended = false;
			String num = "";
			
			for(int i = 0; i < codeStart.Length; i++)
			{
				if(Regex.Match(codeStart[i] , @"[1-9]").Success)
				{
					//number found
					ended = true;
					num = codeStart[i];
				}
				else if(ended)
				{
					end += codeStart[i];
				}
				else
				{
					start += codeStart[i];
				}
			}
            output[0] = "2";
			output[1] = start;
			output[2] = num;
			output[3] = end;
			
			//the rest of the code, including the #'s that are used outside of the code
			for(int i = 1; i < codeParts.Length;i++)
			{
				output[4] += ("#" + codeParts[i]);					
			}
            return output;
		}
		
    }
}
