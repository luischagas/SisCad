using Microsoft.AspNetCore.Mvc;
using SisCad.Domain.Interfaces.Notification;

namespace SisCad.App.Controllers
{
    public class BaseController : Controller
    {
        #region Fields

        private readonly INotifier _notifier;

        #endregion Fields

        #region Constructors

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        #endregion Constructors

        #region Methods

        protected bool ValidOperation()
        {
            return _notifier.HasNotification() is false;
        }

        #endregion Methods
    }
}