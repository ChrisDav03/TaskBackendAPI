using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBackendAPI.Custom;
using TaskBackendAPI.Data;
using TaskBackendAPI.Models;
using TaskBackendAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TaskBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDb _dbContext;
        public TaskController(TaskDb dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var list = await _dbContext.Tasks.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new {value = list});
        }
    }
}
