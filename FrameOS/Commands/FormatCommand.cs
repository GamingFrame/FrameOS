using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.FileSystem;
using sys = Cosmos.System;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class FormatCommand : ICommand
    {
        public string description { get => "Format a drive."; }
        public void Run(CommandArg[] commandArgs)
        {
            if (commandArgs.Length != 1)
            {
                Terminal.WriteLine("Invalid Paramaters");
                return;
            }

            string drive = commandArgs[0].String;

            Terminal.WriteLine("Formating drive " + drive);

            Filesystem.Format(drive);

            Terminal.WriteLine("Rebooting the system");

            sys.Power.Reboot();
        }
    }
}
