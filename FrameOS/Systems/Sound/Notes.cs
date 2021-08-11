using System;
using System.Collections.Generic;
using System.Text;
namespace FrameOS.Systems.Sound
{
    public static class Notes
    {
        public static Dictionary<string, float> frequencies = new Dictionary<string, float> { { "NOTE_C0", 16.35f }, { "NOTE_C0S", 17.32f }, { "NOTE_D0", 18.35f }, { "NOTE_D0S", 19.45f },
        { "NOTE_E0", 20.60f }, { "NOTE_F0", 21.83f }, { "NOTE_F0S", 23.12f }, { "NOTE_G0", 24.50f }, { "NOTE_G0S", 25.96f }, { "NOTE_A0", 27.50f }, { "NOTE_A0S", 29.14f },
        { "NOTE_B0", 30.87f }, { "NOTE_C1", 32.70f }, { "NOTE_C1S", 34.65f }, { "NOTE_D1", 36.71f }, { "NOTE_D1S", 38.89f }, { "NOTE_E1", 41.20f }, { "NOTE_F1", 43.65f },
        { "NOTE_F1S", 46.25f }, { "NOTE_G1", 49.00f }, { "NOTE_G1S", 51.91f }, { "NOTE_A1", 55.00f }, { "NOTE_A1S", 58.27f }, { "NOTE_B1", 61.74f }, { "NOTE_C2", 65.41f }, { "NOTE_C2S", 69.30f },
        { "NOTE_D2", 73.42f }, { "NOTE_D2S", 77.78f }, { "NOTE_E2", 82.41f }, { "NOTE_F2", 87.31f }, { "NOTE_F2S", 92.50f }, { "NOTE_G2", 98.00f }, { "NOTE_G2S", 103.83f },
        { "NOTE_A2", 110.00f }, { "NOTE_A2S", 116.54f }, { "NOTE_B2", 123.47f }, { "NOTE_C3", 130.81f }, { "NOTE_C3S", 138.59f }, { "NOTE_D3", 146.83f }, { "NOTE_D3S", 155.56f },
        { "NOTE_E3", 164.81f }, { "NOTE_F3", 174.61f }, { "NOTE_F3S", 185.00f }, { "NOTE_G3", 196.00f }, { "NOTE_G3S", 207.65f }, { "NOTE_A3", 220.00f }, { "NOTE_A3S", 233.08f }, { "NOTE_B3", 246.94f },
        { "NOTE_C4", 261.63f }, { "NOTE_C4S", 277.18f }, { "NOTE_D4", 293.66f }, { "NOTE_D4S", 311.13f }, { "NOTE_E4", 329.63f }, { "NOTE_F4", 349.23f }, { "NOTE_F4S", 369.99f }, { "NOTE_G4", 392.00f }, 
        { "NOTE_G4S", 415.30f }, { "NOTE_A4", 440.00f }, { "NOTE_A4S", 466.16f }, { "NOTE_B4", 493.88f }, { "NOTE_C5", 523.25f }, { "NOTE_C5S", 554.37f }, { "NOTE_D5", 587.33f }, { "NOTE_D5S", 622.25f },
        { "NOTE_E5", 659.25f }, { "NOTE_F5", 698.46f }, { "NOTE_F5S", 739.99f }, { "NOTE_G5", 783.99f }, { "NOTE_G5S", 830.61f }, { "NOTE_A5", 880.00f }, { "NOTE_A5S", 932.33f },
        { "NOTE_B5", 987.77f }, { "NOTE_C6", 1046.50f }, { "NOTE_C6S", 1108.73f }, { "NOTE_D6", 1174.66f }, { "NOTE_D6S", 1244.51f }, { "NOTE_E6", 1318.51f }, { "NOTE_F6", 1396.91f },};
    }
}
