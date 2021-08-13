using FrameOS.Systems.CommandSystem;
using FrameOS.Systems.Sound;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class TetrisCommand : ICommand
    {
        public string description { get => "Play the tetris theme song."; }

        public void Run(CommandArg[] commandArgs)
        {
            AudioSystem.PlayTetris();
        }
    }
}
