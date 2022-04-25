using MovieLand.Application.DTOs;


namespace MovieLand.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(ContactDTO contact);
    }
}
