using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class PingCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            if (commandArgs.Length != 1)
            {
                Terminal.WriteLine("Invaild synax. Usage: ping <ip address or site>"); // I like this message, imma change this everywhere
                return;
            }

            NetworkSystem.Ping(commandArgs[0].String);

        }
    }
}
