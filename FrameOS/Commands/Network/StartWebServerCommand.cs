using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;

namespace FrameOS.Commands
{
    class StartWebServerCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            NetworkSystem.StartWebServer();
        }
    }
}
