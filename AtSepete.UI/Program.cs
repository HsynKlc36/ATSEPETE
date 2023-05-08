using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using AtSepete.DataAccess.Extensions;
using AtSepete.Business.Mapper.Profiles;
using System.Reflection;
using AtSepete.UI.Extensions;
using AtSepete.UI.MapperUI.Profiles;
using AtSepete.Business.Extensions;
using AtSepete.Repositories.Extensions;
using AspNetCoreHero.ToastNotification.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services
    .AddCookieMVCServices(builder.Configuration)
    .AddMvcServices()
    .AddGoogleMVCServices(builder.Configuration);



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();//Bu metot, geliþtirme ortamýnda çalýþýrken herhangi bir hata meydana geldiðinde, kullanýcýya ayrýntýlý bir hata sayfasý gösterir.
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseNotyf();
app.MapControllerRoute(
    name: "default",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();
