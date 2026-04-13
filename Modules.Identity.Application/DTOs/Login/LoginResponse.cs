using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Application.DTOs.Login
{
    public sealed record LoginResponse(
        string token,
        DateTime expires,
        string userId,
        string role);
}
