using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FrameOS.Systems.Sound
{
    public class Note
    {
        public string note { get; set; }
        public int duration { get; set; }
        public double frequency { get; set; }
    }

    public static class RTTTLParser
    {
        public static string TestMelody = "tetris:d=4,o=5,b=160:e6,8b,8c6,8d6,16e6,16d6,8c6,8b,a,8a,8c6,e6,8d6,8c6,b,8b,8c6,d6,e6,c6,a,2a,8p,d6,8f6,a6,8g6,8f6,e6,8e6,8c6,e6,8d6,8c6,b,8b,8c6,d6,e6,c6,a,a";
        public static string MarioMelody = "mario:d=4,o=5,b=100:16e6,16e6,32p,8e6,16c6,8e6,8g6,8p,8g,8p,8c6,16p,8g,16p,8e,16p,8a,8b,16a#,8a,16g.,16e6,16g6,8a6,16f6,8g6,8e6,16c6,16d6,8b,16p,8c6,16p,8g,16p,8e,16p,8a,8b,16a#,8a,16g.,16e6,16g6,8a6,16f6,8g6,8e6,16c6,16d6,8b,8p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16g#,16a,16c6,16p,16a,16c6,16d6,8p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16c7,16p,16c7,16c7,p,16g6,16f#6,16f6,16d#6,16p,16e6,16p,16g#,16a,16c6,16p,16a,16c6,16d6,8p,16d#6,8p,16d6,8p,16c6";



        public static void Play(string song)
        {
            #region
            #endregion
            Note[] notes = GetData(song);

            foreach (var item in notes)
            {
                if (item.note.Replace("\0", "") == "p")
                {
                    DelayCode(item.duration);
                }
                else
                {
                    Cosmos.System.PCSpeaker.Beep((uint)item.frequency, (uint)item.duration);
                }
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

        public static Note[] GetData(string melody)
        {
            string[] s = melody.Split(":");

            #region Variables
            string name = "";
            int duration = 0;
            int octave = 0;
            int bpm = 0;
            List<Note> notes = new List<Note>();
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
                    return null;
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
                        if (!ListContains(allowed_durations,val))
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
                        if (!ListContains(allowed_octaves, val))
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
                        if (!ListContains(allowed_bpm, val))
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
                //Regex rx = new Regex(@"/(1|2|4|8|16|32|64)?((?:[a-g]|h|p)#?){1}(\.?)(4|5|6|7)?/");

                //Match matches = rx.Match(item);

                int noteDuration = 0;
                string noteNote = "";
                bool isDotted = false;
                int noteOctave = 0;


                bool isDurationChecked = false;
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == '.')
                    {
                        isDotted = true;
                        continue;
                    }

                    string number = "";

                    int temp;

                    while(int.TryParse(item[i].ToString(), out temp))
                    {
                        number += item[i];
                        i++;
                    }
                    if (number.Length > 0)
                    {
                        if (isDurationChecked == false)
                        {
                            noteDuration = int.Parse(number);
                            number = "";
                        }
                        else
                        {
                            noteOctave = int.Parse(number);
                            number = "";
                        }
                    }

                    if (isDurationChecked == false)
                    {
                        isDurationChecked = true;
                    }

                    if (number == "")
                    {
                        noteNote += item[i];
                    }
                }

                if (noteDuration == 0)
                {
                    noteDuration = duration;
                }
                if (noteOctave == 0)
                {
                    noteOctave = octave;
                }

                //if (!int.TryParse(matches.Groups[0].Value, out noteDuration))
                //{
                //    noteDuration = duration;
                //}
                //if (matches.Groups[1].Value == "h")
                //{
                //    noteNote = "b";
                //}
                //else
                //{
                //    noteNote = matches.Groups[1].Value;
                //}
                //if (matches.Groups[2].Value == ".")
                //{
                //    isDotted = true;
                //}
                //if (!int.TryParse(matches.Groups[3].Value, out noteOctave))
                //{
                //    noteOctave = octave;
                //}

                Note note = new Note();
                note.duration = CalculateDuration(beat_every, noteDuration, isDotted);
                note.frequency = CalculateFrequency(noteNote, noteOctave);
                note.note = noteNote;

                notes.Add(note);

            }
            #endregion

            return notes.ToArray();
        }


        public static double CalculateFrequency(string note, int octave)
        {
            if (note == "p")
            {
                return 0;
            }

            double C4 = 261.63f;
            double twelfth_root = Math.Pow(2, (double)1 / 12);
            double N = CalculateSemitonesFromC4(note, octave);
            double frequency = C4 * Math.Pow(twelfth_root, N);

            return Math.Round(frequency * 1e1) / 1e1;
        }

        public static int CalculateDuration(int beatEvery, int noteDuration, bool isDotted)
        {
            int duration = (beatEvery * 4) / noteDuration;
            int prolonged = 0;
            if (isDotted)
            {
                prolonged = duration / 2;
            }

            return duration + prolonged;
        }


        private static double CalculateSemitonesFromC4(string note, int octave)
        {
            string[] note_order = { "c", "c#", "d", "d#", "e", "f", "f#", "g", "g#", "a", "a#", "b" };

            double middle_octave = 4;

            double semitones_in_octave = 12;

            double octave_jump = (octave - middle_octave) * semitones_in_octave;
            return IndexOf(note_order, note.Replace("\0", "")) + octave_jump;
        }

        public static bool ListContains(List<string> t, string value)
        {
            foreach (var item in t)
            {
                if (item == value)
                {
                    return true;
                }
            }
            return false;
        }

        public static int IndexOf(string[] t, string value)
        {
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }
        //public static string Trim(string s)
        //{
        //    string str = "";

        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        if (s[i] != '\0')
        //        {
        //            str += s[i];
        //        }
        //    }

        //    return str;
        //}
    }
}
