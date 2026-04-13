using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.AssignTeacherToFaculty
{

    public sealed record AssignTeacherToFacultyCommand(Guid TeacherId, Guid FacultyId)
        :IRequest;
    public class AssignTeacherToFacultyCommandHandler(CurrentUser _currentUser, IPeopleDbContext _people)
        : IRequestHandler<AssignTeacherToFacultyCommand>
    {
        public async Task Handle(AssignTeacherToFacultyCommand request, CancellationToken cancellationToken)
        {
            var AssetManagerFacultyId = _currentUser.GetFacultyOrThrow();

            if (request.FacultyId != AssetManagerFacultyId)
                throw new InvalidOperationException("You can only assign teachers to your own faculty");
            //
            //here we check wether the faculty exists or not.
            //var facultyExists = 0;

            var teacher = await _people
                .teachers
                .Include(x => x.TeacherFaculties)
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId, cancellationToken) ?? 
                throw new InvalidOperationException($"Couldn't find a teacher with this Id {request.TeacherId}");

            teacher.AssignToFaculty(request.FacultyId);
            await _people.SaveChangesAsync(cancellationToken);
        }
    }
}