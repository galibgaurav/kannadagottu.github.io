using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HelperComponent
{
    public class OneWayHash
    {

        private static readonly SHA512Managed shaHelper = new SHA512Managed();

        public static string Create(string text)
        {
            byte[] byteToHash = Encoding.Unicode.GetBytes(text);
            byte[] hashedByte = shaHelper.ComputeHash(byteToHash);
            return Encoding.Unicode.GetString(hashedByte);
        }
    }
}
