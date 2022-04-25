using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieLand.Application.DTOs;
using MovieLand.Infrastructure.Interfaces;
using System;
using System.Net;
using System.Net.Mail;


namespace MovieLand.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmailService(IMapper mapper, ILogger<EmailService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public void SendEmail(ContactDTO contact)
        {
            var host = "smtp.mailtrap.io";
            var port = 2525;

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential("0fab9f4bc8a446", "cc52c1e25a21d4"),
                EnableSsl = true
            };

            client.Send(contact.Email, "movieland@gmail.com", contact.Subject, contact.Message);
        }
    }
}
