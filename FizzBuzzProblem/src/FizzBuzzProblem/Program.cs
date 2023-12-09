using FizzBuzzProblem.Services.Implementations;
using FizzBuzzProblem.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace FizzBuzzProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IFizzBuzzSolver, FizzBuzzService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Adding Swagger so the consumer have a better idea of what the endpoint can return
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
