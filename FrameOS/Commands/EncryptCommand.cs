using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Encryption;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class EncryptCommand : ICommand
    {
        public string description { get => "Encrypt text."; }

        public string command => "encrypt";

        public void Run(CommandArg[] commandArgs)
        {
            Terminal.WriteLine(Hashing.generateHash(commandArgs[0].String));
        }
    }
}
