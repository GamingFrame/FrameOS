﻿using Cosmos.HAL;
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

            Terminal.WriteLine("Checking drive 0 is accessible...");
            try
            {
                //fs.GetDirectoryListing(@"0:\");
                if (!Directory.Exists(@"0:\System"))
                {
                    Directory.CreateDirectory(@"0:\System");
                    Directory.CreateDirectory(@"0:\System\Environment");
                    Directory.CreateDirectory(@"0:\System\Audio");
                    Directory.CreateDirectory(@"0:\System\Commands");
                    Directory.CreateDirectory(@"0:\System\Logs");
                }
                if (!Directory.Exists(@"0:\httpdocs"))
                {
                    Directory.CreateDirectory(@"0:\httpdocs");
                    string[] default_file = { "<!DOCTYPE html>",
                        "<html lang=\"en\">",
                        "<head>",
                        "    <meta charset=\"UTF-8\">",
                        "    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">",
                        "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">",
                        "    <title>Frame OS</title>",
                        "</head>",
                        "<body>",
                        "   <h1>Frame Web Server V0.1</h1>",
                        "   <p>Web server is running! Enjoy!</p>",
                        "</body>"};

                    File.WriteAllLines(@"0:\httpdocs\index.html", default_file);
                }
            }
            catch (Exception)
            {
                Terminal.WriteLine("WARNING: Could not access drive 0!");
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
            Terminal.WriteLine("Could not go to: " + currentPath);
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
            Terminal.WriteLine(directory + " does not exist");
        }

        public static void CreateFolder(string name)
        {
            if (fs.GetDirectory(GetFullPath() + @"\" + name) != null) {
                Terminal.WriteLine(name + " already exists");
                return;
            }
            fs.CreateDirectory(GetFullPath() + @"\" + name);
            CurrentPath = CurrentPath + @"\" + name;
        }

        public static void CreateFile(string name)
        {
            if (fs.GetFile(GetFullPath() + @"\" + name) != null)
            {
                Terminal.WriteLine(name + " already exists");
                return;
            }

            fs.CreateFile(GetFullPath() + @"\" + name);
        }

        public static string[] GetFileContent(string fileName)
        {
            if (fs.GetFile(GetFullPath() + @"\" + fileName) == null)
            {
                Terminal.WriteLine(fileName + " does not exists");
                return new string[] { };
            }

            string[] lines = File.ReadAllLines(GetFullPath() + @"\" + fileName);

            if(lines.Length == 0)
            {
                Terminal.WriteLine(fileName + " does not have content");
                return new string[] { };
            }

            return lines;
        }
 

        public static void RemoveFile(string fileName)
        {
            if (fs.GetFile(GetFullPath() + @"\" + fileName) == null)
            {
                Terminal.WriteLine(fileName + " does not exists");
                return;
            }

            File.Delete(GetFullPath() + @"\" + fileName);
        }

        public static void RemoveFolder(string folder)
        {
            if (fs.GetDirectory(GetFullPath() + @"\" + folder) == null)
            {
                Terminal.WriteLine(folder + " does not exists");
                return;
            }

            Directory.Delete(GetFullPath() + @"\" + folder);
        }

        public static bool fileExists(string file)
        {
           return fs.GetFile(GetFullPath() + @"\" + file) != null;
        }

        public static void writeAllText(string file, string text)
        {
            File.WriteAllText(GetFullPath() + @"\" + file, text);
        }

        public static bool folderExists(string folder)
        {
            return fs.GetDirectory(GetFullPath() + @"\" + folder) != null;
        }

        public static void moveFile(string fileName, string newDestinaction)
        {
            if(!fileExists(fileName))
            {
                Terminal.WriteLine("File " + fileName + " cannot be found!");
                return;
            }

            if (!folderExists(newDestinaction))
            {
                Terminal.WriteLine("Folder " + fileName + " cannot be found!");
                return;
            }

            string[] fileContent = GetFileContent(fileName);
            RemoveFile(fileName);
            CreateFile(newDestinaction + @"\" + fileName);
            writeAllText(newDestinaction + @"\" + fileName, string.Join("\n", fileContent));
        }

        public static void copyFile(string fileName, string newDestinaction)
        {
            if (!fileExists(fileName))
            {
                Terminal.WriteLine("File " + fileName + " cannot be found!");
                return;
            }

            if (!folderExists(newDestinaction))
            {
                Terminal.WriteLine("Folder " + fileName + " cannot be found!");
                return;
            }

            string[] fileContent = GetFileContent(fileName);
            CreateFile(newDestinaction + @"\" + fileName);
            writeAllText(newDestinaction + @"\" + fileName, string.Join("\n", fileContent));
        }
    }
}
