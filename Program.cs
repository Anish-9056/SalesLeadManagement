
using Microsoft.EntityFrameworkCore;
using SalesLeadManagement.Data;
using SalesLeadManagement.Middleware;
using SalesLeadManagement.Services;
using SalesLeadManagement.Services.Interface;

namespace SalesLeadManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            });
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<ILeadService, LeadService>();

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

            app.UseMiddleware<GlobalErrorMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
