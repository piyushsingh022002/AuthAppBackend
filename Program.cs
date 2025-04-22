


using AuthApp.Data;
using AuthApp.Repositories;
using AuthApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.DisableConnectionPooling", false);
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.DisableLogging", true);
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.DisablePerformanceCounters", true);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000", // for local dev
            "https://piyushregislogin.vercel.app" // for deployed frontend
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();









var app = builder.Build();
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
// app.UseSwagger();
// app.UseSwaggerUI();


// app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    // .WithStaticAssets();

app.MapDefaultControllerRoute(); 
app.Run();
