using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Logs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class TestCrashCommand : ICommand
    {
        public string description => "System command to test the Crash Catch System";

        public string command => "crash";

        public void Run(CommandArg[] commandArgs)
        {
            LogManager.Log("Testing Crash System Log", LogType.Error);
            throw new FatalException("Testing Crash System");
        }
    }
}
