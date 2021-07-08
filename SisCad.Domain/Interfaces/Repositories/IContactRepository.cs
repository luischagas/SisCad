using SisCad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SisCad.Domain.Enums.Contact;

namespace SisCad.Domain.Interfaces.Repositories
{
    public interface IContactRepository : IDisposable
    {
        #region Methods

        Task AddAsync(Contact contact);

        Task<IEnumerable<Contact>> GetAllAsync();

        Task<Contact> GetAsync(Guid id);

        Task<Contact> GetAsync(string value, EType type);

        void Update(Contact contact);

        #endregion Methods
    }
}