using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porter.Domain;

namespace Porter.Infra.Postgres.Repository
{
    public abstract  class DbContextBase : DbContext
    {
        
        public DbSet<Log> Logs { get; set; } = default!;



        public DbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }

     
    }

    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("log");
            builder.Property(e => e.Id)
                .HasColumnName("id");


            builder.Property(e => e.Action)
                .HasColumnName("action");

            builder.Property(e => e.Data)
                .HasColumnName("data");

            builder.Property(e => e.MethodName)
                .HasColumnName("methodname");

            builder.Property(e => e.EntityType)
                .HasColumnName("entitytype");


            builder.Property(e => e.CreateTime)
                .HasColumnName("createtime");

     
        }
    }
}
