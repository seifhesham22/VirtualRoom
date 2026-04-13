using Modules.Identity.Infrastructure.IdentityModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Infrastructure.TokenService
{
    public interface ITokenHandler
    {
        public string GenerateToken(User user);
    }
}
