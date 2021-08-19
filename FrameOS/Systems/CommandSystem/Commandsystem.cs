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

        public static void RegisterCommand(ICommand _ICommand)
        {
            string command = _ICommand.command.ToLower().Trim();

            commands.Add(command, _ICommand);
        }

        public static void RegisterCommands(ICommand[] commandList)
        {
            for (int i = 0; i < commandList.Length; i++)
            {
                string command = commandList[i].command.ToLower().Trim();

                commands.Add(command, commandList[i]);
            }
        }
    }
}
