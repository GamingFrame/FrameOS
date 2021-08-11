using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace FrameOS.FileSystem
{
    public static class Filesystem
    {
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        private static string CurrentPath = "";
        private static string CurrentVol = "0";

        public static void SetUp()
        {
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            Console.WriteLine("Checking drive 0 is accessible...");
            try
            {
                fs.GetDirectoryListing(@"0:\");
            }
            catch (Exception)
            {
                Console.WriteLine("WARNING: Could not access drive 0!");
            }
        }

        public static void Format(string drive)
        {
            fs.Format(drive, "FAT32", true);
        }

        public static string GetCurrentPath()
        {
            return CurrentPath;
        }

        public static string GetFullPath()
        {
            return CurrentVol + @":\" + CurrentPath;
        }

        public static string GetCurrentVolume()
        {
            return CurrentVol;
        }

        internal static void SetPath(string currentPath)
        {
            if (Directory.Exists(CurrentVol + @":\" + currentPath))
            {
                CurrentPath = currentPath;
                return;
            }
            Console.WriteLine("Could not go to: " + currentPath);
        }

        public static string GetCurrentDirectory()
        {
            string[] Path = CurrentPath.Split(@"\");
            return Path[Path.Length];
        }

        public static void SetDirectory(string directory)
        {
            if(Directory.Exists(CurrentPath + @"\" + directory))
            {
                CurrentPath = CurrentPath + @"\" + directory;
                return;
            }
            Console.WriteLine(directory + " does not exist");
        }

        public static void CreateFolder(string name)
        {
            if (fs.GetDirectory(GetFullPath() + @"\" + name) != null) {
                Console.WriteLine(name + " already exists");
                return;
            }
            fs.CreateDirectory(GetFullPath() + @"\" + name);
            CurrentPath = CurrentPath + @"\" + name;
        }
    }
}
