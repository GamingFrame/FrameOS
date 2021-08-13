using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class RmCommand : ICommand
    {
        public string description { get => "Remove a file."; }
        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 1)
            {
                Terminal.WriteLine("Invalid Paramaters");
                return;
            }

            Filesystem.RemoveFile(commandArgs[0].String);
            Terminal.WriteLine("The file " + commandArgs[0].String + " has been deleted");
        }
    }
}
