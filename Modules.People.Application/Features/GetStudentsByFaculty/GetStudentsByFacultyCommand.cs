using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.GetStudentsByFaculty
{
    public sealed record GetStudentsByFacultyCommand : IRequest<List<StudentSummary>>;

    public sealed record StudentSummary(
        Guid Id,
        string Name,
        Guid FacultyId);

    public sealed class GetStudentByFacultyCommandHandler(
        IPeopleDbContext _people,
        CurrentUser _currentUser)
        : IRequestHandler<GetStudentsByFacultyCommand, List<StudentSummary>>
    {
        public async Task<List<StudentSummary>> Handle(GetStudentsByFacultyCommand request, CancellationToken cancellationToken)
        {
            var UserFacultyId = _currentUser.GetFacultyOrThrow();

            var students = await _people
                .studentProfiles
                .Where(x => x.FacultyId == UserFacultyId)
                .ToListAsync(cancellationToken);

            return students.Select(s => new StudentSummary(
                s.Id,
                s.FullName,
                s.FacultyId
                ))
                .ToList();
        }
    }
}