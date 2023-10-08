using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VirtualProject.BL.Interfaces;
using VirtualProject.BL.Repositories;
using VirtualProject.DAL;
using VirtualProject.Models;
using VirtualProject.Models.Identity.ApplicationsIdentity;
using static VirtualProject.DAL.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);

//Mail Setting
var mailSettings = builder.Configuration.GetSection("MailSetting");
builder.Services.Configure<MailSettingOutlookVM>(mailSettings);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI To Connection String \
var connectionstring = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContextPool<ApplicationDbContext>(opt => opt.UseSqlServer(connectionstring));

// DI To BL
builder.Services.AddScoped<IEmployeesRep,EmployeesRep>();
builder.Services.AddScoped<IMailingRep,MailingRep>();
builder.Services.AddScoped<IHrRep, HrRep>();
// DI To Identity
//Instance of Identity Usres and Roles

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
    options =>
    {
        // Default Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    }
    ).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    // allow to token in forget password action
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
