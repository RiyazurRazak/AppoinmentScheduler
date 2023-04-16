using System.ComponentModel.DataAnnotations;

namespace AppoinmentScheduler.Models
{
    public class RootUsers
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

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

        public Guid Token { get; set; }

    }
}
