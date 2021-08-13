using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    public interface ICommand
    {
        string description { get; }

        void Run(CommandArg[] commandArgs);
    }
}
