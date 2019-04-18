using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS.Web.ViewModels.Profile
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
