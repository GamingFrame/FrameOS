using FrameOS.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.UserSystem;
using FrameOS.Systems.CommandSystem;
using ts = FrameOS.Shell;
using FrameOS.Commands;
using Cosmos.System.Graphics;
using FrameOS.Systems.Networking;
using Cosmos.HAL;
using FrameOS.Systems.Logs;

namespace FrameOS.Boot
{
    public static class Boot
    {

        public static void StartUp()
        {
            try
            {
                Terminal.WriteLine("Booting FrameOS...");

                Terminal.WriteLine("Starting File System...");
                // Load File System

                Filesystem.SetUp();
                Terminal.Clear();

                Terminal.WriteLine("Starting Networking System...");
                // Load Networking System
                NetworkSystem.SetLocalIP();

                Terminal.WriteLine("Starting User System...");
                // Load User System

                bool firstTime = UserProfileSystem.FirstTime();

                Terminal.WriteLine("Checking First Time Setup...");

                if (firstTime)
                {
                    Terminal.WriteLine("First time setup...");
                    Terminal.WriteLine("Please make an account.");

                    Terminal.Write("Username: ");
                    string username = Terminal.ReadLine();
                    Terminal.Write("Password: ");
                    string password = Terminal.ReadLine();

                    UserProfileSystem.CreateUser(username, password, 2);

                    Terminal.WriteLine("Acount succesfully created.");
                }

                Terminal.WriteLine("Loading Commands...");

                CommandSystem.RegisterCommand(new HelpCommand());
                CommandSystem.RegisterCommand(new TetrisCommand());
                CommandSystem.RegisterCommand(new FormatCommand());
                CommandSystem.RegisterCommand(new WhoamiCommand());
                CommandSystem.RegisterCommand(new CDCommand());
                CommandSystem.RegisterCommand(new LSCommand());
                CommandSystem.RegisterCommand(new MKDirCommand());
                CommandSystem.RegisterCommand(new TouchCommand());
                CommandSystem.RegisterCommand(new CatCommand());
                CommandSystem.RegisterCommand(new RmCommand());
                CommandSystem.RegisterCommand(new RmDirCommand());
                CommandSystem.RegisterCommand(new RebootCommand());
                CommandSystem.RegisterCommand(new ShutdownCommand());
                CommandSystem.RegisterCommand(new StartFTPCommand());
                CommandSystem.RegisterCommand(new StartWebServerCommand());
                CommandSystem.RegisterCommand(new WurlCommand());
                CommandSystem.RegisterCommand(new GetIPCommand());
                CommandSystem.RegisterCommand(new NetworkInfoCommand());
                CommandSystem.RegisterCommand(new GetNetworkTimeCommand());
                CommandSystem.RegisterCommand(new PingCommand());
                CommandSystem.RegisterCommand(new SysInfoCommand());
                CommandSystem.RegisterCommand(new KateCommand());
                CommandSystem.RegisterCommand(new EchoCommand());
                CommandSystem.RegisterCommand(new MvCommand());
                CommandSystem.RegisterCommand(new CpCommand());
                CommandSystem.RegisterCommand(new EncryptCommand());
                CommandSystem.RegisterCommand(new DecryptCommand());
                CommandSystem.RegisterCommand(new RTTTLCommand());
                CommandSystem.RegisterCommand(new TestCrashCommand());
                CommandSystem.RegisterCommand(new IpConfigCommand());
                CommandSystem.RegisterCommand(new SnakeCommand());
                CommandSystem.RegisterCommand(new TestConsoleWidthCommand());



                Terminal.WriteLine("Loading Terminal...");
                // Load Terminal    
                ts.Shell.SetUp();
            }
            catch (Exception e)
            {
                if (e is FatalException)
                {
                    ts.Shell.Crash(e);
                }
                else
                {
                    LogManager.Log(e.Message, LogType.Error);
                }
            }

        }
    }
}
