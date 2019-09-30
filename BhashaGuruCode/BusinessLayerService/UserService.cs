using BhashaguruModel;
using DataStorage;
using EmailSender;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerService
{
    public class UserService : IUserService
    {
        IConfiguration _iconfiguration;
        IUnicastEmailSender _unicastEmailSender;
        IUserDataStorage _userDataStorage;
        public UserService(IConfiguration iconfiguration, IUnicastEmailSender unicastEmailSender)
        {
            _iconfiguration = iconfiguration;
            _unicastEmailSender = unicastEmailSender;
        }
    

        public async Task<string> AddUser(RegisterUser registerUser)
        {
            UserDataStorage userDataStorage = new UserDataStorage(_iconfiguration, _unicastEmailSender);
            try
            {
                var result = await userDataStorage.AddUser(registerUser);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<string> ChangePassword(string loginId, string newPassword, string oldpassword)
        {
            UserDataStorage userDataStorage = new UserDataStorage(_iconfiguration, _unicastEmailSender);
            try
            {
                var result = await userDataStorage.ChangePassword(loginId,newPassword,oldpassword);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> ForgetPassword(string loginid)
        {
            UserDataStorage userDataStorage = new UserDataStorage(_iconfiguration, _unicastEmailSender);
            try
            {
                var result = await userDataStorage.ForgetPassword(loginid);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<User> validateUserLogin(string loginId,string password)
        {
            UserDataStorage userDataStorage = new UserDataStorage(_iconfiguration, _unicastEmailSender);

            try
            {
                var result = await userDataStorage.ValidateUserLogin(loginId, password);

                if(result==null)
                {
                    return result;
                }
                else
                {
                    TokenService tokenService = new TokenService();
                    string authtoken = tokenService.GetToken(/*pass role*/);
                    result.authToken = authtoken;
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
