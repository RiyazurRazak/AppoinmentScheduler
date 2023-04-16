using AppoinmentScheduler.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppoinmentScheduler.Dto
{
    public class OrganizationRegister
    {
        public string? OrganizationName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }

    public class OrganizationLogin
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class AddAppoinment 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartRange { get; set; }
        public DateTime EndRange { get; set; }
        public decimal Intreval { get; set; }
    }


}
