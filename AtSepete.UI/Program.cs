using AtSepete.UI.Extensions;
using System.Globalization;
using MassTransit;
using AtSepete.UI.AdminConsumers;
using FormHelper;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCookieMVCServices(builder.Configuration)
    .AddMvcServices(builder.Configuration);

builder.Services.AddStackExchangeRedisCache(Options => Options.Configuration = builder.Configuration["RedisCache:RedisPort"]);//docker da ayaða kaldýrdýðýmýz portu yazarýz (REDÝS CACHE)

// MassTransit configuration
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<AdminMessageConsumer>(); // Consumer tanýmladýk.
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQHost:RabbitMQ"]);
        _configurator.ReceiveEndpoint("createOrders", e => e.ConfigureConsumer<AdminMessageConsumer>(context)); // Consumer'ýn hangi queue dinleyeceðini burada belirtmemiz gerekiyor.
    });
});
builder.Services.AddHttpClient();
var userToken = builder.Configuration["TokenMiddleware:Token"];
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








