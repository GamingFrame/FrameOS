using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    public class KateCommand : ICommand
    {
        public string description { get => "Editing a file on the system"; }

        public void Run(CommandArg[] commandArgs)
        {
            if(commandArgs.Length != 1)
            {
                Terminal.WriteLine("Invalid arguments. Use: kate <filename>");
                return;
            }

            Kate.Startkate(commandArgs[0].String);
        }
    }
}
