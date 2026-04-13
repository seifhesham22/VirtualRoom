using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.Identity.Infrastructure.IdentityModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Infrastructure.Presestance
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<User>()
                .Property(x => x.Role)
                .HasConversion<string>();
        }
    }
}