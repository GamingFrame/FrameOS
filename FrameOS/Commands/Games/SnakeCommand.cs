using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;
using FrameOS.Games;
using Cosmos.HAL;

namespace FrameOS.Commands
{
    class SnakeCommand : ICommand
    {
        public string description => "Play snake!";

        public string command => "snake";

        public void Run(CommandArg[] commandArgs)
        {
            Snake snake = new Snake();
            VGADriverII.SetMode(VGAMode.Text80x25);
            snake.Run();
        }
    }
}
