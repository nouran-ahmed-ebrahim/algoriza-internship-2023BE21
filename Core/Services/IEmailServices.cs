using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
