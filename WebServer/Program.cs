using Core;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PostgreSQL;

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