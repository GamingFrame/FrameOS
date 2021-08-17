using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace FrameOS
{
    public class Kernel : Sys.Kernel
    {
        public static string boottime = RTC.Month + "/" + RTC.DayOfTheMonth + "/" + RTC.Year + ", " + RTC.Hour + ":" + RTC.Minute + ":" + RTC.Second;
        protected override void BeforeRun()
        {
            VGADriverII.Initialize(VGAMode.Text90x60);
            Boot.Boot.StartUp();
        }

        protected override void Run()
        {
            try
            {
                Terminal.TextColor = ConsoleColor.Cyan;
                Terminal.Write(UserSystem.UserProfileSystem.CurrentUser);
                Terminal.TextColor = ConsoleColor.Green;
                Terminal.Write("@" + FileSystem.Filesystem.GetCurrentPath() + "> ");
                Terminal.TextColor = ConsoleColor.White;

                string input = Terminal.ReadLine();
                Terminal.TextColor = ConsoleColor.Gray;
                Shell.Shell.Run(input);

                Terminal.NewLine();
            }
            catch (Exception e)
            {
                if (e is FatalException)
                {
                    Shell.Shell.Crash(e);
                }
                else
                {
                    Terminal.WriteLine("Error: " + e.Message);
                }
            }
        }
    }

    class FatalException : Exception
    {
        public FatalException(string additionalData) : base("FatalException: " + additionalData) { }
        public FatalException() : base("FatalException") { }
    }
}
