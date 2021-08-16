using System;
using System.Collections.Generic;
using System.Text;
using cosmos = Cosmos.HAL;

namespace FrameOS.Systems.Input
{
    public class Kate
    {
        public static void printKateStartScreen()
        {
            cosmos.Terminal.Clear();
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~                               Kate - The best VIM alternative");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~                                        version 1.0");
            cosmos.Terminal.WriteLine("~                                     by Thomas and Corne");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~                        type :help<Enter>          for information");
            cosmos.Terminal.WriteLine("~                        type :q<Enter>             to exit");
            cosmos.Terminal.WriteLine("~                        type :wq<Enter>            save to file and exit");
            cosmos.Terminal.WriteLine("~                        press i                    to write");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.WriteLine("~");
            cosmos.Terminal.Write("~");
        }

        public static String stringCopy(String value)
        {
            String newString = String.Empty;

            for (int i = 0; i < value.Length - 1; i++)
            {
                newString += value[i];
            }

            return newString;
        }

        public static void printkateScreen(char[] chars, int pos, String infoBar, Boolean editMode)
        {
            int countNewLine = 0;
            int countChars = 0;
            delay(10000000);
            cosmos.Terminal.Clear();

            for (int i = 0; i < pos; i++)
            {
                if (chars[i] == '\n')
                {
                    cosmos.Terminal.WriteLine("");
                    countNewLine++;
                    countChars = 0;
                }
                else
                {
                    cosmos.Terminal.Write(chars[i].ToString());
                    countChars++;
                    if (countChars % 80 == 79)
                    {
                        countNewLine++;
                    }
                }
            }

            cosmos.Terminal.Write("|");

            for (int i = 0; i < 23 - countNewLine; i++)
            {
                cosmos.Terminal.WriteLine("");
                cosmos.Terminal.Write("~");
            }

            cosmos.Terminal.WriteLine("");
            for (int i = 0; i < 72; i++)
            {
                if (i < infoBar.Length)
                {
                    cosmos.Terminal.Write(infoBar[i].ToString());
                }
                else
                {
                    cosmos.Terminal.Write(" ");
                }
            }

            if (editMode)
            {
                cosmos.Terminal.Write(countNewLine + 1 + "," + countChars);
            }

        }

        public static String kate(String start)
        {
            Boolean editMode = false;
            int pos = 0;
            char[] chars = new char[2000];
            String infoBar = String.Empty;

            if (start == null)
            {
                printKateStartScreen();
            }
            else
            {
                pos = start.Length;

                for (int i = 0; i < start.Length; i++)
                {
                    chars[i] = start[i];
                }
                printkateScreen(chars, pos, infoBar, editMode);
            }

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (isForbiddenKey(keyInfo.Key)) continue;

                else if (!editMode && keyInfo.KeyChar == ':')
                {
                    infoBar = ":";
                    printkateScreen(chars, pos, infoBar, editMode);
                    do
                    {
                        keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            if (infoBar == ":wq")
                            {
                                String returnString = String.Empty;
                                for (int i = 0; i < pos; i++)
                                {
                                    returnString += chars[i];
                                }
                                return returnString;
                            }
                            else if (infoBar == ":q")
                            {
                                return null;

                            }
                            else if (infoBar == ":help")
                            {
                                printKateStartScreen();
                                break;
                            }
                            else
                            {
                                infoBar = "ERROR: No such command";
                                printkateScreen(chars, pos, infoBar, editMode);
                                break;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            infoBar = stringCopy(infoBar);
                            printkateScreen(chars, pos, infoBar, editMode);
                        }
                        else if (keyInfo.KeyChar == 'q')
                        {
                            infoBar += "q";
                        }
                        else if (keyInfo.KeyChar == ':')
                        {
                            infoBar += ":";
                        }
                        else if (keyInfo.KeyChar == 'w')
                        {
                            infoBar += "w";
                        }
                        else if (keyInfo.KeyChar == 'h')
                        {
                            infoBar += "h";
                        }
                        else if (keyInfo.KeyChar == 'e')
                        {
                            infoBar += "e";
                        }
                        else if (keyInfo.KeyChar == 'l')
                        {
                            infoBar += "l";
                        }
                        else if (keyInfo.KeyChar == 'p')
                        {
                            infoBar += "p";
                        }
                        else
                        {
                            continue;
                        }
                        printkateScreen(chars, pos, infoBar, editMode);



                    } while (keyInfo.Key != ConsoleKey.Escape);
                }

                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    editMode = false;
                    infoBar = String.Empty;
                    printkateScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.I && !editMode)
                {
                    editMode = true;
                    infoBar = "-- INSERT --";
                    printkateScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.Enter && editMode && pos >= 0)
                {
                    chars[pos++] = '\n';
                    printkateScreen(chars, pos, infoBar, editMode);
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && editMode && pos >= 0)
                {
                    if (pos > 0) pos--;

                    chars[pos] = '\0';

                    printkateScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                if (editMode && pos >= 0)
                {
                    chars[pos++] = keyInfo.KeyChar;
                    printkateScreen(chars, pos, infoBar, editMode);
                }

            } while (true);
        }

        public static bool isForbiddenKey(ConsoleKey key)
        {
            ConsoleKey[] forbiddenKeys = { ConsoleKey.Print, ConsoleKey.PrintScreen, ConsoleKey.Pause, ConsoleKey.Home, ConsoleKey.PageUp, ConsoleKey.PageDown, ConsoleKey.End, ConsoleKey.NumPad0, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.NumPad4, ConsoleKey.NumPad5, ConsoleKey.NumPad6, ConsoleKey.NumPad7, ConsoleKey.NumPad8, ConsoleKey.NumPad9, ConsoleKey.Insert, ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.F4, ConsoleKey.F5, ConsoleKey.F6, ConsoleKey.F7, ConsoleKey.F8, ConsoleKey.F9, ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12, ConsoleKey.Add, ConsoleKey.Divide, ConsoleKey.Multiply, ConsoleKey.Subtract, ConsoleKey.LeftWindows, ConsoleKey.RightWindows };
            for (int i = 0; i < forbiddenKeys.Length; i++)
            {
                if (key == forbiddenKeys[i]) return true;
            }
            return false;
        }

        public static void delay(int time)
        {
            for (int i = 0; i < time; i++) ;
        }

        public static void Startkate(string fileName)
        {
            try
            {
                if (FileSystem.Filesystem.fileExists(fileName))
                {
                    cosmos.Terminal.WriteLine("Found file!");
                }
                else
                {
                    cosmos.Terminal.WriteLine("Creating file!");
                    FileSystem.Filesystem.CreateFile(fileName);
                }
                cosmos.Terminal.Clear();
            }
            catch (Exception ex)
            {
                cosmos.Terminal.WriteLine(ex.Message);
            }

            String text = String.Empty;
            text = kate(string.Join("\n", FileSystem.Filesystem.GetFileContent(fileName)));

            cosmos.Terminal.Clear();

            if (text != null)
            {
                FileSystem.Filesystem.writeAllText(fileName, text);
                cosmos.Terminal.WriteLine("Content has been saved to " + fileName);
            }
            cosmos.Terminal.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
