using AppoinmentScheduler.Models;
using Microsoft.EntityFrameworkCore;

namespace AppoinmentScheduler.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<RootUsers> RootUsers { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Appoinments> Appoinments { get; set; }

        public DbSet<Slots> Slots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slots>()
                .Property(p => p.SlotId)
                .ValueGeneratedOnAdd();
        }
    }
}
