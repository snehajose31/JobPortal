using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using System.Collections.Generic;

namespace JobPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<JobCategory> JobCategories { get; set; }

        public DbSet<JobDetails> JobDetails { get; set; }


        public DbSet<Job> Jobs { get; set; }


        public DbSet<JobDtl> JobDtls { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure JobApplication primary key
            modelBuilder.Entity<JobApplication>()
                .HasKey(j => j.ApplicationId);

            // Other configurations can go here
        }
    }
}
