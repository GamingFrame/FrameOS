using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using sys = Cosmos.System;

namespace FrameOS.Systems.Sound
{
    public static class AudioSystem
    {
        public static void PlayTetris()
        {
            Tetris tetris = new Tetris();

            for (int thisNote = 0; thisNote < tetris.melody.Length; thisNote++)
            {

                uint noteDuration = 1000 / tetris.noteDurations[thisNote];

                if (tetris.melody[thisNote] == "0")
                {
                    DelayCode(noteDuration);
                }else
                {
                    float freq;
                    Notes.frequencies.TryGetValue(tetris.melody[thisNote], out freq);
                    uint vOut = Convert.ToUInt32(freq);
                    sys.PCSpeaker.Beep(vOut, noteDuration);
                }
                DelayCode(noteDuration / 2.1);
            }
        }

        public static void PlayCantina()
        {
            // End my suffering
            CantinaBand cantinaBand = new CantinaBand();

            for (int thisNote = 0; thisNote < cantinaBand.melody.Length; thisNote++)
            {

                uint noteDuration = 1000 / cantinaBand.noteDurations[thisNote];

                if (cantinaBand.melody[thisNote] == "0")
                {
                    DelayCode(noteDuration);
                }
                else
                {
                    float freq;
                    Notes.frequencies.TryGetValue(cantinaBand.melody[thisNote], out freq);
                    uint vOut = Convert.ToUInt32(freq);
                    sys.PCSpeaker.Beep(vOut, noteDuration);
                }
                DelayCode(noteDuration / 2.1);
            }
        }

        static void DelayCode(double ms)
        {
            for (int i = 0; i < ms * 100000; i++)
            {
                ;
                ;
                ;
                ;
                ;
            }
        }
    }

    public class Tetris
    {
        public string[] melody = {
          "NOTE_E5", "NOTE_E3", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_E5", "NOTE_D5", "NOTE_C5",
          "NOTE_B4", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_C5", "NOTE_E5", "NOTE_A3", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_G4", "NOTE_C5", "NOTE_D5", "NOTE_E3", "NOTE_E5",
          "NOTE_E3", "NOTE_C5", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_B2",
          "NOTE_C3", "NOTE_D3", "NOTE_D5", "NOTE_F5", "NOTE_A5", "NOTE_C5", "NOTE_C5", "NOTE_G5",
          "NOTE_F5", "NOTE_E5", "NOTE_C3", "0", "NOTE_C5", "NOTE_E5", "NOTE_A4", "NOTE_G4", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_G4", "NOTE_E5",
          "NOTE_G4", "NOTE_C5", "NOTE_E4", "NOTE_A4", "NOTE_E3", "NOTE_A4", "0",
          "NOTE_E5", "NOTE_E3", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_E5", "NOTE_D5", "NOTE_C5",
          "NOTE_B4", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_C5", "NOTE_E5", "NOTE_A3", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_G4", "NOTE_C5", "NOTE_D5", "NOTE_E3", "NOTE_E5",
          "NOTE_E3", "NOTE_C5", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_B2",
          "NOTE_C3", "NOTE_D3", "NOTE_D5", "NOTE_F5", "NOTE_A5", "NOTE_C5", "NOTE_C5", "NOTE_G5",
          "NOTE_F5", "NOTE_E5", "NOTE_C3", "0", "NOTE_C5", "NOTE_E5", "NOTE_A4", "NOTE_G4", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_G4", "NOTE_E5",
          "NOTE_G4", "NOTE_C5", "NOTE_E4", "NOTE_A4", "NOTE_E3", "NOTE_A4", "0",
          "NOTE_E4", "NOTE_E3", "NOTE_A2", "NOTE_E3", "NOTE_C4", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_D4", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_B3", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
          "NOTE_C4", "NOTE_E3", "NOTE_A2", "NOTE_E3", "NOTE_A3", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_G3S", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_B3", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
          "NOTE_E4", "NOTE_E3", "NOTE_A2", "NOTE_E3", "NOTE_C4", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_D4", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_B3", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
          "NOTE_C4", "NOTE_E3", "NOTE_E4", "NOTE_E3", "NOTE_A4", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_G4S", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
          "NOTE_E5", "NOTE_E3", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_E5", "NOTE_D5", "NOTE_C5",
          "NOTE_B4", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_C5", "NOTE_E5", "NOTE_A3", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_G4", "NOTE_C5", "NOTE_D5", "NOTE_E3", "NOTE_E5",
          "NOTE_E3", "NOTE_C5", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_B2",
          "NOTE_C3", "NOTE_D3", "NOTE_D5", "NOTE_F5", "NOTE_A5", "NOTE_C5", "NOTE_C5", "NOTE_G5",
          "NOTE_F5", "NOTE_E5", "NOTE_C3", "0", "NOTE_C5", "NOTE_E5", "NOTE_A4", "NOTE_G4", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_G4", "NOTE_E5",
          "NOTE_G4", "NOTE_C5", "NOTE_E4", "NOTE_A4", "NOTE_E3", "NOTE_A4", "0",
          "NOTE_E5", "NOTE_E3", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_E5", "NOTE_D5", "NOT5",
          "NOTE_B4", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_C5", "NOTE_E5", "NOTE_A3", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_G4", "NOTE_C5", "NOTE_D5", "NOTE_E3", "NOTE_E5",
          "NOTE_E3", "NOTE_C5", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_A4", "NOTE_A3", "NOTE_B2",
          "NOTE_C3", "NOTE_D3", "NOTE_D5", "NOTE_F5", "NOTE_A5", "NOTE_C5", "NOTE_C5", "NOTE_G5",
          "NOTE_F5", "NOTE_E5", "NOTE_C3", "0", "NOTE_C5", "NOTE_E5", "NOTE_A4", "NOTE_G4", "NOTE_D5",
          "NOTE_C5", "NOTE_B4", "NOTE_E4", "NOTE_B4", "NOTE_C5", "NOTE_D5", "NOTE_G4", "NOTE_E5",
          "NOTE_G4", "NOTE_C5", "NOTE_E4", "NOTE_A4", "NOTE_E3", "NOTE_A4", "0",
          "NOTE_E4", "NOTE_E3", "NOTE_A2", "NOTE_E3", "NOTE_C4", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_D4", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_B3", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
          "NOTE_C4", "NOTE_E3", "NOTE_A2", "NOTE_E3", "NOTE_A3", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_G3S", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_B3", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
          "NOTE_E4", "NOTE_E3", "NOTE_A2", "NOTE_E3", "NOTE_C4", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_D4", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_B3", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
          "NOTE_C4", "NOTE_E3", "NOTE_E4", "NOTE_E3", "NOTE_A4", "NOTE_E3", "NOTE_A2", "NOTE_E3",
          "NOTE_G4S", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_G2S", "NOTE_E3", "NOTE_G2S", "NOTE_E3",
        };

