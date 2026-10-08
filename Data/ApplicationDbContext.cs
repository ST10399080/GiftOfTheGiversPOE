using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversPOE.Models;

namespace GiftOfTheGiversPOE.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReliefProject> ReliefProjects { get; set; }

        public DbSet<ProjectUpdate> ProjectUpdates { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Donation> Donations { get; set; }


        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Donation>()
                .Property(d => d.Amount)
                .HasPrecision(18, 2);
        }
    }
}