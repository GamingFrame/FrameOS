using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Systems.Encryption
{
    class Decrypter
    {

        public static string Decrypt(string v)
        {
            string[] encrypted = v.Split('|');

            string decrypted = "";

            for (int i = 0; i < encrypted.Length; i++)
            {
                double number = double.Parse(encrypted[i]);
                
                int charCode = (int)Math.Round(Pow(10, number));
                Console.WriteLine(charCode);
                decrypted += Convert.ToChar(charCode);
            }

            return decrypted;

        }

        public static double Pow(double x, double y)
        {
            double val = y * Math.Log(x);

            double result = Math.Exp(val);

            return result;
        }
    }
}
