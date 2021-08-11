using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Commands;

namespace FrameOS.Systems.CommandSystem
{
    public static class CommandSystem
    {

        //public static Dictionary<string, Func<bool>> commands = new Dictionary<string, Func<bool>> { {"Help", Help } };
        //public static Dictionary<string, string> descriptions = new Dictionary<string, string> { { } }

        internal static Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public static ICommand GetCommand(string command)
        {
            command = command.ToLower().Trim();
            if (commands.ContainsKey(command))
            {
                return commands[command];
            }
            return null;
        }

        public static void RegisterCommand(string command, ICommand _ICommand)
        {
            command = command.ToLower().Trim();

            commands.Add(command, _ICommand);
        }
    }
}
