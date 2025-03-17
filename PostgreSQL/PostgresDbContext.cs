using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL;

public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ConnectionEntity> Connections { get; set; }
    public DbSet<TileEntity> Tiles { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
}