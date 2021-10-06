﻿using System.ComponentModel.DataAnnotations;


namespace MovieLand.Web.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(4)]
        public string Subject { get; set; }
        [Required, MinLength(10)]
        public string Message { get; set; }
    }
}
