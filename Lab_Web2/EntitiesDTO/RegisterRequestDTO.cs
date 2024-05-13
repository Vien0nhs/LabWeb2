using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
#nullable disable
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Lab_Web2.EntitiesDTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}