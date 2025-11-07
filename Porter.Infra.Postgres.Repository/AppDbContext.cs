using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal.Common.Infra.EF.Repository;
using Porter.Domain;

namespace Porter.Infra.Postgres.Repository
{
    public class AppDbContext : DbContextBase
    {
        public DbSet<Client> Clients { get; set; } = default!;

        public DbSet<Room> Rooms { get; set; } = default!;

        
        public DbSet<Booking> Bookings { get; set; } = default!;


        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

  
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            base.OnModelCreating(modelBuilder);
        }

     
    }

    public class ClientConfiguration : EntityConfigurationBase<Client>
    {
        public ClientConfiguration() : base("client")
        {
        }

        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);


            builder.Property(e => e.Docto)
                .HasColumnName("docto");
            
            builder.Property(e => e.Name)
                .HasColumnName("name");


        }
    }

    public class RoomConfiguration : EntityConfigurationBase<Room>
    {


        public RoomConfiguration() : base("room")
        {
        }
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .HasColumnName("name");


            //builder.HasMany(b => b.Bookings)
            //.WithOne(b => b.Room)       
            //.HasForeignKey(b => b.RoomId)
            //.IsRequired();
        }
    }

    public class BookingConfiguration : EntityConfigurationBase<Booking>
    {
        public BookingConfiguration() : base("booking")
        {
        }

        public override  void Configure(EntityTypeBuilder<Booking> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Obs)
                .HasColumnName("obs");


            builder.Property(e => e.StartDate)
                .HasColumnName("startdate");


            builder.Property(e => e.EndDate)
                .HasColumnName("enddate");


            builder.Property(e => e.DeletedDate)
                .HasColumnName("deleteddate");

            builder.Property(e => e.RoomId)
                .HasColumnName("roomid");

            builder.Property(e => e.ReservedById)
                .HasColumnName("reservedbyid");


            builder.HasOne(b => b.Room)
               .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId);


            builder.HasOne(e => e.ReservedBy)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ReservedById);


        }
    }

    


}
