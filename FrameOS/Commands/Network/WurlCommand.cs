using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class WurlCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            Terminal.WriteLine(NetworkSystem.Get(commandArgs[0].String));
        }
    }
}
