
using System.ComponentModel.DataAnnotations;

namespace Application.Actions.UserActions
{
    public class CreateUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string PhotoUrl { get; set; }
    }
}
