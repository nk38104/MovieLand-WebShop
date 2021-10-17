using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(ContactViewModel contact);
    }
}
 