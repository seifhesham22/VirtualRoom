using Microsoft.AspNetCore.Http;
using Shared.Enums;
using System.Security.Claims;
namespace Shared.Authorization
{
    public class CurrentUserFactory
    {
        private readonly IHttpContextAccessor _http;
        public CurrentUserFactory(IHttpContextAccessor http) => _http = http;
        public CurrentUser Create()
        {
            var user = _http.HttpContext?.User
            ?? throw new UnauthorizedAccessException("No HTTP context.");
            var userId = user.FindFirstValue(AppClaims.UserId)
            ?? throw new UnauthorizedAccessException("UserId claim missing.");
            var profileId = user.FindFirstValue(AppClaims.ProfileId)
            ?? throw new UnauthorizedAccessException("ProfileId claim missing.");
            var role = user.FindFirstValue(AppClaims.Role)
            ?? throw new UnauthorizedAccessException("Role claim missing.");
            var facultyId = user.FindFirstValue(AppClaims.FacultyId);
            return new CurrentUser
            {
                UserId = Guid.Parse(userId),
                ProfileId = Guid.Parse(profileId),
                Role = Enum.Parse<Role>(role),
                Email = user.FindFirstValue(AppClaims.Email) ?? string.Empty,
                FacultyId = facultyId is not null ? Guid.Parse(facultyId) : null
            };
        }
    }
}