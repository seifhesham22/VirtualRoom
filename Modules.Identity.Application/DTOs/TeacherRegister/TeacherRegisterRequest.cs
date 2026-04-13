using System.ComponentModel.DataAnnotations;

namespace Modules.Identity.Application.DTOs.TeacherRegister
{
    public sealed record TeacherRegisterRequest(
        [EmailAddress]
        [Required]
        string Email,
        [Required]
        string Password,
        [Required]
        [MaxLength(20)]
        string FullName);
}