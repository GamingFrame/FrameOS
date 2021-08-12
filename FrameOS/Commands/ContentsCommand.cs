using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;
using System.IO;

namespace FrameOS.Commands
{
    class ContentsCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            if (commandArgs.Length != 1)
            {
                Console.WriteLine("Invalid Paramaters");
                return;
            }
            string[] contents = File.ReadAllLines(Filesystem.GetFullPath() + @"\" + commandArgs[0].String);
            Console.WriteLine(string.Join(" ", contents));
        }
    }
}
