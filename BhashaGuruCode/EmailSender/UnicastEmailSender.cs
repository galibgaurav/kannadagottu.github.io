using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    public class UnicastEmailSender : IUnicastEmailSender
    {
        IConfiguration _iconfiguration;
        public UnicastEmailSender(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public async Task<Response> SendUnicastMail(string fromEmail, string toEmail, string subject, string plainContent, string htmlContent)
        {

            try
            {
                var apiKey = _iconfiguration.GetSection("Data").GetSection("SendGridConnectionString").Value;
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(fromEmail),
                    Subject = subject,
                    PlainTextContent = plainContent,
                    HtmlContent = htmlContent
                };
                msg.AddTo(new EmailAddress(toEmail));
                Response response = await client.SendEmailAsync(msg);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
