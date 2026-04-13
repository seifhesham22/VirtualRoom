using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.People.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Infrastructure.Persistence.Configs
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentProfile>
    {
        public void Configure(EntityTypeBuilder<StudentProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("students");

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.FacultyId)
                .IsRequired();

            builder.HasIndex(x => x.FacultyId)
                .IsUnique();

            builder.Property(x => x.FullName)
                .HasMaxLength(60);
        }
    }
}