using SisCad.Application.Interfaces;
using SisCad.Application.Models.Contact;
using SisCad.Domain.Entities;
using SisCad.Domain.Interfaces.Notification;
using SisCad.Domain.Interfaces.Repositories;
using SisCad.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SisCad.Application.Models.Client;

namespace SisCad.Application.Services
{
    public class ContactService : AppService, IContactService
    {
        #region Fields

        private readonly IClientRepository _clientRepository;
        private readonly IContactRepository _contactRepository;
        #endregion Fields

        #region Constructors

        public ContactService(IUnitOfWork unitOfWork,
            INotifier notifier,
            IClientRepository clientRepository,
            IContactRepository contactRepository)
            : base(unitOfWork, notifier)
        {
            _contactRepository = contactRepository;
            _clientRepository = clientRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task Create(ContactDataModel request)
        {
            var contact = await _contactRepository.GetAsync(request.Value, request.Type);

            if (contact is not null)
            {
                Notify("Já existe um contato cadastrado com esta descrição e este tipo.");
                return;
            }

            var newContact = new Contact(request.Value, request.Type, request.ClientId);

            if (newContact.IsValid())
                await _contactRepository.AddAsync(newContact);
            else
            {
                Notify(newContact.ValidationResult);
                return;
            }

            if (await CommitAsync() is false)
                Notify("Erro ao salvar dados.");
        }

        public async Task Delete(Guid id)
        {
            var contact = await _contactRepository.GetAsync(id);

            if (contact is null)
            {
                Notify("Dados do Contato não encontrado.");
                return;
            }

            contact.Delete();

            _contactRepository.Update(contact);

            if (await CommitAsync() is false)
                Notify("Erro ao salvar dados.");
        }

        public async Task<ContactDataModel> Get(Guid id)
        {
            ContactDataModel contactModel = null;

            var contact = await _contactRepository.GetAsync(id);

            if (contact is null)
                Notify("Dados do Contato não encontrado.");
            else
            {
                contactModel = new ContactDataModel()
                {
                    Id = contact.Id,
                    Value = contact.Value,
                    Type = contact.Type
                };

                if (contact.Client is not null)
                {
                    contactModel.Client = new ClientDataModel()
                    {
                        Id = contact.Client.Id,
                        Address = contact.Client.Address,
                        BirthDate = contact.Client.BirthDate,
                        Name = contact.Client.Name,
                        Status = contact.Client.Status
                    };
                }
            }

            return contactModel;
        }

        public async Task<IEnumerable<ContactDataModel>> GetAll()
        {
            var contacts = await _contactRepository.GetAllAsync();

            var contactsModel = new List<ContactDataModel>();

            foreach (var contact in contacts)
            {
                contactsModel.Add(new ContactDataModel()
                {
                    Id = contact.Id,
                    Value = contact.Value,
                    Type = contact.Type
                });
            }

            return contactsModel;
        }

        public async Task Update(ContactDataModel request)
        {
            var contact = await _contactRepository.GetAsync(request.Id);

            if (contact is null)
            {
                Notify("Dados do Contato não encontrado.");
                return;
            }

            contact.Update(request.Value, request.Type);

            if (contact.IsValid())
                _contactRepository.Update(contact);
            else
            {
                Notify(contact.ValidationResult);
                return;
            }

            if (await CommitAsync() is false)
                Notify("Erro ao salvar dados.");
        }

        #endregion Methods
    }
}