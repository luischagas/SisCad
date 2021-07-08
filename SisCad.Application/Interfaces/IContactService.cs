using SisCad.Application.Models.Contact;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisCad.Application.Interfaces
{
    public interface IContactService
    {
        #region Methods

        Task Create(ContactDataModel request);

        Task Delete(Guid id);

        Task<ContactDataModel> Get(Guid id);

        Task<IEnumerable<ContactDataModel>> GetAll();

        Task Update(ContactDataModel request);

        #endregion Methods
    }
}