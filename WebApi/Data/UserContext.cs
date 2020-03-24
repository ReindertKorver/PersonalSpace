using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Document>().HasOne(d => d.User).WithMany(u => u.Documents);
            modelBuilder.Entity<PSTask>().HasOne(d => d.User).WithMany(u => u.Tasks);
        }

        public DbSet<WebApi.Entities.User> User { get; set; }
        public DbSet<WebApi.Entities.Document> Document { get; set; }
        public DbSet<WebApi.Entities.PSTask> PSTask { get; set; }
    }
}