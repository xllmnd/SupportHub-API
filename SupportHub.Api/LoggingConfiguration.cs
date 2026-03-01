using Serilog;

namespace SupportHub.Api
{
    public static class LoggingConfiguration
    {
        public static void SetLoggingConfiuration(this WebApplicationBuilder builder) {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() 
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true
                })
            .CreateLogger();
           /*
            for getting setting from appsettings
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
           */
            builder.Host.UseSerilog(); 
        }
    }
}
