using FluentValidation;
using SisCad.Domain.Enums.Contact;
using System;

namespace SisCad.Domain.Entities
{
    public class Contact : Entity<Contact>
    {
        #region Properties

        public string Value { get; protected set; }
        public ETypePhone Type { get; protected set; }
        public Client Client { get; set; }
        public Guid ClientId { get; set; }
        
        #endregion Properties

        #region Methods

        public override bool IsValid()
        {
            ValidateValue();
            ValidateType();

            AddErrors(Validate(this));

            return ValidationResult.IsValid;
        }

        private void ValidateType()
        {
            RuleFor(d => d.Type)
                .IsInEnum()
                .WithMessage("O tipo deve ter um valor válido");
        }

        private void ValidateValue()
        {
            RuleFor(d => d.Value)
                .NotEmpty()
                .WithMessage("O valor deve ser preenchido");
        }

        #endregion Methods
    }
}