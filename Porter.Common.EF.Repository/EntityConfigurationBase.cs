using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porter.Common.Domain;

namespace Porter.Common.EF.Repository
{
    public abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : BaseDomain
    {
        private readonly string _tableName;

        protected EntityConfigurationBase(string tableName)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(tableName);
            _tableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            

            builder.ToTable(_tableName);
            builder.HasKey(e => e.Id);

            builder.Property(x => x.Id)
           .HasColumnName("id") 
           .ValueGeneratedOnAdd();


            builder.Property(e => e.CreateTime)
            .HasColumnName("createtime");


        }
    }
}
