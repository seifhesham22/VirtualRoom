using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Identity.Application.DTOs.Login
{
    public sealed record LoginRequest([Required] string email, [Required] string password);
}
