using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Shared.Authorization
{
    public class AppClaims
    {
        public const string ProfileId = "app:profileId";
        public const string FacultyId = "app:facultyId";
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string Role = ClaimTypes.Role;
        public const string Email = ClaimTypes.Email;
    }
}