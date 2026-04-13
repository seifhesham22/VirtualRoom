using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Modules.People.Domain.Entities
{
    public sealed class StudentProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid FacultyId { get; private set; }
        public string FullName { get; private set; } = null!;

        private StudentProfile() { }

        public StudentProfile(
            Guid userId,
            Guid facultyId,
            string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentNullException(nameof(fullName),"full name can't be null or empty");

            Id = Guid.NewGuid();
            UserId = userId;
            FacultyId = facultyId;
            FullName = fullName;
        }

        public void TransferFaculty(Guid newFacultyId) => FacultyId = newFacultyId;
    }
}