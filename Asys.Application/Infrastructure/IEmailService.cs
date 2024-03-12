using Asys.Application.Models;

namespace Asys.Application.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}