using Microsoft.EntityFrameworkCore;
using SisCad.Domain.Entities;
using System.Linq;

namespace SisCad.Infrastructure.Context
{
    public class SisCadContext : DbContext
    {
        #region Properties

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        #endregion Properties

        #region Constructors

        public SisCadContext(DbContextOptions options) : base(options)
        {
        }

        #endregion Constructors

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        }

        #endregion Methods
    }
}