using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Systems.CommandSystem
{
    public class CommandArg
    {
        private string arg { get; set; }
        public CommandArg(string arg) => this.arg = arg;
        public static CommandArg[] GetCommandArgs(string[] args)
        {
            List<CommandArg> commandArgs = new List<CommandArg>(); 
            for (int i = 1; i < args.Length; i++)
            {
                commandArgs.Add(new CommandArg(args[i]));
            }
            return commandArgs.ToArray();
        }

        public string String => arg;
        public int Int => int.Parse(arg);
    }
}
