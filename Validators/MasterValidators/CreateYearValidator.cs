using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs.MasterDTOs;
using FluentValidation;

namespace devdev_api.Validators.MasterValidators
{
    public class CreateYearValidator : AbstractValidator<CreateYearDto>
    {
        public CreateYearValidator()
        {
            RuleFor(x => x.YearValue)
                .NotEmpty()
                .InclusiveBetween(1900, DateTimeOffset.UtcNow.Year + 5);
        }
    }
}