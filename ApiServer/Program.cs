using ApiServer.Controllers;
using Core;
using Core.Authentication;
using Core.Interfaces;
using Core.Platforms;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PostgreSQL;
using PostgreSQL.Access;
using PostgreSQL.Implementations;

_ = new Baseline();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

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

builder.Services.AddAuthentication("JwtCookieScheme")
    .AddScheme<AuthenticationSchemeOptions, JwtTokenAuthenticationHandler>("JwtCookieScheme", null);

builder.Services.AddSignalR();

builder.Services.AddMemoryCache();
builder.Services.AddTransient<DiscordApiHandler>();
builder.Services.AddTransient<HttpClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(corsPolicyBuilder => 
    corsPolicyBuilder.WithOrigins("https://localhost:44350")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);

app.MapControllers();
app.MapHub<ProfileHub>("/updates");

app.Run();