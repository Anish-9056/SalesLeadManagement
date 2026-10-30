using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SalesLeadManagement.Models;

namespace SalesLeadManagement.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Lead> leads { get; set; }
        public DbSet<LeadActivity> LeadActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.HasKey(lead => lead.Id);
                entity.Property(lead => lead.CustomerName).IsRequired().HasMaxLength(150);
                entity.Property(lead => lead.Email).HasMaxLength(250);
                entity.Property(lead => lead.PhoneNumber).IsRequired().HasMaxLength(30);
                entity.Property(lead => lead.Status).IsRequired().HasMaxLength(30);
                entity.Property(lead => lead.Notes).HasMaxLength(500);
                entity.HasMany(lead => lead.leadActivities).WithOne(activity=>activity.Lead).HasForeignKey(activity => activity.LeadId);
            });

            modelBuilder.Entity<LeadActivity>(entity =>
            {
                entity.HasKey(activity => activity.Id);
                entity.Property(activity => activity.ActivityType).IsRequired().HasMaxLength(30);
                entity.Property(activity => activity.Description).HasMaxLength(500);
            });
        }
    }
}
