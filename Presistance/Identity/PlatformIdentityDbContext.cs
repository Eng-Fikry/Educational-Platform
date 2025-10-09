using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Presistance.Identity
{
    public class PlatformIdentityDbContext(DbContextOptions<PlatformIdentityDbContext> options):IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users"); // دا للمستخدمين
            builder.Entity<IdentityRole>().ToTable("Rules"); // دا لل صلاحيات
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles"); // دا بيوضح صلاحيات كل مستخدم
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();

        }
    }
}
