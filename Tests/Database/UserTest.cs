using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using PostgreSQL;
using PostgreSQL.Implementations;
using static NUnit.Framework.Assert;

namespace Tests.Database;

public class UserTest
{
    IUserAccess _userAccess;
    
    [SetUp]
    public void Setup()
    {
        var opt = new DbContextOptionsBuilder<PostgresDbContext>();
        var env = Environment.GetEnvironmentVariables();
        opt.UseNpgsql($"""
              Host={env["POSTGRES_URI"]};
              Database={env["POSTGRES_DATABASE"]};
              Username={env["POSTGRES_USERNAME"]};
              Password={env["POSTGRES_PASSWORD"]}
        """);
        _userAccess = new UserAccess(new PostgresDbContext(opt.Options));
    }

    [Test]
    public async Task GetUserRoles()
    {
        var userData = await _userAccess.GetUser(2);
        That(userData, Is.Not.Null);
        if (userData == null) return;
        foreach (var role in userData.Roles)
        {
            await Console.Out.WriteLineAsync(role.DisplayName);
        }
        That(userData.Roles, Is.Not.Empty);
    }
    
}