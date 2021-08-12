using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;

namespace FrameOS.Commands
{
    class RmCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 1)
            {
                Console.WriteLine("Invalid Paramaters");
                return;
            }

            Filesystem.RemoveFile(commandArgs[0].String);
            Console.WriteLine("The file " + commandArgs[0].String + " has been deleted");
        }
    }
}
