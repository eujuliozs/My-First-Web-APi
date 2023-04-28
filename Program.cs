using FirstWebApi.Data;
using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace FirstWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<catalogoapidataContext>(options => options.UseMySql("server=localhost;port=3306;user=root;password=1234;database=catalogoapidata", ServerVersion.Parse("8.0.30-mysql")));

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions
                (
                    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );

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