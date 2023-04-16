using System.ComponentModel.DataAnnotations;

namespace AppoinmentScheduler.Models
{
    public class RootUsers
    {
        [Key]
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? OrganizationName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        public string IamSlug { get; set; }

        public string Token { get; set; }

    }
}
