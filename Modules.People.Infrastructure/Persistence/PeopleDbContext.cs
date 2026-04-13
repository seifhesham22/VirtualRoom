using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Modules.People.Domain.Entities;
using Modules.People.Infrastructure.Persistence.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Infrastructure.Persistence
{
    public class PeopleDbContext(DbContextOptions<PeopleDbContext> options) 
        : DbContext(options), IPeopleDbContext
    {
        public DbSet<Teacher> teachers => Set<Teacher>();

        public DbSet<TeacherFaculty> teacherFaculties => Set<TeacherFaculty>();

        public DbSet<StudentProfile> studentProfiles => Set<StudentProfile>();

        public DbSet<Maintainer> maintainers => Set<Maintainer>();

        public DbSet<AssetManager> assetManagers => Set<AssetManager>();

        public DbSet<DepartmentManager> departmentManagers => Set <DepartmentManager>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssetManagerConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherFacultyConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new MaintainerConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentManagerConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
