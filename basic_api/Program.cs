
using basic_api.Database.Models;
using basic_api.Services;
using Microsoft.EntityFrameworkCore;

namespace basic_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<BasicApiDatabaseSettings>(
                builder.Configuration.GetSection("BasicApiDatabase"));

            builder.Services.AddSingleton<UsersService>();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<BasicApiContext>(opt =>
                opt.UseInMemoryDatabase("BasicApi"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
