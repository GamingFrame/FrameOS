using Cosmos.HAL;
using Cosmos.System.FileSystem.Listing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrameOS.Systems.Logs
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }
    public static class LogManager
    {
        public static string fileName = "";
        public static void Log(string message, LogType type)
        {
            try
            {
                if (fileName == "")
                {
                    fileName = Kernel.logFileTime + ".log";
                }


                if (!File.Exists(@"0:\System\Logs\" + fileName))
                {
                    if (Directory.Exists(@"0:\System\Logs\"))
                    {
                        FileSystem.Filesystem.fs.CreateFile(@"0:\System\Logs\" + fileName);
                    }
                    else
                    {
                        throw new FatalException("Your FrameOS has corrupted. Please reinstall FrameOS to fix this.");
                    }
                }

                string toWrite = "";

                switch (type)
                {
                    case LogType.Info:
                        toWrite = "[INFO][" + RTC.Hour + "-" + RTC.Minute + "-" + RTC.Second + "]" + " " + message + "\n";
                        break;
                    case LogType.Warning:
                        toWrite = "[WARNING][" + RTC.Hour + "-" + RTC.Minute + "-" + RTC.Second + "]" + " " + message + "\n";
                        break;
                    case LogType.Error:
                        toWrite = "[ERROR][" + RTC.Hour + "-" + RTC.Minute + "-" + RTC.Second + "]" + " " + message + "\n";
                        break;
                }

                File.AppendAllText(@"0:\System\Logs\" + fileName, toWrite);
            }
            catch (Exception e)
            {
                Shell.Shell.Crash(e);
            }

        }
    }
}
