
namespace Presentation.API;

using System.Text.Json.Serialization;
using System.Text.Json;

using Application.Commands.AddVehicle;

using Domain.Factories;
using Domain.Interfaces;

using Infrastructure;
using Domain.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<IVehicleFactory, VehicleFactory>();
        builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
        builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();
        builder.Services.AddSingleton<IAuctionService, AuctionService>();
        builder.Services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssemblyContaining<AddVehicleCommand>();
        });

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
