using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.GetTeachersByFaculty
{
    public sealed record GetTeacherByFacultyCommand : IRequest<List<TeacherSummary>>;

    public sealed record TeacherSummary(
        Guid Id,
        string name,
        bool canDesign,
        List<Guid> facultyIds);

    public sealed class GetTeacherByFacultyCommandHandler(
        CurrentUser _currentUser,
        IPeopleDbContext _people)
        : IRequestHandler<GetTeacherByFacultyCommand, List<TeacherSummary>>

    {
        public async Task<List<TeacherSummary>> Handle(GetTeacherByFacultyCommand request, CancellationToken cancellationToken)
        {
            var CurrentUserFacultyId = _currentUser.GetFacultyOrThrow();

            var teachers = await _people.
                teachers
                .Include(x => x.TeacherFaculties)
                .Where(x => x.TeacherFaculties.Any(x => x.FacultyId == CurrentUserFacultyId))
                .ToListAsync(cancellationToken);

            return teachers.Select(t => new TeacherSummary(
                t.Id,
                t.FullName,
                t.CanDesign,
                t.TeacherFaculties.Select(f => f.FacultyId).ToList()
                )).ToList();
        }
    }
}