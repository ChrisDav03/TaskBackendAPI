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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly TaskDb _dbContext;
        public usersController(TaskDb dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _dbContext.Users
                                .Include(u => u.Tasks)
                                .ToListAsync();
            var userDTOs = list.Select(user => new UserDTO
            {
                Id = user.id,
                Name = user.name,
                Email = user.email,
                Role = user.role,
                Tasks = user.Tasks.Select(task => new TaskDTO
                {
                    Title = task.title,
                    Description = task.description,
                    Status = task.status,
                    UserAssignedId = task.userAssignedId

                }).ToList()
            }).ToList();
            return StatusCode(StatusCodes.Status200OK, new { value = userDTOs});
        }
    }
    
        
    }

