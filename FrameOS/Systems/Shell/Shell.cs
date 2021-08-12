using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using FrameOS.Commands;
using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Sound;
using FrameOS.Systems.Terminal;
using FrameOS.UserSystem;
using Sys = Cosmos.System;
using FrameOS.Systems.Networking;

namespace FrameOS.Shell
{
    public static class Shell
    {

        private static ShellHistory history;

        public static void SetUp()
        {
            Terminal.Clear();
            Terminal.WriteLine(@"  ______                         ____   _____ ");
            Terminal.WriteLine(@" |  ____|                       / __ \ / ____|");
            Terminal.WriteLine(@" | |__ _ __ __ _ _ __ ___   ___| |  | | (___  ");
            Terminal.WriteLine(@" |  __| '__/ _` | '_ ` _ \ / _ \ |  | |\___ \ ");
            Terminal.WriteLine(@" | |  | | | (_| | | | | | |  __/ |__| |____) |");
            Terminal.WriteLine(@" |_|  |_|  \__,_|_| |_| |_|\___|\____/|_____/ ");
            NTPClient client = new NTPClient();

            DateTime time = client.GetNetworkTime();
            if (time != null) Terminal.WriteLine("The current time is: " + time);

            Terminal.NewLine();

            try
            {
                Terminal.Write("Username: ");
                string username = Terminal.ReadLine();
                Terminal.Write("Password: ");
                string password = Terminal.ReadLine();
                while (!UserProfileSystem.Login(username, password))
                {
                    Terminal.Write("Username: ");
                    username = Terminal.ReadLine();
                    Terminal.Write("Password: ");
                    password = Terminal.ReadLine();
                }

                Terminal.WriteLine("Logged in as " + username);
                history = new ShellHistory(10);
            }
            catch (Exception e)
            {
                if (e is FatalException)
                {
                    Crash(e);
                }
                else
                {
                    Terminal.WriteLine("Error: " + e.Message);
                }

            }
        }
        public static void Run(string v)
        {
            var args = v.Split(' ');
            List<string> argsFixed = new List<string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith('"') && !args[i].EndsWith('"'))
                {
                    string temp = args[i];
                    while(i < args.Length && !args[i].EndsWith('"'))
                    {
                        temp += " " + args[++i];
                    }
                    argsFixed.Add(temp.Replace("\"", ""));
                }
                else
                {
                    argsFixed.Add(args[i].Replace("\"", ""));
                }
            }

            CommandArg[] commandArgs = CommandArg.GetCommandArgs(argsFixed.ToArray());
            ICommand command = CommandSystem.GetCommand(args[0]);

            if (command != null)
            {
                command.Run(commandArgs);
            }else
            {
                Terminal.WriteLine("That command does not excist");
            }

            history.AddToHistory(v);
        }


        public static void ClearSlow(ConsoleColor color)
        {
            Terminal.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 5400; i++)
            {
                Terminal.Write(" ");
            }
            Terminal.SetCursorPos(0, 0);
        }

        internal static void Crash(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Shell.ClearSlow(ConsoleColor.DarkGreen);
            Terminal.WriteLine("Your PC has run into a problem and has been shut down to prevent damage to the system.");
            Terminal.WriteLine("");
            Terminal.WriteLine("Error:");
            Terminal.WriteLine(e.ToString());
            int y = Console.CursorTop;
            Terminal.SetCursorPos((int)Math.Round(Console.WindowWidth / 2.5f), Console.WindowHeight - 2); Console.Write("@Gaming Frame - 2021");

            Terminal.SetCursorPos(0, y);
            Terminal.WriteLine("\nPress enter to reboot, press delete to shut down: ");

            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    Sys.Power.Reboot();
                else if (Console.ReadKey(true).Key == ConsoleKey.Delete)
                    Sys.Power.Shutdown();
        }

        internal static string GetHistoryItem(int historyIndex)
        {
            return history.GetHistory(historyIndex);
        }

        internal static int GetHistoryMax()
        {
            return history.Max();
        }
    }
}
