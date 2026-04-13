using Microsoft.EntityFrameworkCore;
using Modules.People.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Interfaces
{
    public interface IPeopleDbContext
    {
        DbSet<Teacher> teachers { get; }
        DbSet<TeacherFaculty> teacherFaculties { get; }
        DbSet<StudentProfile> studentProfiles { get; }
        DbSet<Maintainer> maintainers { get; }
        DbSet<AssetManager> assetManagers { get; }
        DbSet<DepartmentManager> departmentManagers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}