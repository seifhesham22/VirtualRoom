using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.People.Application.Features.GetMaintainersByDepartment;
using Modules.People.Application.Interfaces;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.People.Application.Features.GetUnAssignedMaintainers
{
    public sealed record GetUnAssignedMaintainersCommand : IRequest<List<MaintainerSummary>>;

    public sealed class GetUnAssignedMaintainersCommandHandler(IPeopleDbContext _people)
        : IRequestHandler<GetUnAssignedMaintainersCommand, List<MaintainerSummary>>
    {
        public async Task<List<MaintainerSummary>> Handle(GetUnAssignedMaintainersCommand request, CancellationToken cancellationToken)
        {
            var maintainers = await _people
                .maintainers
                .Where(x => x.isAssigned == false)
                .ToListAsync(cancellationToken);

            return maintainers.Select(m => new MaintainerSummary(
                m.Id,
                m.FullName
                )).ToList();
        }
    }
}