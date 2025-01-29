using basic_api.Controllers;
using basic_api.Database.Models;
using basic_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace basic_api.Tests
{
    public class GroupsControllerTest
    {
        private readonly Mock<IGroupsService> _mockGroupsService; 
        private readonly GroupsController _controller;
        
        public GroupsControllerTest()
        {
            _mockGroupsService = new Mock<IGroupsService>();
            _controller = new GroupsController(_mockGroupsService.Object);
        }

        [Fact]
        public async Task TestGet()
        {
            var groups = new List<Group>
            {
               new Group { Id = "1", Name = "Test" },
               new Group { Id = "2", Name = "Test2" }
            };
            
            _mockGroupsService.Setup(service => service.GetAsync()).ReturnsAsync(groups);
            
            var result = await _controller.Get();

            Assert.NotNull(result);
            
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task TestGetById()
        {
            var group = new Group { Id = "1", Name = "Test" };
            
            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync(group);
            
            var result = await _controller.Get("1");

            var actionResult = Assert.IsType<ActionResult<Group>>(result);
            
            var returnValue = Assert.IsType<Group>(actionResult.Value);
            
            Assert.Equal("Test", returnValue.Name);
        }

        [Fact]
        public async Task TestGetById_NotFound()
        {
            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync((Group?)null);

            var result = await _controller.Get("1");
            
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task TestPost()
        {
            var group = new Group { Id = "1", Name = "Test" };
            
            _mockGroupsService.Setup(service => service.CreateAsync(group));

            var result = await _controller.Post(group);
            
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task TestUpdate()
        {
            var group = new Group { Id = "1", Name = "Test" };
            
            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync(group);
            
            _mockGroupsService.Setup(service => service.UpdateAsync("1", group));
            
            var result = await _controller.Update("1", group);
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task TestDelete()
        {
            var group = new Group { Id = "1", Name = "Test" };
            
            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync(group);
            
            _mockGroupsService.Setup(service => service.RemoveAsync("1"));
            
            var result = await _controller.Delete("1");
            
            Assert.IsType<NoContentResult>(result);
        }
    }
}
