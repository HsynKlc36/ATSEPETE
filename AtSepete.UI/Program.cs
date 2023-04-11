using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using AtSepete.DataAccess.Extensions;
using AtSepete.Business.Mapper.Profiles;
using System.Reflection;
using AtSepete.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
 
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
