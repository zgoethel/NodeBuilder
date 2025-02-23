using Microsoft.EntityFrameworkCore;
using TestNodeBuilder.Models;

namespace TestNodeBuilder.Ef;

public class AppDbContext : DbContext
{
    public const string DB_FILE = "data.db";

    public DbSet<Grammar> Grammars { get; set; }
    public DbSet<TokenGroup> TokenGroups { get; set; }
    public DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DB_FILE}");
}
