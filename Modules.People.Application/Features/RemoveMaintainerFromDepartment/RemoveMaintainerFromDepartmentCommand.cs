using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.RemoveMaintainerFromDepartment
{
    public sealed record RemoveMaintainerFromDepartmentCommand(Guid MaintainerId) : IRequest;

    public sealed record RemoveMaintainerFromDepartmentCommandHandler(
        IPeopleDbContext _people,
        CurrentUser _currentUser)
        : IRequestHandler<RemoveMaintainerFromDepartmentCommand>
    {
        public async Task Handle(RemoveMaintainerFromDepartmentCommand request, CancellationToken cancellationToken)
        {
            var deptManager = await _people
                .departmentManagers
                .FirstOrDefaultAsync(x => x.Id == _currentUser.ProfileId, cancellationToken)
                ?? throw new InvalidOperationException("departmentManager profile not found");

            var maintainer = await _people
                .maintainers
                .FirstOrDefaultAsync(x => x.Id == request.MaintainerId, cancellationToken)
                ?? throw new InvalidOperationException($"couldn't find maintainer with Id {request.MaintainerId}");

            if (!maintainer.isAssigned)
                throw new InvalidOperationException("can't remove maintainer: he is not assigned yet");

            if (maintainer.DepartmentId != deptManager.DepartmentId)
                throw new UnauthorizedAccessException("this maintainer is not in your department");

            maintainer.RemoveFromDepartment(deptManager.DepartmentId);
            await _people.SaveChangesAsync(cancellationToken);
        }
    }
}