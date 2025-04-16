
namespace Presentation.API;

using System.Text.Json.Serialization;
using System.Text.Json;

using Application.Commands.AddVehicle;

using Domain.Factories;
using Domain.Interfaces;

using Infrastructure;
using Domain.Services;
using MediatR;
using Presentation.API.PipelineBehavior;
using FluentValidation;
using Presentation.API.Handlers;

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
        builder.Services.AddSwaggerGen((options) => 
        {
            options.CustomSchemaIds(type => type.FullName);
        });

        builder.Services.AddSingleton<IVehicleFactory, VehicleFactory>();
        builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
        builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();
        builder.Services.AddSingleton<IAuctionService, AuctionService>();
        builder.Services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssemblyContaining<AddVehicleCommand>();
        });

        builder.Services.AddValidatorsFromAssembly(typeof(AddVehicleCommand).Assembly);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        builder.Services.AddExceptionHandler<BusinessRuleExceptionHandler>();
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseExceptionHandler("/error");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
