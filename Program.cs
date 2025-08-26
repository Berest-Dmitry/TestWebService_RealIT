
using Microsoft.Extensions.Configuration;
using TestWebService_RealIT.Services;

namespace TestWebService_RealIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<MockService>();
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<PayInService>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddLogging(builder =>
                builder
                    .AddDebug()
                    .AddConsole()
                    .AddConfiguration(config.GetSection("Logging"))
                    .SetMinimumLevel(LogLevel.Information)
            );

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
