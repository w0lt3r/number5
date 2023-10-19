using Microsoft.EntityFrameworkCore;
using Number5Poc.Data;
using Number5Poc.Services;
using Number5Poc.Services.Interfaces;
using Number5Poc.Services.Options;
using Number5Poc.Services.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.Configure<ElasticSearch>(builder.Configuration.GetSection("ElasticSearch"));
builder.Services.Configure<Kafka>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAnalyticsHandler, AnalyticsHandler>();
builder.Services.AddScoped<IMessagingSystem, MessagingSystem>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(PermissionProfile));
// builder.Services.AddDbContext<Context>(
//     options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddDbContext<Context>(
    options => options.UseInMemoryDatabase("name=InMemory"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
;

app.Run();