using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.RemoveTeacherFromFaculty
{
    public sealed record RemoveTeacherFromFacultyCommand(Guid TeacherId, Guid FacultyId)
        : IRequest;
    public class RemoveTeacherFromFacultyCommandHandler(CurrentUser _currentUser, IPeopleDbContext _people)
        : IRequestHandler<RemoveTeacherFromFacultyCommand>
    {
        public async Task Handle(RemoveTeacherFromFacultyCommand request, CancellationToken cancellationToken)
        {
            var CurrentUserFacultyId = _currentUser.GetFacultyOrThrow();

            if (request.FacultyId != CurrentUserFacultyId)
                throw new InvalidOperationException("You can't remove a teacher who is not in you faculty!");

            //____________________________________________________________________________________________
            //here we check if the faculty Exist or no.
            // var FacultyExists = _FacultyDbContext.FirstOrDefaultAsync(x => x.Id == request.FacultyId);
            //____________________________________________________________________________________________

            var teacher = await _people
                .teachers
                .Include(x => x.TeacherFaculties)
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId, cancellationToken) 
                ?? throw new ArgumentNullException($"Teacher with the Id {request.TeacherId} not found!");

            teacher.RemoveFromFaculty(request.FacultyId);
            await _people.SaveChangesAsync(cancellationToken);
        }
    }
}