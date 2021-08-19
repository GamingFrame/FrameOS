using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Systems.Networking;
using Cosmos.System.Network.IPv4.TCP.FTP;
using FrameOS.FileSystem;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class StartFTPCommand : ICommand
    {
        public string description { get => "Start a FTP server."; }

        public string command => "startFTP";

        public void Run(CommandArg[] commandArgs)
        {
            Terminal.WriteLine("Started listening on: " + NetworkSystem.GetLocalIP());
            StartFTPServer();
            ftp.Listen();
        }

        private FtpServer ftp;
        public void StartFTPServer()
        {
            ftp = new FtpServer(Filesystem.fs, @"0:\");
        }
    }
}
