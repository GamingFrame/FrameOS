using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class WhoamiCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            Console.WriteLine("Name: " + UserSystem.UserProfileSystem.CurrentUser);
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

            Console.WriteLine("Permmision Level: " + permLevel);
        }
    }
}
