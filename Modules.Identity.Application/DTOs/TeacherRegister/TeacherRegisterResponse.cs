using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Application.DTOs.TeacherRegister
{
    public sealed record TeacherRegisterResponse(string UserId, Guid ProfileId, Role Role);
}