using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Middleware;

namespace devdev_api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseAppMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // ✅ ก่อน
            app.UseAuthorization(); // ✅ หลัง

            app.MapControllers();

            return app;
        }
    }
}