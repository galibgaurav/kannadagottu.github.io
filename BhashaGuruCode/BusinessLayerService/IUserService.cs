using BhashaguruModel;
using System.Threading.Tasks;

namespace BusinessLayerService
{
    public interface IUserService
    {
      Task<string> AddUser(RegisterUser registerUser);
      Task<string> ChangePassword(string loginId, string newPassword, string oldpassword);
      Task<string> ForgetPassword(string loginid);
    }
}