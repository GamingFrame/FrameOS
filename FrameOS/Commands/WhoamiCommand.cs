using Cosmos.HAL;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class WhoamiCommand : ICommand
    {
        public string description { get => "Check as who you are logged in."; }

        public string command => "whoami";

        public void Run(CommandArg[] commandArgs)
        {
            Terminal.WriteLine("Name: " + UserSystem.UserProfileSystem.CurrentUser);
            string permLevel = "";

            switch (UserSystem.UserProfileSystem.CurrentPermLevel)
            {
                case UserSystem.UserPermLevel.Guest:
                    permLevel = "Guest";
                    break;
                case UserSystem.UserPermLevel.User:
                    permLevel = "User";
                    break;
                case UserSystem.UserPermLevel.Administrator:
                    permLevel = "Administrator";
                    break;
                case UserSystem.UserPermLevel.System:
                    permLevel = "System";
                    break;
            }

            Terminal.WriteLine("Permmision Level: " + permLevel);
        }
    }
}
