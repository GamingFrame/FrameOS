﻿using Cosmos.System.FileSystem.Listing;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FrameOS.FileSystem;
using Cosmos.HAL;

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
                    Terminal.WriteLine("Folder: " + directories[i]);
                }else
                {
                    Terminal.WriteLine("Directory name: " + directory.mName);
                }
            }

            string[] files = Directory.GetFiles(fullPath);

            for (int i = 0; i < files.Length; i++)
            {
                DirectoryEntry file = Filesystem.fs.GetFile(Filesystem.GetFullPath() + @"\" + files[i]);

                if (file == null)
                {
                    Terminal.WriteLine("File: " + files[i]);
                }else
                {
                    Terminal.WriteLine("File Name: " + file.mName + "    " + file.mSize + " bytes");
                }
            }
        }
    }
}
