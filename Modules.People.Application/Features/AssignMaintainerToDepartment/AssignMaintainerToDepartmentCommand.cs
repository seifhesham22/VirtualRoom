using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.AssignMaintainerToDepartment
{
    public sealed record AssignMaintainerToDepartmentCommand(Guid MaintainerId) : IRequest;

    public sealed class AssignMaintainerToDepartmentCommandHandler(
        IPeopleDbContext _people,
        CurrentUser _currentUser)
        : IRequestHandler<AssignMaintainerToDepartmentCommand>
    {
        public async Task Handle(AssignMaintainerToDepartmentCommand request, CancellationToken cancellationToken)
        {
            var deptManager = await _people
                .departmentManagers
                .FirstOrDefaultAsync(x => x.Id == _currentUser.ProfileId, cancellationToken)
                ?? throw new InvalidOperationException(
                    "Department manager profile not found");

            var maintainer = await _people
                .maintainers
                .FirstOrDefaultAsync(x => x.Id == request.MaintainerId, cancellationToken)
                ?? throw new InvalidOperationException(
                    $"Couldn't find maintatiner with Id {request.MaintainerId}");

            if (maintainer.isAssigned)
                throw new InvalidOperationException("The maintainer is alredy assigned to another department");
                
            maintainer.AssignToDepartment(deptManager.Id);
            await _people.SaveChangesAsync(cancellationToken);
        }
    }
}