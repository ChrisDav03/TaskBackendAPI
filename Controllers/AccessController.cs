using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBackendAPI.Custom;
using TaskBackendAPI.Data;
using TaskBackendAPI.Models;
using TaskBackendAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace TaskBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly TaskDb _dbContext;
        private readonly Utilities _utils;
        public AccessController(TaskDb dbContext, Utilities utils)
        {
           _dbContext = dbContext;
           _utils = utils;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDTO obj)
        {
            var modelUser = new User
            {
                name = obj.Name,
                email = obj.Email,
                password = _utils.encriptSHA256(obj.Password)
            };
            await _dbContext.Users.AddAsync(modelUser);
            await _dbContext.SaveChangesAsync();
            
            if (modelUser.id != 0) 
                return StatusCode(StatusCodes.Status200OK,new {isSuccess = true});
            else
                return StatusCode(StatusCodes.Status200OK,new {isSuccess = false});
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO obj)
        {
            var userFound = await _dbContext.Users
                            .Where(u=>
                            u.email == obj.Email &&
                            u.password == _utils.encriptSHA256(obj.Password)
                            ).FirstOrDefaultAsync();
            if(userFound == null)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = false, token = ""});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = _utils.generateJWT(userFound) });

        }

    }
}
