using Microsoft.Extensions.Configuration;
using PostsAndComments.Services.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Services.Implementation
{
    public class SendEmails : IMail
    {
        private IConfiguration _configuration;
        public SendEmails(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmilAsync(string toMail, string subject, string content)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("blog.imaging360@gmail.com", "Confirmation");
            var to = new EmailAddress(toMail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
