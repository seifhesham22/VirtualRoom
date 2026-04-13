using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.GetMaintainersByDepartment
{
    public sealed record GetMaintainersByDepartmentCommand : IRequest<List<MaintainerSummary>>;

    public sealed record MaintainerSummary(Guid Id, string Name);

    public sealed class GetMaintainersByDepartmentCommandHandler(
        IPeopleDbContext _people,
        CurrentUser _currentUser)
        : IRequestHandler<GetMaintainersByDepartmentCommand, List<MaintainerSummary>>
    {
        public async Task<List<MaintainerSummary>> Handle(GetMaintainersByDepartmentCommand request, CancellationToken cancellationToken)
        {
            var deptmanager = await _people
                .departmentManagers
                .FirstOrDefaultAsync(x => x.Id == _currentUser.ProfileId, cancellationToken)
                ?? throw new InvalidOperationException("department manager profile not found");

            var maintainers = await _people
                .maintainers
                .Where(x => x.DepartmentId == deptmanager.DepartmentId)
                .ToListAsync(cancellationToken);

            return maintainers.Select(m => new MaintainerSummary(
                m.Id,
                m.FullName
                )).ToList();
        }
    }
}