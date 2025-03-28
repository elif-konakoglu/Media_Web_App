using Alpata.API.Business;
using Alpata.API.Business.Interfaces;
using Alpata.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlpataAPI", Version = "v1" });
});

builder.Services
    .AddScoped<IUserService, UserManager>()
    .AddScoped<IMeetingService, MeetingManager>()
    .AddScoped<ICryptographyHelper, CryptographyHelper>()
    .AddScoped<IFileHelper, FileHelper>()
    .AddScoped<IMailHelper,MailHelper>();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AlpataAPIDbContext>(options =>
        options.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AlpataAPIDbContext>();

    // Apply migrations
    dbContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlpataAPI V1");
    });
}

app.UseCors("AllowAllOrigins");


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
