using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Application.Abstractions
{
    public interface ITeacherFacade
    {
        Task<Guid> CreateTeacherProfileAsync(string fullName,Guid userId);
        Task DeleteTeacherProfileAsync(Guid profileId);
    }
}