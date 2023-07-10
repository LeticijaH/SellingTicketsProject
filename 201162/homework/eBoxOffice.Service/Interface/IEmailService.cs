using eBoxOffice.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eBoxOffice.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<EmailMessage> allMails);
    }
}
