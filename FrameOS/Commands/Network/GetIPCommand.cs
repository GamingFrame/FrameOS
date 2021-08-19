using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;

namespace FrameOS.Commands
{
    class GetIPCommand : ICommand
    {
        public string description { get => "Get the IP from a host."; }

        public string command => "getip";

        public void Run(CommandArg[] commandArgs)
        {
            NetworkSystem.GetIP(commandArgs[0].String.Replace("http://", "").Replace("https://", "").Replace("/", ""));
        }
    }
}
