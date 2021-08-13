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
            Console.WriteLine(Decrypter.Decrypt(commandArgs[0].String));
        }
    }
}
