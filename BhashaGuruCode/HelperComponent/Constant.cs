using System;
using System.Collections.Generic;
using System.Text;

namespace HelperComponent
{
    public sealed class ApplicationMessage
    {

        public static readonly String UserRegisteredSuccessfully = "User registered successfully";
        public static readonly String UserRegistrationFailed = "User registration failed";
        public static readonly String UserAlreadyExist = "User already exist";

        public static readonly String OldPasswordNotMatch = "Old password does not matches with the existing password";
        public static readonly String PasswordChangedSuccessfully = "Password changed successfully";

        public static readonly String NewPasswordGenerated = "New password is sent to your registered Email Address";
        public static readonly String LoginCreadentialNotCorrect = "Login creadentials are not correct";

        public static readonly String TopicAlreadyExist = "Topic already exist";


    }
}
