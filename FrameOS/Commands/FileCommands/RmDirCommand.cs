using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class RmDirCommand : ICommand
    {
        public string description { get => "Remove a directory."; }

        public string command => "rmdir";

        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 1)
            {
                Terminal.WriteLine("Invalid Paramaters");
                return;
            }

            Filesystem.RemoveFolder(commandArgs[0].String);
        }
    }
}
