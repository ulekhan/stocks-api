using Microsoft.EntityFrameworkCore;
using StocksData.Domain;

namespace StocksData.Data.Db;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HistoricalBar>().HasIndex(e => e.Symbol);
    }

    public DbSet<HistoricalBar> Bars { get; set; }
}