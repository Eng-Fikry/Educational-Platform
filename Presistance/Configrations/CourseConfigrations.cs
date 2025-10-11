using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Configrations
{
    public class CourseConfigrations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasMany(L => L.Lessons)
                   .WithOne(C => C.Course)
                   .HasForeignKey(L=>L.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
