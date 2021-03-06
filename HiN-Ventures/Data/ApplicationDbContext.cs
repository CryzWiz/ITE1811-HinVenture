﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HiN_Ventures.Models;

namespace HiN_Ventures.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<FreelancerInfo> FreelancerInfo { get; set; }
        public virtual DbSet<KlientInfo> KlientInfo { get; set; }
        public virtual DbSet<BitCoinAddress> BitCoinAddress { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Skill>().ToTable("Skill");
            builder.Entity<FreelancerInfo>().ToTable("Freelancer");
            builder.Entity<KlientInfo>().ToTable("Klient");
            builder.Entity<Project>().ToTable("Project");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
