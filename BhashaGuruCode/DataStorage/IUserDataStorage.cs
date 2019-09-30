using System.Threading.Tasks;
using BhashaguruModel;

namespace DataStorage
{
    public interface IUserDataStorage
    {
        Task<string> AddUser(RegisterUser resgisterUser);
        Task<string> ChangePassword(string loginId, string newPassword, string oldpassword);
        Task<string> ForgetPassword(string loginid);
        Task<User>   ValidateUserLogin(string loginId, string password);
    }
}