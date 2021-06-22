using System.Threading.Tasks;

namespace Application.Infrastructure.Email
{
    public interface IEmailService
    {
        Task SendEmail(string emailAdress, string emailSubject, string emailBody);
    }
}
