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

                CommandSystem.RegisterCommand("help", new HelpCommand());
                CommandSystem.RegisterCommand("tetris", new TetrisCommand());
                CommandSystem.RegisterCommand("format", new FormatCommand());
                CommandSystem.RegisterCommand("whoami", new WhoamiCommand());
                CommandSystem.RegisterCommand("cd", new CDCommand());
                CommandSystem.RegisterCommand("ls", new LSCommand());
                CommandSystem.RegisterCommand("mkdir", new MKDirCommand());
                CommandSystem.RegisterCommand("touch", new TouchCommand());
                CommandSystem.RegisterCommand("cat", new CatCommand());
                CommandSystem.RegisterCommand("rm", new RmCommand());
                CommandSystem.RegisterCommand("rmdir", new RmDirCommand());
                CommandSystem.RegisterCommand("reboot", new RebootCommand());
                CommandSystem.RegisterCommand("shutdown", new ShutdownCommand());
                CommandSystem.RegisterCommand("startFTP", new StartFTPCommand());
                CommandSystem.RegisterCommand("startWeb", new StartWebServerCommand());
                CommandSystem.RegisterCommand("wurl", new WurlCommand());
                CommandSystem.RegisterCommand("getip", new GetIPCommand());
                CommandSystem.RegisterCommand("networkinfo", new NetworkInfoCommand());
                CommandSystem.RegisterCommand("time", new GetNetworkTimeCommand());
                CommandSystem.RegisterCommand("ping", new PingCommand());
                CommandSystem.RegisterCommand("sys", new SysInfoCommand());
                CommandSystem.RegisterCommand("kate", new KateCommand());
                CommandSystem.RegisterCommand("echo", new EchoCommand());
                CommandSystem.RegisterCommand("mv", new MvCommand());
                CommandSystem.RegisterCommand("cp", new CpCommand());
                CommandSystem.RegisterCommand("encrypt", new EncryptCommand());
                CommandSystem.RegisterCommand("decrypt", new DecryptCommand());
                CommandSystem.RegisterCommand("test", new RTTTLCommand());

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
                    Terminal.WriteLine("Error: " + e.Message);
                }
            }

        }
    }
}
