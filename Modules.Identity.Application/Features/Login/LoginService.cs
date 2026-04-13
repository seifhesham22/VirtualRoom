using Microsoft.AspNetCore.Identity;
using Modules.Identity.Application.DTOs.Login;
using Modules.Identity.Infrastructure.IdentityModules;
using Modules.Identity.Infrastructure.Presestance;
using Modules.Identity.Infrastructure.TokenService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Application.Features.Login
{
    public class LoginService(UserManager<User> _userManager, ITokenHandler _token)
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.email) ?? 
                throw new ArgumentNullException($"couldn't find a user with the email {request.email}");

            if (!user.IsActive)
                throw new UnauthorizedAccessException($"The user account {request.email} is deactivated");

            var success = await _userManager.CheckPasswordAsync(user, request.password);
            if (!success)
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _token.GenerateToken(user);

            return new LoginResponse(
                token: token,
                expires: DateTime.UtcNow.AddHours(8),
                userId: user.Id,
                role: user.Role.ToString());
        }
    }
}