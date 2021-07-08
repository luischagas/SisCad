using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SisCad.Domain.Entities;
using SisCad.Domain.Interfaces.Repositories;
using SisCad.Infrastructure.Context;

namespace SisCad.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        #region Fields

        private readonly SisCadContext _db;
        private readonly DbSet<Client> _clients;
        private bool _disposed;

        #endregion Fields

        #region Constructors

        public ClientRepository(SisCadContext db)
        {
            _db = db;
            _clients = _db.Set<Client>();
            _disposed = false;
        }

        public async Task AddAsync(Client client)
        {
            await _clients
                .AddAsync(client);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _clients
                .ToListAsync();
        }

        public async Task<Client> GetAsync(Guid id)
        {
            return await _clients
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Client> GetAsync(string name)
        {
            return await _clients
                .FirstOrDefaultAsync(d => d.Name == name);
        }

        public void Update(Client client)
        {
            _db.Update(client);
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