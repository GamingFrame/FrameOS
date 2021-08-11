using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Systems.Terminal
{
    class TerminalHistory
    {
        private int _stackSize;

        public TerminalHistory(int stackSize)
        {
            _stackSize = stackSize;
        }

        private List<string> history = new List<string>();

        public void AddToHistory(string v)
        {
            if (v.Trim() == "") return;

            while (history.Count >= _stackSize)
            {
                history.RemoveAt(0);
            }

            history.Add(v);
        }

        public string GetHistory(int index)
        {
            int i = history.Count - index;
            i = Math.Min(i, 0);

            return history[i];
        }

        public int Max() => _stackSize;
    }
}
