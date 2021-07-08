using SisCad.Application.Models.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisCad.Application.Models.Client
{
    public class ClientDataModel
    {
        #region Constructors

        public ClientDataModel()
        {
            Contatos = new List<ContactDataModel>();
        }

        #endregion Constructors

        #region Properties

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Address { get; set; }

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        public List<ContactDataModel> Contatos { get; set; }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Status { get; set; }

        #endregion Properties
    }
}