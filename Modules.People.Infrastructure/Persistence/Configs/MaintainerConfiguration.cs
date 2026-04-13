using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Infrastructure.Persistence.Configs
{
    public class MaintainerConfiguration : IEntityTypeConfiguration<Maintainer>
    {
        public void Configure(EntityTypeBuilder<Maintainer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasIndex(x => x.DepartmentId)
                .IsUnique();

            builder.Property(x => x.DepartmentId)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(60);
        }
    }
}