using Microsoft.AspNetCore.Identity;
using Modules.Identity.Application.Abstractions;
using Modules.Identity.Application.DTOs.TeacherRegister;
using Modules.Identity.Infrastructure.IdentityModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Application.Features.TeacherSelfRegisteration
{
    public class TeacherSelfRegistrationService(UserManager<User> _userManager, ITeacherFacade _teacher)
    {
        public async Task<TeacherRegisterResponse> RegisterTeacherAsync(TeacherRegisterRequest request)
        {
            var existing = await _userManager.FindByEmailAsync(request.Email);

            if (existing is not null)
                throw new InvalidOperationException("Email already taken");

            var user = new User(request.Email, Shared.Enums.Role.Teacher);

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException(errors);
            }

            Guid? profileId = null;
            try
            {
                profileId = await _teacher.CreateTeacherProfileAsync(
                    request.FullName,
                    Guid.Parse(user.Id));

                user.LinkProfile(profileId.Value);

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    var errors = string.Join(", ", updateResult.Errors.Select(x => x.Description));
                    throw new InvalidOperationException(errors);
                }
            }
            catch (Exception ex)
            {
                if (profileId.HasValue)
                {
                    await _teacher.DeleteTeacherProfileAsync(profileId.Value);
                }

                var deleteResult = await _userManager.DeleteAsync(user);
                if (!deleteResult.Succeeded)
                {
                    var deleteErrors = string.Join(", ", deleteResult.Errors.Select(x => x.Description));
                    throw new InvalidOperationException(
                        $"Couldn't create profile, and rollback user deletion failed: {deleteErrors}",
                        ex);
                }

                throw new InvalidOperationException("Couldn't create profile.", ex);
            }

            return new TeacherRegisterResponse(
                user.Id,
                user.ProfileId!.Value,
                user.Role
                );       
        }
    }
}