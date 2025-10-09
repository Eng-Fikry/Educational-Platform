using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Presistance
{
    public class PlatformDbContext(DbContextOptions<PlatformDbContext> option) :DbContext(option)
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemplyRefrence).Assembly);
        }
    }
}
