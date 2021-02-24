using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class UserDto
    {

       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress()]
        public string Email { get; set; }
       
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsAcceptEmails { get; set; }      
        [Required]
        public int CountryId { get; set; }

        public int UserId { get; set; }
        public DateTime DateRegistered { get; set; }
        public string CountryName { get; set; }

    }
}
