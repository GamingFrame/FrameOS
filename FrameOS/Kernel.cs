using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace FrameOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            VGADriverII.Initialize(VGAMode.Text90x60);
            Boot.Boot.StartUp();
        }

        protected override void Run()
        {
            try
            {
                Terminal.Write(UserSystem.UserProfileSystem.CurrentUser + "@" + FileSystem.Filesystem.GetCurrentPath() + ">");
                Shell.Shell.Run(Terminal.ReadLine());
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
