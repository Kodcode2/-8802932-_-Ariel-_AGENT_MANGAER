
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using BE_AgentGuard.Data;

namespace BE_AgentGuard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BE_AgentGuardContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BE_AgentGuardContext") ?? throw new InvalidOperationException("Connection string 'BE_AgentGuardContext' not found.")));


            builder.Services.AddControllers();
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
