using basic_api.Controllers;
using basic_api.Database.Models;
using basic_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace basic_api.Tests
{
    public class UsersControllerTest
    {
        private readonly Mock<IUsersService> _mockUsersService;
        private readonly Mock<IGroupsService> _mockGroupsService;
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            _mockUsersService = new Mock<IUsersService>();
            _mockGroupsService = new Mock<IGroupsService>();
            _controller = new UsersController(_mockUsersService.Object, _mockGroupsService.Object);
        }

        [Fact]
        public async Task TestGet()
        {
            var users = new List<User>
            {
                new User { Id = "1", Name = "John", GroupId = "1" },
                new User { Id = "2", Name = "Josh", GroupId = "2" }
            };
            
            _mockUsersService.Setup(service => service.GetAsync()).ReturnsAsync(users);

            var result = await _controller.Get();

            Assert.NotNull(result);
            
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task TestGetById()
        {
            var user = new User { Id = "1", Name = "John", GroupId = "1" };
           
            _mockUsersService.Setup(service => service.GetAsync("1")).ReturnsAsync(user);

            var result = await _controller.Get("1");

            var actionResult = Assert.IsType<ActionResult<User>>(result);
            
            var returnValue = Assert.IsType<User>(actionResult.Value);
            
            Assert.Equal("John", returnValue.Name);
        }

        [Fact]
        public async Task TestGetById_UserNotFound()
        {
            _mockUsersService.Setup(service => service.GetAsync("1")).ReturnsAsync((User?)null);

            var result = await _controller.Get("1");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task TestPost()
        {
            var user = new User { Id = "1", Name = "John", GroupId = "1" };

            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync(new Group { Id = "1", Name = "Test" });

            _mockUsersService.Setup(service => service.CreateAsync(user));

            var result = await _controller.Post(user);
            
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task TestPost_GroupNotFound()
        {
            var user = new User { Id = "1", Name = "John", GroupId = "1" };
            
            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync((Group?)null);
            
            var result = await _controller.Post(user);
            
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task TestUpdate()
        {
            var user = new User { Id = "1", Name = "John", GroupId = "1" };
            
            _mockUsersService.Setup(service => service.GetAsync("1")).ReturnsAsync(user);
            
            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync(new Group { Id = "1", Name = "Test" });
            
            _mockUsersService.Setup(service => service.UpdateAsync("1", user));
            
            var result = await _controller.Update("1", user);
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task TestUpdate_UserNotFound()
        {
            var user = new User { Id = "1", Name = "John", GroupId = "1" };
            
            _mockUsersService.Setup(service => service.GetAsync("1")).ReturnsAsync((User?)null);
            
            var result = await _controller.Update("1", user);
            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestUpdate_GroupNotFound()
        {
            var user = new User { Id = "1", Name = "John", GroupId = "1" };
            
            _mockUsersService.Setup(service => service.GetAsync("1")).ReturnsAsync(user);
            
            _mockGroupsService.Setup(service => service.GetAsync("1")).ReturnsAsync((Group?)null);
            
            var result = await _controller.Update("1", user);
            
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task TestDelete()
        {
            var user = new User { Id = "1", Name = "John", GroupId = "1" };
            
            _mockUsersService.Setup(service => service.GetAsync("1")).ReturnsAsync(user);
            
            _mockUsersService.Setup(service => service.RemoveAsync("1"));
            
            var result = await _controller.Delete("1");
            
            Assert.IsType<NoContentResult>(result);
        }
    }
}
