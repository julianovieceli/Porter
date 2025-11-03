using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porter.Domain;

namespace Porter.Infra.Postgres.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = default!;

        public DbSet<Room> Rooms { get; set; } = default!;

        
        public DbSet<Booking> Bookings { get; set; } = default!;


        public DbSet<Log> Logs { get; set; } = default!;




        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            modelBuilder.ApplyConfiguration(new LogConfiguration());

            base.OnModelCreating(modelBuilder);
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

            //builder.HasMany(c => c.Bookings)
            //.WithOne(b => b.ReservedBy)
            //.HasForeignKey(b => b.ReservedById)
            //.IsRequired();
        }
    }

    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("room");
            builder.Property(e => e.Id)
                .HasColumnName("id");


            builder.Property(e => e.Name)
                .HasColumnName("name");


            builder.Property(e => e.CreateTime)
                .HasColumnName("createtime");

            //builder.HasMany(b => b.Bookings)
            //.WithOne(b => b.Room)       
            //.HasForeignKey(b => b.RoomId)
            //.IsRequired();
        }
    }

    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("booking");
            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Obs)
                .HasColumnName("obs");


            builder.Property(e => e.CreateTime)
                .HasColumnName("createtime");

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
