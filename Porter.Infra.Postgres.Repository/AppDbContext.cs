using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porter.Domain;

namespace Porter.Infra.Postgres.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserPorter> Users { get; set; } = default!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserPorterConfiguration());
        }

     
    }

    public class UserPorterConfiguration : IEntityTypeConfiguration<UserPorter>
    {
        public void Configure(EntityTypeBuilder<UserPorter> builder)
        {
            builder.ToTable("userporter");
            builder.Property(e => e.Id)
                .HasColumnName("id");


            builder.Property(e => e.Login)
                .HasColumnName("login");

            builder.Property(e => e.CreateTime)
                .HasColumnName("createtime");
        }
    }
}
