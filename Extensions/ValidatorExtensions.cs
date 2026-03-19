using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Common;
using FluentValidation;

namespace devdev_api.Extensions
{
    public static class ValidatorExtensions
    {
        public static async Task<(bool IsValid, ApiResponse<T>? ErrorResponse)> ValidateRequestAsync<T>(
            this IValidator<T> validator, T dto, CancellationToken ct = default)
        {
            var result = await validator.ValidateAsync(dto, ct);
            if (result.IsValid)
                return (true, null);

            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            return (false, ApiResponse<T>.Fail("Validation failed.", errors));
        }
    }
}