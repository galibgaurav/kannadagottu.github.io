using System;
using System.Collections.Generic;
using System.Text;

namespace BhashaguruModel
{
    public class ChangeCredential
    {
        public string loginId { get; set; }
        public string newPassword { get; set; }
        public string oldpassword { get; set; }
    }

    public class ForgetCredential
    {
        public string EmailID { get; set; }
    }
}
