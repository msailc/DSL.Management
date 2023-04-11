using Microsoft.EntityFrameworkCore;

namespace DSLManagement
{
    public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Pipeline> Pipelines { get; set; }
    public DbSet<PipelineStep> PipelineSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PipelineStep>()
            .HasKey(ps => ps.Id);
    }
}
}
