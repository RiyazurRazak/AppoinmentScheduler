using AppoinmentScheduler.Models;
using Microsoft.EntityFrameworkCore;

namespace AppoinmentScheduler.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slots>()
                .Property(p => p.SlotId)
                .ValueGeneratedOnAdd();
        }
    }
}
