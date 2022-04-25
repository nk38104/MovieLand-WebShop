using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Net;
using System.Net.Mail;


namespace MovieLand.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmailService(IMapper mapper, ILogger<EmailService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger= logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public void SendEmail(ContactViewModel contact)
        {
            var host = "smtp.mailtrap.io";
            var port = 2525;

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential("646b5458ce6a5d", "5e0c0c991b797a"),
                EnableSsl = true
            };

            client.Send(contact.Email, "movieland@gmail.com", contact.Subject, contact.Message);
        }
    }
}
