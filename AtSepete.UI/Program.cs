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
using System.Globalization;
using MassTransit;
using AtSepete.UI.AdminConsumers;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services
    .AddCookieMVCServices(builder.Configuration)
    .AddMvcServices();
builder.Services.AddStackExchangeRedisCache(Options => Options.Configuration = "localhost:1453");//docker da ayaða kaldýrdýðýmýz portu yazarýz (REDÝS CACHE)

// MassTransit configuration
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<AdminMessageConsumer>(); // Consumer tanýmladýk.
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host("amqps://gikvqzuf:eKjmPiSgqFLMMfm0w8uwySxpH614wgTz@moose.rmq.cloudamqp.com/gikvqzuf");
        _configurator.ReceiveEndpoint("createOrders", e => e.ConfigureConsumer<AdminMessageConsumer>(context)); // Consumer'ýn hangi queue dinleyeceðini burada belirtmemiz gerekiyor.
    });
});
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
var cultures = new List<CultureInfo>
{
    new CultureInfo("tr")
};

app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("tr");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseNToastNotify();
app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
//defaults: new { area = "Admin" }
);

app.MapControllerRoute(
    name: "customer",
    pattern: "{area:exists}/{controller=Customer}/{action=Index}/{id?}"
//defaults: new { area = "Customer" }
);

app.MapControllerRoute(
    name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();



app.Run();








