using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProfileService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Freelancer>().HasKey(k => new { k.Id });
            modelBuilder.Entity<Buyer>().HasKey(k => new { k.Id });
            modelBuilder.Entity<User>().HasKey(k => new { k.Id });
            modelBuilder.Entity<FreelancerSkill>().HasKey(k => new { k.Id });
            modelBuilder.Entity<Skill>().HasKey(k => new { k.Id });

            modelBuilder.Entity<Freelancer>()
                .HasMany(freelancer => freelancer.FreelancerSkills)
                .WithOne(freelancerSkill => freelancerSkill.Freelancer)
                .HasForeignKey(freelancerSkill => freelancerSkill.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
                .HasMany(skill => skill.FreelancerSkills)
                .WithOne(freelancerSkill => freelancerSkill.Skill)
                .HasForeignKey(freelancerSkill => freelancerSkill.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<FreelancerSkill> FreelancerSkills { get; set; }
    }
}

