using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppoinmentScheduler.Models
{
    public class Users
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? EmailAddress { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? ContactNumber { get; set; }

        [ForeignKey("RootUsers")]
        public Guid OrganizationId { get; set; }
        public virtual RootUsers? Organization { get; set; }



    }
}
