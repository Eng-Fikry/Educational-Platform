using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Configrations
{
    public class TeacherConfigrations : IEntityTypeConfiguration<Teacher> 
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");

            // 📌 المفتاح الأساسي
            builder.HasKey(s => s.Id);

            // 📌 UserId (مفتاح منطقي للربط مع AspNetUsers)
            builder.Property(s => s.UserId)
                   .IsRequired()
                   .HasMaxLength(450); // نفس طول Id في AspNetUsers

            builder.HasIndex(s => s.UserId)
                   .HasDatabaseName("IX_Teachers_UserId"); // لتحسين الأداء في عمليات البحث


            builder.Property(s => s.UserName)
                   .IsRequired()
                   .HasMaxLength(100);


            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(150);


            builder.Property(s => s.PhoneNumber)
                   .HasMaxLength(20);


            //builder.Property(s => s.Photo)
            //       .HasMaxLength(255);


            builder.Property(s => s.Role)
                   .HasConversion<string>()
                   .IsRequired();


            builder.Property(s => s.JoinedDate)
                   .HasDefaultValueSql("GETDATE()");


        }
    }
    public class StudentConfigrations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            // 📌 المفتاح الأساسي
            builder.HasKey(s => s.Id);

            // 📌 UserId (مفتاح منطقي للربط مع AspNetUsers)
            builder.Property(s => s.UserId)
                   .IsRequired()
                   .HasMaxLength(450); // نفس طول Id في AspNetUsers

            builder.HasIndex(s => s.UserId)
                   .HasDatabaseName("IX_Students_UserId"); // لتحسين الأداء في عمليات البحث

            
            builder.Property(s => s.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

           
            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            
            builder.Property(s => s.PhoneNumber)
                   .HasMaxLength(20);

           
            //builder.Property(s => s.Photo)
            //       .HasMaxLength(255);

          
            builder.Property(s => s.Role)
                   .HasConversion<string>() 
                   .IsRequired();

            
            builder.Property(s => s.JoinedDate)
                   .HasDefaultValueSql("GETDATE()");

           
        }
    }
}
