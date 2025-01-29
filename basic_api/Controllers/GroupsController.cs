using basic_api.Database.Models;
using basic_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace basic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService) =>
            _groupsService = groupsService;

        [HttpGet]
        public async Task<List<Group>> Get() =>
            await _groupsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Group>> Get(string id)
        {
            var Group = await _groupsService.GetAsync(id);

            if (Group is null)
            {
                return NotFound();
            }

            return Group;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Group newGroup)
        {

            await _groupsService.CreateAsync(newGroup);

            return CreatedAtAction(nameof(Get), new { id = newGroup.Id }, newGroup);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Group updatedGroup)
        {
            var Group = await _groupsService.GetAsync(id);

            if (Group is null)
            {
                return NotFound();
            }
            updatedGroup.Id = Group.Id;

            await _groupsService.UpdateAsync(id, updatedGroup);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Group = await _groupsService.GetAsync(id);

            if (Group is null)
            {
                return NotFound();
            }

            await _groupsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
