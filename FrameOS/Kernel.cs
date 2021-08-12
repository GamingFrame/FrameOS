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
            Boot.Boot.StartUp();
        }

        protected override void Run()
        {
            try
            {
                Sys.Notes.AS1
                Console.Write(UserSystem.UserProfileSystem.CurrentUser + "@" + FileSystem.Filesystem.GetCurrentPath() + ">");
                Terminal.Terminal.Run(Terminal.Terminal.ReadLine());
            }
            catch (Exception e)
            {
                if (e is FatalException)
                {
                    Terminal.Terminal.Crash(e);
                }
                else
                {
                    Console.WriteLine("Error: " + e.Message);
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
