using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porter.Common.Domain;

namespace Porter.Common.EF.Repository
{
    public class DbContextBase : DbContext
    {
        public DbSet<Log> Logs { get; set; } = default!;

   
        public DbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogConfiguration());

            base.OnModelCreating(modelBuilder);
        }

     
    }

    public class LogConfiguration : EntityConfigurationBase<Log>
    {
        public LogConfiguration() : base("log")
        {
        }

        public new void Configure(EntityTypeBuilder<Log> builder)
        {
            base.Configure(builder);


            builder.Property(e => e.Action)
                .HasColumnName("action");

            builder.Property(e => e.Data)
                .HasColumnName("data")
                .HasColumnType("jsonb"); 

            builder.Property(e => e.MethodName)
                .HasColumnName("methodname");

            builder.Property(e => e.EntityType)
                .HasColumnName("entitytype");




        }
    }


}
