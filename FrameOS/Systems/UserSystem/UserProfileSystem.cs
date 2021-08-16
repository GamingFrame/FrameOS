using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FrameOS.Systems.Encryption;

namespace FrameOS.UserSystem
{
    public class UserProfileSystem
    {
        public static string CurrentUser { get; private set; }
        public static UserPermLevel CurrentPermLevel { get; private set; }


        public static bool FirstTime()
        {
            if (!Directory.Exists(@"0:\Users"))
            {
                return true;
            }

            return false;
        }

        public static bool UserExists(string username)
        {
            if (Directory.Exists(@"0:\Users\" + username))
            {
                if (File.Exists(@"0:\Users\" + username + @"\!USERC"))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Authorize(string username, string password)
        {
            string hashed_password = File.ReadAllLines(@"0:\Users\" + username + @"\!USERC")[0];
            return Hashing.verifyHash(password, hashed_password);
            /*            return File.ReadAllLines(@"0:\Users\" + username + @"\USERC")[0] == password;
            */
        }

        public static bool Login(string username, string password)
        {
            if (UserExists(username))
            {
                if (Authorize(username, password))
                {
                    CurrentUser = username;
                    CurrentPermLevel = (UserPermLevel)int.Parse(File.ReadAllLines(@"0:\Users\" + username + @"\!USERC")[1]);
                    return true;
                }
            }
            return false;
        }

        public static void CreateUser(string username, string password, int perm)
        {
            if (!Directory.Exists(@"0:\Users"))
            {
                Directory.CreateDirectory(@"0:\Users");
            }
            Directory.CreateDirectory(@"0:\Users\" + username);
            string hashed_password = Hashing.generateHash(password);
            File.WriteAllLines(@"0:\Users\" + username + @"\!USERC", new string[] { hashed_password, perm.ToString() });
        }
    }

    public enum UserPermLevel
    {
        Guest = 0,
        User = 1,
        Administrator = 2,
        System = 3,
    }
}
