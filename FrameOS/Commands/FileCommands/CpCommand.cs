using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class CpCommand : ICommand
    {
        public string description { get => "Copy a file to another folder"; }

        public string command => "cp";

        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 2)
            {
                Terminal.WriteLine("Invalid Paramaters. Use: mv <file> <new destinacion>");
                return;
            }

            FileSystem.Filesystem.copyFile(commandArgs[0].String, commandArgs[1].String);
        }
    }
}