        //note durations: 4 = quarter note, 8 = eighth note, etc
        public uint[] noteDurations = {
          8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
          8,4,8,8,16,16,8,8,8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,4,4,
          8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
          8,4,8,8,16,16,8,8,8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,4,4,
          8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
          8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
          8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
          8,4,8,8,16,16,8,8,8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,4,4,
          8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
          8,4,8,8,16,16,8,8,8,8,8,8,8,16,16,8,8,8,8,8,8,8,8,8,8,8,8,8,8,4,4,
          8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
          8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,
        };
    }

    public class CantinaBand
    {
        public string[] melody = {
              "NOTE_B4", "NOTE_E5", "NOTE_B4", "NOTE_E5",
              "NOTE_B4",  "NOTE_E5", "NOTE_B4", "0",  "NOTE_A4S", "NOTE_B4",
              "NOTE_B4",  "NOTE_A4S", "NOTE_B4", "NOTE_A4", "0", "NOTE_G4S", "NOTE_A4", "NOTE_G4",
              "NOTE_G4",  "NOTE_E4",
              "NOTE_B4", "NOTE_E5", "NOTE_B4", "NOTE_E5",
              "NOTE_B4",  "NOTE_E5", "NOTE_B4", "0",  "NOTE_A4S", "NOTE_B4",
              "NOTE_A4", "NOTE_A4", "NOTE_G4S", "NOTE_A4",
              "NOTE_D5",  "NOTE_C5", "NOTE_B4", "NOTE_A4",
              "NOTE_B4", "NOTE_E5", "NOTE_B4", "NOTE_E5",
              "NOTE_B4",  "NOTE_E5", "NOTE_B4", "0",  "NOTE_A4S", "NOTE_B4",
              "NOTE_D5", "NOTE_D5", "NOTE_B4", "NOTE_A4",
              "NOTE_G4", "NOTE_E4",
              "NOTE_E4", "NOTE_G4",
              "NOTE_B4", "NOTE_D5",
              "NOTE_F5", "NOTE_E5", "NOTE_A4S", "NOTE_A4S", "NOTE_B4", "NOTE_G4",
        };

        public uint[] noteDurations = {
            4,4,4,4,8,4,8,8,8,8,8,8,8,8,8,8,8,8,4,2,4,4,4,4,8,4,8,8,8,8,4,4,8,4,8,4,4,4,4,4,4,4,8,4,8,8,8,8,4,4,8,4,4,2,2,2,2,2,4,4,8,8,4,4,
        };

    }
}
