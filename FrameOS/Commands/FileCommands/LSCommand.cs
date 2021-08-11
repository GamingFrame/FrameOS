using Cosmos.System.FileSystem.Listing;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FrameOS.FileSystem;

namespace FrameOS.Commands
{
    class LSCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            string fullPath = Filesystem.GetFullPath();
            string[] directories = Directory.GetDirectories(fullPath);

            for (int i = 0; i < directories.Length; i++)
            {
                DirectoryEntry directory = Filesystem.fs.GetDirectory(Filesystem.GetFullPath() + @"\" + directories[i]);
                if (directory == null)
                {
                    Console.WriteLine("Folder: " + directories[i]);
                }else
                {
                    Console.WriteLine("Directory name: " + directory.mName);
                }
            }

            string[] files = Directory.GetFiles(fullPath);

            for (int i = 0; i < files.Length; i++)
            {
                DirectoryEntry file = Filesystem.fs.GetFile(Filesystem.GetFullPath() + @"\" + files[i]);

                if (file == null)
                {
                    Console.WriteLine("File: " + files[i]);
                }else
                {
                    Console.WriteLine("File Name: " + file.mName + "    " + file.mSize + " bytes");
                }
            }
        }
    }
}
