using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Encryption;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class DecryptCommand : ICommand
    {
        public string description { get => "Decrypt an encrypted text."; }
        public void Run(CommandArg[] commandArgs)
        {
            Terminal.WriteLine(Hashing.verifyHash("hi", "8f434346648f6b96df89dda901c5176b10a6d83961dd3c1ac88b59b2dc327aa4".ToUpper()).ToString());
        }
    }
}
