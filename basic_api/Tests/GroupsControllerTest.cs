using basic_api.Controllers;
using basic_api.Database.Models;
using basic_api.Services.Interfaces;
using Moq;
using Xunit;

namespace basic_api.Tests
{
    public class GroupsControllerTest
    {
        private readonly Mock<IGroupsService> _mockService; 
        private readonly GroupsController _controller;
        
        public GroupsControllerTest()
        {
            _mockService = new Mock<IGroupsService>();
            _controller = new GroupsController(_mockService.Object);
        }

        [Fact]
        public async Task TestGet()
        {
            var groups = new List<Group>
            {
               new Group { Id = "1", Name = "Test" },
               new Group { Id = "2", Name = "Test2" }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(groups);
            
            var result = await _controller.Get();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task TestGetById()
        {
            var group = new Group { Id = "1", Name = "Test" };
            _mockService.Setup(service => service.GetAsync("1")).ReturnsAsync(group);
            
            var result = await _controller.Get("1");

            Assert.NotNull(result);
            Assert.Equal("1", result.Value.Id);
            Assert.Equal("Test", result.Value.Name);
        }
    }
}
