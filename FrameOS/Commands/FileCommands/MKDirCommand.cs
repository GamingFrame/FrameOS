using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class MKDirCommand : ICommand
    {
        public string description { get => "Make a directory."; }

        public string command => "mkdir";

        public void Run(CommandArg[] commandArgs)
        {
            if (commandArgs.Length != 1)
            {
                Terminal.WriteLine("Invalid Paramaters");
                return;
            }

            Filesystem.CreateFolder(commandArgs[0].String);
        }
    }
}
