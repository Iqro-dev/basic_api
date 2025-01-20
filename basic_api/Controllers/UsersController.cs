using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using basic_api.Database.Models;
using basic_api.Services;
using System.Security.Cryptography;

namespace basic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly GroupsService _groupsService;

        public UsersController(UsersService usersService, GroupsService groupsService)
        {
            _usersService = usersService;
            _groupsService = groupsService;
        }

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _usersService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var User = await _usersService.GetAsync(id);

            if (User is null)
            {
                return NotFound();
            }

            return User;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            if (newUser.GroupId != null)
            {
                var groupId = await _groupsService.GetAsync(newUser.GroupId);
                if (groupId is null)
                {
                    return NotFound("Group not found");

                }
            }
            await _usersService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            var User = await _usersService.GetAsync(id);

            if (User is null)
            {
                return NotFound();
            }

            if (updatedUser.GroupId != null)
            {
                var groupId = await _groupsService.GetAsync(updatedUser.GroupId);
                if (groupId is null)
                {
                    return NotFound("Group not found");

                }
            }

            updatedUser.Id = User.Id;

            await _usersService.UpdateAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var User = await _usersService.GetAsync(id);

            if (User is null)
            {
                return NotFound();
            }

            await _usersService.RemoveAsync(id);

            return NoContent();
        }
    }
}
