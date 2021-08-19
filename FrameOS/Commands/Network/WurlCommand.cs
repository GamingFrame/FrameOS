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
        public string description { get => "Send a GET request."; }

        public string command => "wurl";

        public void Run(CommandArg[] commandArgs)
        {
            Terminal.WriteLine(NetworkSystem.Get(commandArgs[0].String));
        }
    }
}
