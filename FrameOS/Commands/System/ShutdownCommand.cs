using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class ShutdownCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            Cosmos.System.Power.Shutdown();
        }
    }
}
