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

builder.Services.AddStackExchangeRedisCache(Options => Options.Configuration = builder.Configuration["RedisCache:RedisPort"]);//docker da aya�a kald�rd���m�z portu yazar�z (RED�S CACHE)

// MassTransit configuration
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<AdminMessageConsumer>(); // Consumer tan�mlad�k.
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQHost:RabbitMQ"]);
        _configurator.ReceiveEndpoint("createOrders", e => e.ConfigureConsumer<AdminMessageConsumer>(context)); // Consumer'�n hangi queue dinleyece�ini burada belirtmemiz gerekiyor.
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
    app.UseDeveloperExceptionPage();//Bu metot, geli�tirme ortam�nda �al���rken herhangi bir hata meydana geldi�inde, kullan�c�ya ayr�nt�l� bir hata sayfas� g�sterir.
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








