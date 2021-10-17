using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartsTraderApi.Models
{
    public class PartsDbContext:DbContext
    {

        public PartsDbContext(DbContextOptions<PartsDbContext> options) : base(options)
        {
        }

        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>().ToTable("Parts");
        }

    }
}
