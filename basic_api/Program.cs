using basic_api.Database.Models;
using basic_api.Services;
using basic_api.Services.Interfaces; // Make sure your interfaces are included
using Microsoft.EntityFrameworkCore;

namespace basic_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configure database settings
            builder.Services.Configure<BasicApiDatabaseSettings>(
                builder.Configuration.GetSection("BasicApiDatabase"));

            // Register services with interfaces, using Scoped for services tied to a request
            builder.Services.AddScoped<IUsersService, UsersService>(); // Register IUsersService to UsersService
            builder.Services.AddScoped<IGroupsService, GroupsService>(); // Register IGroupsService to GroupsService

            // Add controllers
            builder.Services.AddControllers();

            // Add DBContext with InMemoryDatabase configuration
            builder.Services.AddDbContext<BasicApiContext>(opt =>
                opt.UseInMemoryDatabase("BasicApi"));

            // Add Swagger for API documentation (optional but useful in development)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // Redirect to HTTPS
            app.UseAuthorization(); // Use Authorization middleware

            app.MapControllers(); // Map the controllers to the request pipeline

            app.Run(); // Run the application
        }
    }
}
