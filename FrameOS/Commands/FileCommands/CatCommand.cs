using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class CatCommand : ICommand
    {
        public string description { get => "Read the contents of a file."; }
        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 1)
            {
                Terminal.WriteLine("Invalid Paramaters");
                return;
            }

            if (commandArgs[0].String.StartsWith("!"))
            {
                Terminal.WriteLine("File " + commandArgs[0].String + " doesn't exist");
                return;
            }

            string[] lines = Filesystem.GetFileContent(commandArgs[0].String);

            if(lines.Length == 0) { return; }

            for(int index = 0, line = 1; index < lines.Length; index++, line++)
            {
                Terminal.WriteLine(line + ". " + lines[index]);
            }
        }
    }
}
