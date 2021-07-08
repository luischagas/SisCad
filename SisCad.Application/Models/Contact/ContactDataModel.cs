using SisCad.Domain.Enums.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SisCad.Application.Models.Client;

namespace SisCad.Application.Models.Contact
{
    public class ContactDataModel
    {
        #region Properties

        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Cliente")]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Descrição")]
        public string Value { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Tipo")]
        public EType Type { get; set; }

        public ClientDataModel Client { get; set; }

        public IEnumerable<ClientDataModel> Clients { get; set; }

        #endregion Properties
    }
}