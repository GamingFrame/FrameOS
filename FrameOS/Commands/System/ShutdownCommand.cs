using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class ShutdownCommand : ICommand
    {
        public string description { get => "Shut down the OS."; }

        public void Run(CommandArg[] commandArgs)
        {
            Cosmos.System.Power.Shutdown();
        }
    }
}
