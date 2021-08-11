using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;
using Cosmos.System.FileSystem.Listing;

namespace FrameOS.Commands
{
    class FormatCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            if (commandArgs.Length != 1)
            {
                Console.WriteLine("Invalid Paramaters");
                return;
            }

            string drive = commandArgs[0].String;

            Filesystem.Format(drive);
        }
    }
}
