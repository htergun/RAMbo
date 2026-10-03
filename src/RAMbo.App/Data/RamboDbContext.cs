using Microsoft.EntityFrameworkCore;
using RAMbo.App.Models;

namespace RAMbo.App.Data;

public class RamboDbContext : DbContext
{
    public RamboDbContext(DbContextOptions<RamboDbContext> options) : base(options) { }

    public DbSet<Simulation> Simulations => Set<Simulation>();
    public DbSet<PageReference> PageReferences => Set<PageReference>();
    public DbSet<StepResult> Results => Set<StepResult>();
    public DbSet<ProcessInfo> Processes => Set<ProcessInfo>();
    public DbSet<MemoryBlock> MemoryBlocks => Set<MemoryBlock>();
    public DbSet<Allocation> Allocations => Set<Allocation>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<StepResult>().ToTable("Results");
        b.Entity<ProcessInfo>().ToTable("Processes");
        b.Entity<Simulation>().Property(s => s.HitRatio).HasPrecision(5, 2);

        // SQL şemasında Allocations -> Processes / MemoryBlocks için cascade yok; EF de aynısını yapsın.
        b.Entity<Allocation>().HasOne<ProcessInfo>().WithMany()
            .HasForeignKey(a => a.ProcessId).OnDelete(DeleteBehavior.NoAction);
        b.Entity<Allocation>().HasOne<MemoryBlock>().WithMany()
            .HasForeignKey(a => a.MemoryBlockId).OnDelete(DeleteBehavior.NoAction);
    }
}
