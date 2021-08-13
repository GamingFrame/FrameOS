using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;

namespace FrameOS.Commands
{
    class NetworkInfoCommand : ICommand
    {
        public string description { get => "Check your current network configuration."; }
        public void Run(CommandArg[] commandArgs)
        {
            NetworkSystem.ListNetworkInfo();
        }
    }
}
