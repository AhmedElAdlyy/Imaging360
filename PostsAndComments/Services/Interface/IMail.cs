using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Services.Interface
{
    public interface IMail
    {
        Task SendEmilAsync(string toMail, string subject, string content);
    }
}
