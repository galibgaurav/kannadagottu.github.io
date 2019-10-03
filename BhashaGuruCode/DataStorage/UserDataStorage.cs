using BhashaguruModel;
using EmailSender;
using HelperComponent;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public class UserDataStorage : IUserDataStorage
    {
        IConfiguration _iconfiguration;
        IUnicastEmailSender _unicastEmailSender;

        public UserDataStorage(IConfiguration iconfiguration, IUnicastEmailSender unicastEmailSender)
        {
            _iconfiguration = iconfiguration;
            _unicastEmailSender = unicastEmailSender;
        }

        public async Task<string> AddUser(RegisterUser registerUser)
        {
            try
            {
                string tableName = "user";
                common cmn = new common(_iconfiguration);
                // Create or reference an existing table
                CloudTable table = await cmn.CreateTableAsync(tableName);

                var exitingUser = await DataStorageUtils.RetrieveEntityUsingPointQueryAsync(table, "BhashaGuru", registerUser.emailId);
                if (exitingUser != null)
                {
                    return ApplicationMessage.UserAlreadyExist;
                }
                else
                {
                    User user = new User(registerUser.emailId, "BhashaGuru");
                    user.firstName = registerUser.firstName;
                    user.lastName = registerUser.lastName;
                    user.emailId = registerUser.emailId;
                    user.createdDateTime = new DateTime();
                    user.createdDateTime = DateTime.UtcNow;
                    user.isActive = false;
                    user.isPasswordResetRequired = true;
                    user.loginId = registerUser.emailId;
                    user.modifiedDateTime = new DateTime();
                    user.modifiedDateTime = DateTime.UtcNow;
                    user.photoUrl = String.Empty;
                    user.userLanguage = "english";

                    var tempPassword= PasswordHelper.GetTempPassword(7);
                    user.password = OneWayHash.Create(tempPassword);
                    var newUser = await DataStorageUtils.InsertOrMergeEntityAsync(table, user);

                    var fromEmailAddress = _iconfiguration.GetSection("Data").GetSection("SendGridFromEmailAddress").Value;
                    var subject = "Bhashaguru: User registration and temporary password";
                    var plainContent = "Thanks for registering with Bhashagru, your password for first login is " + tempPassword + " Happy Learning :-) .";
                    await _unicastEmailSender.SendUnicastMail(fromEmailAddress,user.emailId,subject,plainContent,String.Empty);
                    return ApplicationMessage.UserRegisteredSuccessfully;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        public async  Task<string> ChangePassword(string loginId, string newPassword, string oldpassword)
        {
            try
            {
                common cmn = new common(_iconfiguration);
                // Create or reference an existing table
                CloudTable table = await cmn.CreateTableAsync("user");
                //check if older password exist
                var exitingUser = await DataStorageUtils.RetrieveEntityUsingPointQueryAsync(table, "BhashaGuru", loginId);
                var oldPasswordHash = OneWayHash.Create(oldpassword);
                if (oldPasswordHash.Equals(exitingUser.password))
                {
                    var newPasswordHash = OneWayHash.Create(newPassword);
                    //User user = new User(loginId, "BhashaGuru");
                    exitingUser.password = newPasswordHash;
                    exitingUser.isPasswordResetRequired = false;
                    exitingUser.isActive = true;
                    await DataStorageUtils.InsertOrMergeEntityAsync(table, exitingUser);
                    return ApplicationMessage.PasswordChangedSuccessfully;
                }
                else
                {
                    return ApplicationMessage.OldPasswordNotMatch;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<string> ForgetPassword(string loginid)
        {
            try
            {
                common cmn = new common(_iconfiguration);
                // Create or reference an existing table
                CloudTable table = await cmn.CreateTableAsync("user");
                //check if older password exist
                var exitingUser = await DataStorageUtils.RetrieveEntityUsingPointQueryAsync(table, "BhashaGuru", loginid);

                var tempPassword = PasswordHelper.GetTempPassword(7);
                exitingUser.password = OneWayHash.Create(tempPassword);
                exitingUser.isPasswordResetRequired = true;
                await DataStorageUtils.InsertOrMergeEntityAsync(table, exitingUser);

                var fromEmailAddress = _iconfiguration.GetSection("Data").GetSection("SendGridFromEmailAddress").Value;
                var subject = "Bhashaguru: Your new password for login";
                var plainContent = "Your password for login is " + tempPassword + " kindly change password after login. Happy Learning :-) .";
                await _unicastEmailSender.SendUnicastMail(fromEmailAddress, exitingUser.emailId, subject, plainContent, String.Empty);

                return ApplicationMessage.NewPasswordGenerated;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> ValidateUserLogin(string loginId, string password)
        {
            try
            {
                common cmn = new common(_iconfiguration);
                // Create or reference an existing table
                CloudTable table = await cmn.CreateTableAsync("user");
                //check if older password exist
                var exitingUser = await DataStorageUtils.RetrieveEntityUsingPointQueryAsync(table, "BhashaGuru", loginId);
                if(exitingUser==null)
                {
                    return null;
                }
                var passwordHash = OneWayHash.Create(password);
                bool isPasswordMatches = exitingUser.password.Equals(passwordHash);
                if(isPasswordMatches)
                {
                    return exitingUser;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
