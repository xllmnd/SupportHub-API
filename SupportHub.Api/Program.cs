using Serilog;
using SupportHub.Api.Middlewares;
using SupportHub.Application;
using SupportHub.Infrastructure.Persistence;

namespace SupportHub.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.SetLoggingConfiuration();
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseSerilogRequestLogging();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            app.Run();


        }
    }
}
