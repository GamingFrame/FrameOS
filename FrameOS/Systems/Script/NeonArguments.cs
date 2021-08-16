using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FrameOS.Systems.Script;

namespace FrameOS.Systems.Script
{
    public static class NeonArguments
    {

        public static string getTotalString(string line)
        {
            string[] sections = line.Split(' ');

            for (int i = 0; i < sections.Length; i++)
            {
                if (sections[i].StartsWith('"') && !sections[i].EndsWith('"'))
                {
                    string temp = sections[i];

                    while (i < sections.Length && !sections[i].EndsWith('"'))
                    {
                        temp += " " + sections[++i];
                    }
                }
            }
            return null;
        }

        public class StringArgument
        {
            public string String {get;set;}
            public bool IsString {get;set;}

            public StringArgument(char[] letters, bool isString)
            {
                String = new string(letters);
                IsString = isString;
            }
        }

        public static string[] GetArgsFromString(string text)
        {
            List<List<StringArgument>> args = new List<List<StringArgument>>();
            args.Add(new List<StringArgument>());
            char[] letters = text.ToCharArray();
            int indexOfLastParamChange = 0;
            bool isString = false;
            bool isEvaluated = false;
            for(int i = 0; i < letters.Length; i++)
            {
                if(letters[i]=='"'){
                    if(isString)
                    {
                        args[args.Count-1].Add(new StringArgument(letters.Skip(indexOfLastParamChange).Take(i-indexOfLastParamChange).ToArray(), true));
                    }
                    indexOfLastParamChange = i;
                    isString = !isString;
                    continue;
                }
                if(letters[i]==','&&!isString)
                {
                    if(indexOfLastParamChange != i)
                        args[args.Count-1].Add(new StringArgument(letters.Skip(indexOfLastParamChange).Take(i-indexOfLastParamChange).ToArray(), false));
                    args.Add(new List<StringArgument>());
                }
            }

            List<string> result = new List<string>();
            for(int i = 0; i< args.Count;i++)
            {
                if(args[i].Count > 1)
                {
                    result.Add(Evaluate(args[i]));
                }
                else if(args[i].Count == 1)
                {
                    result.Add(args[i][0].String);
                }
                else
                {
                    result.Add(null);
                }
            }

            return result.ToArray();
        }

        public static Dictionary<string, VariableData> variables = new Dictionary<string, VariableData>();

        public class VariableData 
        {
            public enum ResultType {
                String,
                Number
            }
            public ResultType resultType {get;set;}
            public object data {get;set;}
            public string ToString()
            {
                return (string)data;
            }
            public float ToNumber()
            {
                return (float)data;
            }
            public T ToGeneric<T>()
            {
                return (T)data;
            }
        }

        public class EvaluationMethod
        {
            public enum ResultType {
                String,
                Number
            }
            public ResultType resultType {get;set;}
            public string data {get;set;}

            public string ToString()
            {
                bool isVariable = data.ToCharArray()[0]=='$';
                if(isVariable){
                    string key = data.Remove(0,1).Trim();
                    if(variables.ContainsKey(key)){
                        VariableData data = variables[key];
                        return data.ToString();
                    }
                    throw new Exception($"Variable {key} Not Found!");
                }
                return data;
            }
            public float ToNumber()
            {
                bool isVariable = data.ToCharArray()[0]=='$';
                if(isVariable){
                    string key = data.Remove(0,1).Trim();
                    if(variables.ContainsKey(key)){
                        VariableData data = variables[key];
                        return data.ToNumber();
                    }
                    throw new Exception($"Variable {key} Not Found!");
                }
                return float.Parse(data);
            }
        }

        public static string Evaluate(List<StringArgument> input)
        {
            string result = "";
            for(int i = 0; i < input.Count; i++)
            {
                if(input[i].IsString)
                {
                    result += input[i].String;
                    continue;
                }
                List<EvaluationMethod> methods = new List<EvaluationMethod>();
                input[i].String = input[i].String.Trim();
                char[] letter = input[i].String.ToCharArray();
                bool equalsOpp = letter[1]=='=';
                switch(letter[0])
                {
                    case '+':
                        // Add;
                        break;
                    case '-':
                        // Subtract
                        break;
                    case '/':
                        // Divide
                        break;
                    case '*':
                        // Multiply
                        break;
                    case '^':
                        // Power
                        break;
                    case '#':
                        // Its a Number
                        break;
                    case '$':
                        // Its a Wizard Harry
                        
                        break;
                    default:
                        throw new Exception("Bad Operator");
                }
            }

            return null; // just to fix error so I can build

        }
    }
}
