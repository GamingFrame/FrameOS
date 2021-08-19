using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class EchoCommand : ICommand
    {
        public string description => "Echo some text";

        public string command => "echo";

        public void Run(CommandArg[] commandArgs)
        {
            Cosmos.HAL.Terminal.WriteLine(commandArgs[0].String);
        }
    }
}
