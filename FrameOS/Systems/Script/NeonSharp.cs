using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Systems.Script
{
    public static class NeonSharp
    {

        /* NeonSharp documentation lol
         * 
         * echoLine() == terminal.writeline()
         * echo() == terminal.write()
         * text = readInput() == ? Still need to figure out how to store variables, maybe with a dictionary<string, string> where the key is the variable name and the value becomes the read line
         * if else endif
         * text += text
         * echoLine("text" + variable)
        */


        public static Dictionary<string, string> variables = new Dictionary<string, string>();


        /// <summary>
        /// Executes the code given.
        /// </summary>
        /// <param name="scriptLines">Code seperated in lines to execute.</param>
        public static void Exec(string[] scriptLines)
        {
            int line = 0;
            try{
                for (int i = 0; i < scriptLines.Length; i++)
                {
                    line = i;
                    ReadLine(scriptLines[i]);
                }
            }catch(Exception e)
            {
                Cosmos.HAL.Terminal.WriteLine($"Neon Sharp Exception on line: {line+1}\n{e.Message}");
            }
        }

        private static void ReadLine(string line)
        {
            if (line.StartsWith("echoLine(") && line.EndsWith(")"))
            {
                string text = line.Remove(0, 9).Remove(line.Length-1, 1);
                string arguments = NeonArguments.getTotalString(text);
                if (arguments != "")
                {
                    Cosmos.HAL.Terminal.WriteLine(arguments);
                }
            }
        }


        public static string TryGetVariable(string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }
            throw new Exception("Variable: " + name + " doesn't exist!");
        }
    }
}
