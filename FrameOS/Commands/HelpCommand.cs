using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class HelpCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            Dictionary<string, ICommand> commands = CommandSystem.commands;

            foreach (var item in commands)
            {
                Console.WriteLine(item.Key + " - Add description WIP");
            }
        }
    }
}
