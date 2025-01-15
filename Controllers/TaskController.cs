using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBackendAPI.Custom;
using TaskBackendAPI.Data;
using TaskBackendAPI.Models;
using TaskBackendAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System;

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
            var list = await _dbContext.Tasks
                .Include(t => t.userAssigned)
                .ToListAsync();
            var taskDTOs = list.Select(task => new TaskDTO
            {
                Id = task.id,
                Title = task.title,
                Description = task.description,
                Status = task.status,
                UserAssigned = new UserDTO  // Assuming one user is assigned to the task
                {
                    Id = task.userAssigned.id,
                    Name = task.userAssigned.name,
                }
            }).ToList();


            return StatusCode(StatusCodes.Status200OK, new {value = taskDTOs});
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(TaskDTO obj)
        {
            var modelTask = new Models.Task
            {
                title = obj.Title,
                description = obj.Description,
                status = obj.Status,
                userAssignedId = obj.UserAssignedId,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };
            await _dbContext.Tasks.AddAsync(modelTask);
            await _dbContext.SaveChangesAsync();

            if (modelTask.id != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            if (task == null)
                return NotFound(new { message = "Task not found" });

            return Ok(task);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, TaskDTO obj)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            if (task == null)
                return NotFound(new { message = "Task not found" });

            task.title = obj.Title;
            task.description = obj.Description;
            task.status = obj.Status;
            task.userAssignedId = obj.UserAssignedId;
            task.updatedAt = DateTime.UtcNow;

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            if (task == null)
                return NotFound(new { message = "Task not found" });

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }
    }
}
    