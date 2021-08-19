using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;

namespace FrameOS.Commands
{
    class StartWebServerCommand : ICommand
    {
        public string description { get => "Run the FRAME server."; }

        public string command => "startWeb";

        public void Run(CommandArg[] commandArgs)
        {
            if (commandArgs.Length > 0)
            {
                if (commandArgs[0].String == "--loop")
                {
                    while (true)
                    {
                        Cosmos.HAL.Terminal.WriteLine("LOOP");
                        NetworkSystem.StartWebServer(true);
                        DelayCode(5000);
                    }
                }
                else
                {
                    throw new Exception("Invalid Flag");
                }
            }
            else
            {
                NetworkSystem.StartWebServer(false);
            }
        }
        void DelayCode(double ms)
        {
            for (int i = 0; i < ms * 100000; i++)
            {
                ;
                ;
                ;
                ;
                ;
            }
        }
    }
}
