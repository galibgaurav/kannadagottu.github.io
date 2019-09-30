using System;
using System.Text;

namespace HelperComponent
{
    public class PasswordHelper
    {
        public static string GetTempPassword(int lenght=15)
        {
            string validchars = "ABCDEFGHIJKMNPQRSTUVWXYZabcdefghjkmnpqrstuv0123456789@*#-";
            Random random = new Random();
            char[] chars = new char[lenght];
            for(int i=0;i<lenght;i++)
            {
                chars[i] = validchars[random.Next(0, validchars.Length)];

            }
            return new string(chars);
        }

    }
}
