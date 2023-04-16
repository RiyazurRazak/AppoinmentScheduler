using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppoinmentScheduler.Models
{
    public class Slots
    {
        [Key]
        [Required]
        public string? Id { get; set; }

        [ForeignKey("Appoinments")]
        public string? AppoinmentId { get; set; }
        public virtual Appoinments Appoinment { get; set; }

        [DefaultValue(false)]
        public bool IsBooked { get; set; }

        public DateTime SlotTime { get; set; }

        [ForeignKey("Users")]
        public string? BookedBy { get; set; }
        public virtual Users SlotUserBy { get; set; }

        public int SlotId { get; set; }

    }
}
