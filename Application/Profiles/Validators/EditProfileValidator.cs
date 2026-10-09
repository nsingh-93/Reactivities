using System.Security.Cryptography.X509Certificates;
using Application.Profiles.Commands;
using Application.Profiles.DTOs;
using FluentValidation;

namespace Application.Profiles.Validators;

public class EditProfileValidator : BaseProfileValidator<EditProfile.Command, EditProfileDto>
{
    public EditProfileValidator()
        : base(x => x.ProfileDto) { }
}
