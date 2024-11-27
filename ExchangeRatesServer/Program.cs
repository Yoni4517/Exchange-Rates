using ExchangeRatesServer.Services;

namespace ExchangeRatesServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseUrls($"http://{builder.Configuration["AppSettings:Host"]}:{builder.Configuration["AppSettings:Port"]}");

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        bool useMockService = builder.Configuration.GetValue<bool>("ExchangeRatesService:UseMockService");

        if (useMockService)
            builder.Services.AddScoped<IExchangeRatesService, MockExchangeRatesService>();
        else
            builder.Services.AddScoped<IExchangeRatesService, RealExchangeRatesService>();

        builder.Services.AddHttpClient<RealExchangeRatesService>();

        var corsAllowedOrigin = builder.Configuration["CorsSettings:CorsAllowedOrigin"];

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins(corsAllowedOrigin!)  
                      .AllowAnyMethod()              
                      .AllowAnyHeader();              
            });
        });



        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseCors("AllowSpecificOrigin");

        app.MapControllers();

        app.Run();
    }
}
