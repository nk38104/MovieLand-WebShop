﻿using MovieLand.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Domain.Entities
{
    public class Contact : Entity
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
