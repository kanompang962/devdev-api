using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace devdev_api.Controllers
{
    [Route("[controller]")]
    public class HealthController(AppDbContext db) : Controller
    {
        private readonly AppDbContext _db = db;

        [HttpGet]
        public async Task<IActionResult> ConnectionAsync()
        {
            try
            {
                await _db.Database.ExecuteSqlRawAsync("SELECT 1");

                return Ok(new
                {
                    status = "success",
                    message = "Database query success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}