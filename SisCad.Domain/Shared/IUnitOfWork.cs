using System;
using System.Threading.Tasks;

namespace SisCad.Domain.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methods

        Task<bool> CommitAsync();

        #endregion Methods
    }
}