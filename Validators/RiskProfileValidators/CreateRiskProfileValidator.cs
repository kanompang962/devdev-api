using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs.RiskProfileDTOs;
using FluentValidation;

namespace devdev_api.Validators.RiskProfileValidators
{
    public class CreateRiskProfileValidator : AbstractValidator<CreateRiskProfileDto>
    {
        public CreateRiskProfileValidator()
        {
            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.FunctionalId)
                .GreaterThan(0).WithMessage("FunctionalId must be greater than 0.")
                .When(x => x.FunctionalId.HasValue);  // validate เฉพาะตอนมีค่า

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("DepartmentId must be greater than 0.")
                .When(x => x.DepartmentId.HasValue); // validate เฉพาะตอนมีค่า
        }
    }
}