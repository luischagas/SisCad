using FluentValidation;
using System;
using System.Collections.Generic;

namespace SisCad.Domain.Entities
{
    public class Client : Entity<Client>
    {
        #region Fields

        private IList<Contact> _contacts;

        #endregion Fields

        public Client(string name, DateTime birthDate, string address, bool status)
        {
            Name = name;
            BirthDate = birthDate;
            Address = address;
            Status = status;
            _contacts = new List<Contact>();

        }

        protected Client()
        {
            _contacts = new List<Contact>();
        }


        #region Properties

        public string Name { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string Address { get; protected set; }
        public bool Status { get; protected set; }

        public IEnumerable<Contact> Contacts => _contacts;

        #endregion Properties

        #region Methods

        public void Update(string name, DateTime birthDate, string address, bool status)
        {
            Name = name;
            BirthDate = birthDate;
            Address = address;
            Status = status;
        }

        public void UpdateStatus(bool status)
        {
            Status = status;
        }

        public override bool IsValid()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateAddress();

            AddErrors(Validate(this));

            return ValidationResult.IsValid;
        }

        private void ValidateAddress()
        {
            RuleFor(d => d.Address)
                .NotEmpty()
                .WithMessage("O endereço deve ser preenchido");
        }

        private void ValidateBirthDate()
        {
            RuleFor(d => d.BirthDate)
                .LessThan(p => DateTime.Now)
                .WithMessage("A data deve estar no passado");
        }

        private void ValidateName()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("O nome deve ser preenchido");
        }

        #endregion Methods
    }
}