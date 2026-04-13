using Shared.Enums;

namespace Shared.Authorization
{
    public class CurrentUser
    {
        public Guid UserId { get; init; }
        public Guid ProfileId { get; init; }
        public Role Role { get; init; }
        public Guid? FacultyId { get; init; }
        public string Email { get; init; } = string.Empty;
        public bool IsInRole(Role role) => Role == role;
        public Guid GetFacultyOrThrow() =>
        FacultyId ?? throw new UnauthorizedAccessException("This user has no faculty.");
    }
}