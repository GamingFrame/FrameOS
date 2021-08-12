using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class RebootCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            Cosmos.System.Power.Reboot();
        }
    }
}
