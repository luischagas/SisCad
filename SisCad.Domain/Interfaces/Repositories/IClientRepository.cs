using SisCad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisCad.Domain.Interfaces.Repositories
{
    public interface IClientRepository : IDisposable
    {
        #region Methods

        Task AddAsync(Client client);

        Task<IEnumerable<Client>> GetAllAsync();

        Task<Client> GetAsync(Guid id);

        Task<Client> GetAsync(string name);

        void Update(Client client);

        #endregion Methods
    }
}