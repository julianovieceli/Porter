using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porter.Domain;

namespace Porter.Infra.Postgres.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = default!;

        public DbSet<Room> Rooms { get; set; } = default!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
        }

     
    }

    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("client");
            builder.Property(e => e.Id)
                .HasColumnName("id");


            builder.Property(e => e.Docto)
                .HasColumnName("docto");
            
            builder.Property(e => e.Name)
                .HasColumnName("name");


            builder.Property(e => e.CreateTime)
                .HasColumnName("createtime");
        }
    }
}
