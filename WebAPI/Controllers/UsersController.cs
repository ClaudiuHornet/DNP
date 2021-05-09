using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IRepositoryUsers _repositoryUsers;

        public UsersController(IRepositoryUsers repositoryUsers)
        {
            _repositoryUsers = repositoryUsers;
        }

        [HttpGet]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                var user = await _repositoryUsers.ValidateUserAsync(username, password);
                Console.WriteLine(user.ToString());
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> ValidateRegister([FromBody] User user)
        {
            Console.WriteLine("Hello from user controller ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User userToAdd = await _repositoryUsers.RegisterUserAsync(user.UserName, user.Password, user.Password);
                Console.WriteLine(userToAdd);
                return Ok(userToAdd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}