using AtSepete.Api.Extensions;
using AtSepete.Api.Middlewares;
using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.Extensions;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.DataAccess.Extensions;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;

using AtSepete.Repositories.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());//o an yürütülen mapper'ı kendisi tanır
builder.Services.AddControllers();
builder.Services.AddRepositoriesServices()
    .AddBusinessServices()
    .AddDataAccessServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{

//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateAudience = true,// hangi sitelerin veya kimlerin kullancağını belirleriz
//        ValidateIssuer = true,//oluşturulacak token değerini kimin dağıttığının belirlendiği yerdir
//        ValidateLifetime = true,//oluşturulan token değerinin süresini kontrol edecek olan doğrulama
//        ValidateIssuerSigningKey = true,//üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key  verisinin doğrulanmasıdır
//        ValidAudience = builder.Configuration["Token:Audience"],
//        ValidIssuer = builder.Configuration["Token:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
//    };

    
//});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AtSepeteApi", Version = "v1" });

    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Scheme = "bearer",
    //    BearerFormat = "Token",
    //    In = ParameterLocation.Header,
    //    Name = "Authorization",
    //    Description = "Bearer Authentication with Token",
    //    Type = SecuritySchemeType.Http
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Id = "Bearer",
    //                Type = ReferenceType.SecurityScheme
    //            }
    //        },
    //        new List<string>()
    //    }
    //});
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

app.Run();
