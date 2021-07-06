using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisCad.Domain.Entities;

namespace SisCad.Infrastructure.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .ToTable("Contacts");

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
                .Property(c => c.Value)
                .IsRequired();

            builder
                .Property(c => c.Type)
                .IsRequired();

            builder
                .HasOne(c => c.Client)
                .WithMany(cl => cl.Contacts)
                .HasForeignKey(c => c.ClientId)
                .IsRequired();


            builder
                .Ignore(c => c.CascadeMode);

            builder
                .Ignore(c => c.ValidationResult);

            builder
                .HasQueryFilter(u => u.IsDeleted == false);
        }

        #endregion Methods
    }
}