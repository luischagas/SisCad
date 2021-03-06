using FluentValidation;
using FluentValidation.Results;
using SequentialGuid;
using System;

namespace SisCad.Domain.Entities
{
    public abstract class Entity<T> : AbstractValidator<T>
        where T : Entity<T>
    {
        #region Constructors

        protected Entity()
        {
            Id = SequentialGuidGenerator.Instance.NewGuid();
            IsDeleted = false;
            CreatedOn = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; protected set; }

        public DateTime CreatedOn { get; protected set; }
       
        public bool IsDeleted { get; protected set; }

        public ValidationResult ValidationResult { get; protected set; }

        #endregion Properties

        #region Methods

        public void Delete()
        {
            IsDeleted = true;
        }

        public abstract bool IsValid();
        protected void AddErrors(ValidationResult validateResult)
        {
            foreach (var error in validateResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

        #endregion Methods
    }
}