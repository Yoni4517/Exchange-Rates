using ExchangeRatesServer.Services;

namespace ExchangeRatesServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IExchangeRatesService, MockExchangeRatesService>(); 

        builder.Services.AddHttpClient<MockExchangeRatesService>(); 

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins("http://localhost:3000")  
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
