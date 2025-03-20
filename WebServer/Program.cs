using Core;
using Core.Authentication;
using Core.Interfaces;
using Core.Services;
using dotenv.net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PostgreSQL;
using PostgreSQL.Access;
using PostgreSQL.Implementations;

_ = new Baseline();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddDbContextPool<PostgresDbContext>(opt =>
{
    DotEnv.Load();
    var env = DotEnv.Read();
    opt.UseNpgsql($"""
                      Host={env["POSTGRES_URI"]};
                      Database={env["POSTGRES_DATABASE"]};
                      Username={env["POSTGRES_USERNAME"]};
                      Password={env["POSTGRES_PASSWORD"]}
                   """);
});
// user
builder.Services.AddScoped<IUserAccess, UserAccess>();
builder.Services.AddScoped<UserService>();
// tile
builder.Services.AddScoped<ITileAccess, TileAccess>();
builder.Services.AddScoped<TileService>();
// role
builder.Services.AddScoped<IRoleAccess, RoleAccess>();
builder.Services.AddScoped<RoleService>();
// connection
builder.Services.AddScoped<IConnectionAccess, ConnectionAccess>();
builder.Services.AddScoped<ConnectionService>();

builder.Services.AddAuthentication("JwtCookieScheme")
    .AddScheme<AuthenticationSchemeOptions, JwtTokenAuthenticationHandler>("JwtCookieScheme", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();