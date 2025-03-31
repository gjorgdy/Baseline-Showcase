using Core;
using Core.Interfaces;
using Core.Platforms;
using Core.Services;
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
    var env = Environment.GetEnvironmentVariables();
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

builder.Services.AddTransient<DiscordApiHandler>();
builder.Services.AddTransient<HttpClient>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();

// Check and apply pending migrations
var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    Console.WriteLine("Applying pending migrations...");
    dbContext.Database.Migrate();
    Console.WriteLine("Migrations applied successfully.");
}
else
{
    Console.WriteLine("No pending migrations found.");
}

app.Run();