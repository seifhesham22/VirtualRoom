using Microsoft.EntityFrameworkCore;
using Modules.Identity.Application.Abstractions;
using Modules.People.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Modules.People.Infrastructure.IdentityFacades
{
    public class TeacherFacade(PeopleDbContext _db) : ITeacherFacade
    {
        public async Task<Guid> CreateTeacherProfileAsync(string fullName, Guid userId)
        {
            var Teacher = new Teacher(userId, fullName);

            var res = await _db.teachers.AddAsync(Teacher);
            await _db.SaveChangesAsync();

            return Teacher.Id;
        }

        public async Task DeleteTeacherProfileAsync(Guid userId)
        {
            var teacher = await _db
                .teachers
                .FirstOrDefaultAsync(x => x.UserId == userId)
                ?? throw new InvalidOperationException($"couldn't find teacher with user Id {userId}");

            _db.teachers.Remove(teacher);
            await _db.SaveChangesAsync();
        }
    }
}