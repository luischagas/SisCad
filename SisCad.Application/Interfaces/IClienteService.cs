using SisCad.Application.Models.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisCad.Application.Interfaces
{
    public interface IClienteService
    {
        #region Methods

        Task Create(ClientDataModel request);

        Task Delete(Guid id);

        Task<ClientDataModel> Get(Guid id);

        Task<IEnumerable<ClientDataModel>> GetAll();

        Task Update(ClientDataModel request);

        #endregion Methods
    }
}