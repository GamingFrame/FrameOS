using System;
using System.Collections.Generic;

namespace FrameOS.Systems.Encryption
{
    public class Encrypter
    {
        public static string Encrypt(string v)
        {
            char[] letters = v.ToCharArray();

            string encryptedValue = "";

            for (int i = 0; i < letters.Length; i++)
            {
                //encryptedValue += DecimalEx.Log10(letters[i].GetHashCode()) + "|";
                Console.WriteLine(letters[i].GetHashCode());
            }

            return encryptedValue;
        }
    }
}
