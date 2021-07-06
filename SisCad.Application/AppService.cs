using FluentValidation;
using FluentValidation.Results;
using SisCad.Domain.Entities;
using SisCad.Domain.Interfaces.Notification;
using SisCad.Domain.Notification;
using SisCad.Domain.Shared;
using System.Threading.Tasks;

namespace SisCad.Application
{
    public abstract class AppService
    {
        #region Fields

        private readonly INotifier _notifier;

        private readonly IUnitOfWork _unitOfWork;

        #endregion Fields

        #region Constructors

        public AppService(IUnitOfWork unitOfWork, INotifier notifier)
        {
            _unitOfWork = unitOfWork;
            _notifier = notifier;
        }

        #endregion Constructors

        #region Methods

        public async Task<bool> CommitAsync()
        {
            if (await _unitOfWork.CommitAsync())
                return await Task.FromResult(true);

            return await Task.FromResult(false);
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notify(error.ErrorMessage);
        }

        protected void Notify(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

        #endregion Methods
    }
}