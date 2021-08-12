using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class GetNetworkTimeCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            NTPClient client = new NTPClient();

            DateTime time = client.GetNetworkTime();

            if (time == null)
            {
                Terminal.WriteLine("Couldn't get the time! Check your internet connection!");
            }else
            {
                Terminal.WriteLine("Time: " + time);
            }
        }
    }
}
