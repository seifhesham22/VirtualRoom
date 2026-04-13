using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.GrantTeacherDesignAccess
{
    public sealed record GrantTeacherDesignAccessCommand(Guid TeacherId) : IRequest;

    public sealed class GrantTeacherDesignAccessCommandHandler(
        IPeopleDbContext _people,
        CurrentUser _currentUser)
        : IRequestHandler<GrantTeacherDesignAccessCommand>
    {
        public async Task Handle(GrantTeacherDesignAccessCommand request, CancellationToken cancellationToken)
        {
            var currentUserFacultyId = _currentUser.GetFacultyOrThrow();

            var teacher = await _people
                .teachers
                .Include(x => x.TeacherFaculties)
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId,cancellationToken)
                ?? throw new InvalidOperationException($"couldn't find teacher with this id {request.TeacherId}");

            if (!teacher.BelongsToFaculty(currentUserFacultyId))
                throw new InvalidOperationException(
                    "this teacher doesn't belong to you faculty");

            teacher.GrantDesignAccess();
            await _people.SaveChangesAsync(cancellationToken);
        }
    }
}
