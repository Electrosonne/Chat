// ------------------------------------------------------------
// <copyright file="RegistrationUserCommandValidator.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using FluentValidation;
using FluentValidation.Validators;

namespace Chat.Application.Commands
{
    /// <summary>
    /// UserVm Validator.
    /// </summary>
    public class RegistrationUserCommandValidator : AbstractValidator<RegistrationUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationUserCommandValidator"/> class.
        /// </summary>
        public RegistrationUserCommandValidator()
        {
            this.RuleFor(userVm => userVm.UserVm.Nickname).MinimumLength(3).WithMessage($"Nickname must be longer.");
            this.RuleFor(userVm => userVm.UserVm.Nickname).MaximumLength(20).WithMessage($"Nickname must be less.");
            this.RuleFor(userVm => userVm.UserVm.Password).MinimumLength(5).WithMessage($"Password must be longer.");
            this.RuleFor(userVm => userVm.UserVm.Password).SetValidator(new RegularExpressionValidator<RegistrationUserCommand>("[A-Z]")).WithMessage($"Password must contain upper case.");
            this.RuleFor(userVm => userVm.UserVm.Password).SetValidator(new RegularExpressionValidator<RegistrationUserCommand>("[a-z]")).WithMessage($"Password must contain lower case.");
            this.RuleFor(userVm => userVm.UserVm.Password).SetValidator(new RegularExpressionValidator<RegistrationUserCommand>("[0-9]")).WithMessage($"Password must contain digits.");
        }
    }
}
