using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;

namespace FrameOS.Commands
{
    class RmDirCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 1)
            {
                Console.WriteLine("Invalid Paramaters");
                return;
            }

            Filesystem.RemoveFolder(commandArgs[0].String);
        }
    }
}
