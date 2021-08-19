using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class TestConsoleWidthCommand : ICommand
    {
        public string description => "To test the console size";

        public string command => "testSize";

        //5400

        public void Run(CommandArg[] commandArgs)
        {
            Terminal.Write(VGADriverII.Height.ToString() + VGADriverII.Width.ToString());
            
        }
    }
}
