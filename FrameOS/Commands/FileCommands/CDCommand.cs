using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrameOS.Commands
{
    class CDCommand : ICommand
    {
        public void Run(CommandArg[] commandArgs)
        {
            string currentPath = FileSystem.Filesystem.GetCurrentPath();

            if (commandArgs[0].String == "..")
            {
                if (FileSystem.Filesystem.GetCurrentPath() == "")
                {
                    return;
                }
                char currletter = currentPath[currentPath.Length - 1];

                while (!(currletter == "\\".ToCharArray()[0]))
                {
                    currentPath = currentPath.Remove(currentPath.Length - 1);
                    if (currentPath.Length == 0) {
                        FileSystem.Filesystem.SetPath(currentPath);
                        return; 
                    }
                    currletter = currentPath[currentPath.Length - 1];
                }

                if (currentPath.Length == 0) { return; }
                currentPath = currentPath.Remove(currentPath.Length - 1);

                FileSystem.Filesystem.SetPath(currentPath);
                return;
            }
            if (currentPath == "")
            {
                currentPath += commandArgs[0].String;
            }
            else
            {
                currentPath += "\\" + commandArgs[0].String;
            }
            FileSystem.Filesystem.SetPath(currentPath);
        }
    }
}
