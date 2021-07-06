using SisCad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisCad.Domain.Interfaces.Repositories
{
    public interface IContactRepository : IDisposable
    {
        #region Methods

        Task AddAsync(Contact contact);

        Task<IEnumerable<Contact>> GetAllAsync();

        Task<Contact> GetAsync(Guid id);

        void Update(Contact contact);

        #endregion Methods
    }
}