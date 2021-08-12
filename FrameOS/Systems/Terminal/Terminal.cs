using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Commands;
using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Sound;
using FrameOS.Systems.Terminal;
using FrameOS.UserSystem;
using Sys = Cosmos.System;

namespace FrameOS.Terminal
{
    public static class Terminal
    {


        private static TerminalHistory history;

        public static void SetUp()
        {
            Console.Clear();
            Console.WriteLine(@"  ______                         ____   _____ ");
            Console.WriteLine(@" |  ____|                       / __ \ / ____|");
            Console.WriteLine(@" | |__ _ __ __ _ _ __ ___   ___| |  | | (___  ");
            Console.WriteLine(@" |  __| '__/ _` | '_ ` _ \ / _ \ |  | |\___ \ ");
            Console.WriteLine(@" | |  | | | (_| | | | | | |  __/ |__| |____) |");
            Console.WriteLine(@" |_|  |_|  \__,_|_| |_| |_|\___|\____/|_____/ ");

            Console.WriteLine("");

            try
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                while (!UserProfileSystem.Login(username, password))
                {
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                }

                Console.WriteLine("Logged in as " + username);
                history = new TerminalHistory(10);
            }
            catch (Exception e)
            {
                if (e is FatalException)
                {
                    Crash(e);
                }
                else
                {
                    Console.WriteLine("Error: " + e.Message);
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
                Console.WriteLine("That command does not excist");
            }

            //AddToHistory(v);
        }


        public static void ClearSlow(ConsoleColor color)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 5400; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, 0);
        }

        internal static void Crash(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Terminal.ClearSlow(ConsoleColor.DarkGreen);
            Console.WriteLine("Your PC has run into a problem and has been shut down to prevent damage to the system.");
            Console.WriteLine("");
            Console.WriteLine("Error:");
            Console.WriteLine(e.ToString());
            int y = Console.CursorTop;
            Console.SetCursorPosition((int)Math.Round(Console.WindowWidth / 2.5f), Console.WindowHeight - 2); Console.Write("@Gaming Frame - 2021");

            Console.SetCursorPosition(0, y);
            Console.WriteLine("\nPress enter to reboot, press delete to shut down: ");

            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    Sys.Power.Reboot();
                else if (Console.ReadKey(true).Key == ConsoleKey.Delete)
                    Sys.Power.Shutdown();
        }

        public static void PutChar(char c)
        {
            Console.Write(c);
        }

        public static void Backspace()
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            Console.SetCursorPosition(x - 1, y);
            Console.Write(" ");
            Console.SetCursorPosition(x - 1, y);
        }

        public static string ReadLine()
        {
            string input = "";
            int historyIndex = 0;
            bool enterPressed = false;

            while (!enterPressed)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar >= 32 && key.KeyChar <= 126) { PutChar(key.KeyChar); input += key.KeyChar; }

                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Remove(input.Length - 1, 1);
                        Backspace();
                    }
                    else
                    {
                        Sys.PCSpeaker.Beep(1500, 100);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    NewLine();
                    if (input == "") { enterPressed = true; break; }
                    history.AddToHistory(input);
                    enterPressed = true;
                }
            }
            return input;

        }

        public static void NewLine()
        {
            /*            int y = Console.CursorTop;
                        Console.SetCursorPosition(0, y+1);*/
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
