using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Infrastructure.Persistence.Configs
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.FullName)
                .HasMaxLength(30);

            builder.HasMany(x => x.TeacherFaculties)
                .WithOne()
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CanDesign)
                .IsRequired();

            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}