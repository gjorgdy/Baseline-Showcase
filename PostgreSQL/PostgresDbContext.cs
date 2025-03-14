using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL;

public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<Tile> Tiles { get; set; }
    public DbSet<Role> Roles { get; set; }
}