using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SisCad.Domain.Entities;
using SisCad.Domain.Enums.Contact;
using SisCad.Domain.Interfaces.Repositories;
using SisCad.Infrastructure.Context;

namespace SisCad.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        #region Fields

        private readonly SisCadContext _db;
        private readonly DbSet<Contact> _contacts;
        private bool _disposed;

        #endregion Fields

        #region Constructors

        public ContactRepository(SisCadContext db)
        {
            _db = db;
            _contacts = _db.Set<Contact>();
            _disposed = false;
        }

        public async Task AddAsync(Contact contact)
        {
            await _contacts
                .AddAsync(contact);
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _contacts
                .ToListAsync();
        }

      

        public async Task<Contact> GetAsync(Guid id)
        {
            return await _contacts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contact> GetAsync(string value, EType type)
        {
            return await _contacts
                .FirstOrDefaultAsync(c => c.Value == value && c.Type == type);
        }

        public void Update(Contact contact)
        {
            _db.Update(contact);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        #endregion Constructors
    }
}