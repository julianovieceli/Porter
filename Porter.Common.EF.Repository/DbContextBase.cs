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
                .HasColumnName("data")
                .HasColumnType("jsonb"); 

            builder.Property(e => e.MethodName)
                .HasColumnName("methodname");

            builder.Property(e => e.EntityType)
                .HasColumnName("entitytype");


            builder.Property(e => e.CreateTime)
                .HasColumnName("createtime");


        }
    }


}
