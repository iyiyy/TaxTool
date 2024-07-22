using Microsoft.EntityFrameworkCore;
using TaxTool.Model.Entity;

namespace TaxTool.Model;

public class TaxContext : DbContext
{
    public TaxContext(DbContextOptions<TaxContext> options) : base(options)
    {
    }

    public TaxContext()
    {
    }

    // NOTE: the DbSets are virtual because moq cannot... mock them otherwise
    public virtual DbSet<Municipality> Municipalities { get; set; }
    public virtual DbSet<TaxRecord> TaxRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
    }
}