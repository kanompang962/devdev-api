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
            app.UseHttpsRedirection(); // ✅ บังคับ redirect HTTP → HTTPS ก่อน
            app.UseAuthentication(); // ✅ ก่อน
            app.UseAuthorization(); // ✅ หลัง

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            return app;
        }
    }
}