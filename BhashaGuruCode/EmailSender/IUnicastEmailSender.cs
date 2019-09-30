using System.Threading.Tasks;
using SendGrid;

namespace EmailSender
{
    public interface IUnicastEmailSender
    {
        Task<Response> SendUnicastMail(string fromEmail, string toEmail, string subject, string plainContent, string htmlContent);
    }
}