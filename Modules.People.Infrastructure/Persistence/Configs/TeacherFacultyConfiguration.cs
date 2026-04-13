using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.People.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Infrastructure.Persistence.Configs
{
    public class TeacherFacultyConfiguration : IEntityTypeConfiguration<TeacherFaculty>
    {
        public void Configure(EntityTypeBuilder<TeacherFaculty> builder)
        {
            builder.ToTable("TeacherFaculties");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TeacherId).IsRequired();
            builder.Property(x => x.FacultyId).IsRequired();

            builder.HasIndex(x => new { x.TeacherId, x.FacultyId })
                .IsUnique();
        }
    }
}