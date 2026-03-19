using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs.AuthDTOs;
using FluentValidation;

namespace devdev_api.Validators.AuthValidators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(200);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                // .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .MaximumLength(100);
        }
    }
}