using DSLManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DSLManagement
{
    public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Pipeline> Pipelines { get; set; }
    public DbSet<PipelineStep> PipelineSteps { get; set; }
    public DbSet<PipelineStepParameter> PipelineStepParameters { get; set; }
    public DbSet<PipelineExecution> PipelineExecutions { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pipeline>()
            .HasMany(p => p.Steps)
            .WithOne(ps => ps.Pipeline)
            .HasForeignKey(ps => ps.PipelineId);

        modelBuilder.Entity<PipelineStep>()
            .HasMany(ps => ps.Parameters)
            .WithOne(psp => psp.PipelineStep)
            .HasForeignKey(psp => psp.PipelineStepId);

        modelBuilder.Entity<PipelineStep>()
            .HasOne(ps => ps.Pipeline)
            .WithMany(p => p.Steps)
            .HasForeignKey(ps => ps.PipelineId);

        modelBuilder.Entity<PipelineStepParameter>()
            .HasKey(psp => psp.Id);
        
            
    }
}
}
