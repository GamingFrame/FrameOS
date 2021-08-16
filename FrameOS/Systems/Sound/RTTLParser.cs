using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FrameOS.Systems.Sound
{
    class Note
    {
        public string note;
        public string duration;
        public string frequency;
    }

    public class RTTLParser
    {
        public static string TestMelody = "tetris:d=4,o=5,b=160:e6,8b,8c6,8d6,16e6,16d6,8c6,8b,a,8a,8c6,e6,8d6,8c6,b,8b,8c6,d6,e6,c6,a,2a,8p,d6,8f6,a6,8g6,8f6,e6,8e6,8c6,e6,8d6,8c6,b,8b,8c6,d6,e6,c6,a,a";
        public static string MarioMelody = "mario:d=4,o=5,b=100:16e6,16e6,32p,8e6,16c6,8e6,8g6,8p,8g,8p,8c6,16p,8g,16p,8e,16p,8a,8b,16a#,8a,16g.,16e6,16g6,8a6,16f6,8g6,8e6,16c6,16d6,8b,16p,8c6,16p,8g,16p,8e,16p,8a,8b,16a#,8a,16g.,16e6,16g6,8a6,16f6,8g6,8e6,16c6,16d6,8b,8p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16g#,16a,16c6,16p,16a,16c6,16d6,8p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16c7,16p,16c7,16c7,p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16g#,16a,16c6,16p,16a,16c6,16d6,8p,16d#6,8p,16d6,8p,16c6";


        public static void test()
        {
            string[] s = TestMelody.Split(":");

            #region Variables
            string name = "";
            int duration = 0;
            int octave = 0;
            int bpm = 0;
            Note[] notes;
            #endregion

            #region Get Name
            name = s[0];
            #endregion

            #region Get Defaults
            string[] defaults = s[1].Split(",");

            foreach (var item in defaults)
            {
                if (item == "")
                {
                    return;
                }

                string[] key_val = item.Split("=");

                if (key_val.Length != 2)
                {
                    throw new Exception("Invalid setting " + item);
                }

                string key = key_val[0];
                string val = key_val[1];

                List<string> allowed_durations = new List<string> { "1", "2", "4", "8", "16", "32" };
                List<string> allowed_octaves = new List<string> { "4", "5", "6", "7" };
                List<string> allowed_bpm = new List<string> {"25", "28", "31", "35", "40", "45", "50", "56", "63", "70", "80", "90", "100",
                "112", "125", "140", "160", "180", "200", "225", "250", "285", "320", "355",
                "400", "450", "500", "565", "635", "715", "800", "900"};

                switch (key)
                {
                    case "d":
                        if (!allowed_durations.Contains(val))
                        {
                            throw new Exception("Invalid duration " + val);
                        }
                        else
                        {
                            if (!int.TryParse(val, out duration))
                            {
                                throw new Exception("Invalid duration " + val);
                            }
                        }
                        break;
                    case "o":
                        if (!allowed_octaves.Contains(val))
                        {
                            throw new Exception("Invalid octave " + val);
                        }
                        else
                        {
                            if (!int.TryParse(val, out octave))
                            {
                                throw new Exception("Invalid octave " + val);
                            }
                        }
                        break;
                    case "b":
                        if (!allowed_bpm.Contains(val))
                        {
                            throw new Exception("Invalid BPM " + val);
                        }
                        else
                        {
                            if (!int.TryParse(val, out bpm))
                            {
                                throw new Exception("Invalid BPM " + val);
                            }
                        }
                        break;
                }
            }
            #endregion


            #region Get Melody... oh boy here we go
            string[] v = s[2].Split(",");

            int beat_every = 60000 / bpm;

            foreach (var item in v)
            {
                Regex rx = new Regex(@"/(1|2|4|8|16|32|64)?((?:[a-g]|h|p)#?){1}(\.?)(4|5|6|7)?/");

                MatchCollection matches = rx.Matches(item);

                int noteDuration;
                string noteNote;
                bool isDotted;
                int noteOctave;

                if (!int.TryParse(matches[0].Value, out noteDuration))
                {
                    noteDuration = duration;
                }

                

                
            }
            #endregion

        }
    }
}
