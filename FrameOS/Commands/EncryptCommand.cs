using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Encryption;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class EncryptCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            Console.WriteLine(Encrypter.Encrypt(commandArgs[0].String));
        }
    }
}
