using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Modules.Identity.Infrastructure.IdentityModules
{
    public class User : IdentityUser
    {
        public Role Role {  get; private set; }
        public Guid ProfileId {  get; private set; }
        public Guid? FacultyId { get; private set; }
        public bool IsActive { get; private set; } = true;

        private User() { }

        public User(
            string email,
            Role role,
            Guid profileId,
            Guid? facultyId = null)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email) , $"Email can't be null or empty");
            Email = email;
            Role = role;
            ProfileId = profileId;
            FacultyId = facultyId;
        }

        public void UpdateProfile(Guid profileId, Guid facultyId)
        {
            ProfileId = profileId;
            FacultyId = facultyId;
        }

        public void Deactivate() => IsActive = false;
        public void ReActivate() => IsActive = true;
    }
}