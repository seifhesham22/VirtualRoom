using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Infrastructure.Persistence.Configs
{
    public class AssetManagerConfiguration : IEntityTypeConfiguration<AssetManager>
    {
        public void Configure(EntityTypeBuilder<AssetManager> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasIndex(x => x.FacultyId)
                .IsUnique();

            builder.Property(x => x.FacultyId)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(60);
        }
    }
}