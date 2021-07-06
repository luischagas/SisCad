using System;
using System.Collections.Generic;
using System.Text;

namespace SisCad.Domain.Notification
{
    public class Notification
    {
        #region Constructors

        public Notification(string message)
        {
            Message = message;
        }

        #endregion Constructors

        #region Properties

        public string Message { get; }

        #endregion Properties
    }
}
