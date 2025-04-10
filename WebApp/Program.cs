using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AlphaDB")));
builder.Services.AddControllersWithViews();
/*builder.Services.AddScoped<IAAuthService, AuthService>();
builder.Services.AddScoped<IMemberService, MemberService>();*/
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/auth/Login";
    options.AccessDeniedPath = "/auth/AccessDenied";
    options.Cookie.HttpOnly = true;
    /*options.Cookie.SecurePolicy = CookieSecurePolicy.Always;*/
    options.Cookie.IsEssential = true;
    options.Cookie.Expiration = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
     
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();



var app = builder.Build();
app.UseHsts();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

/*
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "/Admin/overview"));
*/

app.MapControllerRoute(
        name: "default",
      
        pattern: "{controller=Admin}/{action=Index}/{id?}")
     
     
        /*
        pattern: "{controller=Overview}/{action=Index}/{id?}")
        */
 

    .WithStaticAssets();

app.Run();