using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisCad.Domain.Entities;

namespace SisCad.Infrastructure.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder
                .HasKey(c => c.Id).IsClustered(false);

            builder
                .Property(c => c.CreatedOn)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(c => c.IsDeleted)
                .IsRequired();

            builder
                .Property(c => c.Name)
                .IsRequired();

            builder
                .Property(c => c.BirthDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(c => c.Address)
                .IsRequired();

            builder
                .Property(c => c.Status)
                .IsRequired();

            builder
                .Ignore(c => c.CascadeMode);

            builder
                .Ignore(c => c.ValidationResult);

            builder
                .HasQueryFilter(c => c.IsDeleted == false);
        }

        #endregion Methods
    }
}