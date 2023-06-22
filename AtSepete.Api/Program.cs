using AtSepete.Api.Extensions;
using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.Extensions;
using AtSepete.Business.Logger;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.DataAccess.Extensions;
using AtSepete.DataAccess.SeedData;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;

using AtSepete.Repositories.Extensions;
using AutoMapper;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddRepositoriesServices()
    .AddJWTBusinessServices(builder.Configuration, new HttpContextAccessor())
    .AddDataAccessServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

//masstransit ile publisher ayarý(rabbitmq)

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices(services =>
{
    services.AddMassTransit(configurator =>
    {
        configurator.UsingRabbitMq((context,
           _configurator) =>
        {
            _configurator.Host("amqps://gikvqzuf:eKjmPiSgqFLMMfm0w8uwySxpH614wgTz@moose.rmq.cloudamqp.com/gikvqzuf");
        
        });
    });

    services.AddScoped<ISendOrderMessageService,SendOrderMessageService>();
    services.AddSingleton<ISendEndpointProvider>(provider =>
    {
        using (IServiceScope scope = provider.CreateScope())
        {
            // ISendEndpointProvider'ý çözün
            ISendEndpointProvider sendEndPointProvider = scope.ServiceProvider.GetService<ISendEndpointProvider>();
            return sendEndPointProvider;
        }
    });
    services.AddHostedService<SendOrderMessageService>(provider =>
    {
        using IServiceScope scope = provider.CreateScope();
        ISendEndpointProvider sendEndPointProvider = scope.ServiceProvider.GetService<ISendEndpointProvider>();
        return new SendOrderMessageService(sendEndPointProvider);//bu parametre ile ayaða kalkar
    });//publishMessageService  burada bildirdik
})
.Build();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AtSepeteApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "Token",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with Token",
        Type = SecuritySchemeType.Http
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AtSepeteApi v1");
    });

}

//app.UseMiddleware<Mid>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await AdminSeedData.SeedAsync(app.Configuration);
app.Run();
host.Run();
