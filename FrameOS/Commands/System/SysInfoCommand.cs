using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.Core;
using FrameOS.Systems.Memory;

namespace FrameOS.Commands
{
    class SysInfoCommand : ICommand
    {
        public string description => "See the system info.";

        public void Run(CommandArg[] commandArgs)
        {
            uint freeMemory = MemoryManager.GetFreeMemory();
            uint usedMemory = MemoryManager.GetUsedMemory();

            Terminal.TextColor = ConsoleColor.Red;
            Terminal.WriteLine("FrameOS");
            Terminal.TextColor = ConsoleColor.Gray;
            Terminal.WriteLine("CPU: " + CPU.GetCPUBrandString());
            Terminal.WriteLine("Memory: " + CPU.GetAmountOfRAM() + " MB");
            Terminal.WriteLine("Used Memory: " + usedMemory + " MB");
            Terminal.WriteLine("Free Memory: " + freeMemory + " MB");

            string hour = RTC.Hour.ToString();
            string minute = RTC.Minute.ToString();
            if (hour.Length != 2) hour = "0" + hour;
            if (minute.Length != 2) minute = "0" + minute;

            Terminal.WriteLine("BIOS Time: " + hour + ":" + minute);
            Terminal.WriteLine("Boot Time" + Kernel.boottime);
        }
    }
}
