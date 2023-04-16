﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppoinmentScheduler.Models
{
    public class Appoinments
    {
        [Key]
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime? StartRange { get; set;}

        [Required]
        public DateTime? EndRange { get; set;}

        [Required]
        public decimal? Intreval { get; set; }

        [ForeignKey("RootUsers")]
        public string? OrganizationId { get; set; }
        public virtual RootUsers? CreatedOrganization { get; set; }

    }
}
