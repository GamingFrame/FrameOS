using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    public class MvCommand : ICommand
    {
        public string description { get => "Move the file to a other destinacion"; }

        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 2)
            {
                Terminal.WriteLine("Invalid Paramaters. Use: mv <file> <new destinacion>");
                return;
            }

            Terminal.WriteLine(FileSystem.Filesystem.GetCurrentPath());

            FileSystem.Filesystem.moveFile(commandArgs[0].String, commandArgs[1].String);
        }
    }
}
