using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.GetTeacherFaculties
{
    public sealed record GetTeacherFacultiesQuery : IRequest<List<FacultyItem>>;

    public sealed record FacultyItem(Guid facultyId, string name);

    public sealed class GetTeacherFacultiesQueryHandler(IPeopleDbContext _people, CurrentUser _currentUser)
        : IRequestHandler<GetTeacherFacultiesQuery, List<FacultyItem>>
    {
        public async Task<List<FacultyItem>> Handle(GetTeacherFacultiesQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _people
                .teachers
                .Include(x => x.TeacherFaculties)
                .FirstOrDefaultAsync(x => x.Id == _currentUser.ProfileId, cancellationToken)
                ?? throw new InvalidOperationException(
                    "couldn't find teacher profile");

            var facultyIds = teacher
                .TeacherFaculties
                .Select(x => x.FacultyId)
                .ToList();

            if (!facultyIds.Any())
            {
                return new List<FacultyItem>();
            }

            //Here we get the fauclty Names from the faculty service , we make contracts. maybe from faculty db context;
            //return await _facultyfacade.GetFacultyNames(facultyIds);

            return new List<FacultyItem>();
            // we remove the returning empty list as it is just written to satisfy the compiler.
        }
    }
}