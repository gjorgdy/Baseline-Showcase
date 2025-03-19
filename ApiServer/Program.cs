using ApiServer;
using Core.Interfaces;
using Core.Services;
using dotenv.net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PostgreSQL;
using PostgreSQL.Access;
using PostgreSQL.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

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
    .AddScheme<AuthenticationSchemeOptions, MyAuthenticationHandler>("JwtCookieScheme", null);

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

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

app.MapControllers();
app.Run();