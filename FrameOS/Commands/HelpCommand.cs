using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class HelpCommand : ICommand
    {
        public string description { get => "Show all the commands."; }

        public string command => "help";

        public void Run(CommandArg[] commandArgs)
        {
            Dictionary<string, ICommand> commands = CommandSystem.commands;

            foreach (var item in commands)
            {
                Terminal.WriteLine(item.Key + " - "  + item.Value.description);
            }
        }
    }
}
