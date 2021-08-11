using FrameOS.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.UserSystem;
using FrameOS.Systems.CommandSystem;
using ts = FrameOS.Terminal;
using FrameOS.Commands;

namespace FrameOS.Boot
{
    public static class Boot
    {

        public static void StartUp()
        {
            try
            {
                Console.WriteLine("Booting FrameOS...");

                Console.WriteLine("Starting File System...");
                // Load File System

                Filesystem.SetUp();

                Console.WriteLine("Starting Networking System...");
                // Load Networking System

                Console.WriteLine("Starting User System...");
                // Load User System

                bool firstTime = UserProfileSystem.FirstTime();

                Console.WriteLine("Checking First Time Setup...");

                if (firstTime)
                {
                    Console.WriteLine("First time setup...");
                    Console.WriteLine("Please make an account.");

                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    UserProfileSystem.CreateUser(username, password, 2);

                    Console.WriteLine("Acount succesfully created.");
                }

                Console.WriteLine("Loading Commands...");

                CommandSystem.RegisterCommand("help", new HelpCommand());
                CommandSystem.RegisterCommand("tetris", new TetrisCommand());
                CommandSystem.RegisterCommand("format", new FormatCommand());
                CommandSystem.RegisterCommand("whoami", new WhoamiCommand());
                CommandSystem.RegisterCommand("cd", new CDCommand());
                CommandSystem.RegisterCommand("ls", new LSCommand());
                CommandSystem.RegisterCommand("mkdir", new MKDirCommand());

                //TODO fix the encryption system.
                //CommandSystem.RegisterCommand("encrypt", new EncryptCommand());
                //CommandSystem.RegisterCommand("decrypt", new DecryptCommand());

                Console.WriteLine("Loading Terminal...");
                // Load Terminal    
                ts.Terminal.SetUp();
            }
            catch (Exception e)
            {
                if (e is FatalException)
                {
                    ts.Terminal.Crash(e);
                }
                else
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

        }
    }
}
