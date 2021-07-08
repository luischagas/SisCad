using SisCad.Application.Models.Client;
using SisCad.Domain.Entities;
using SisCad.Domain.Interfaces.Notification;
using SisCad.Domain.Interfaces.Repositories;
using SisCad.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SisCad.Application.Interfaces;
using SisCad.Application.Models.Contact;

namespace SisCad.Application.Services
{
    public class ClientService : AppService, IClienteService
    {
        #region Fields

        private readonly IClientRepository _clientRepository;

        #endregion Fields

        #region Constructors

        public ClientService(IUnitOfWork unitOfWork,
            INotifier notifier,
            IClientRepository clientRepository)
            : base(unitOfWork, notifier)
        {
            _clientRepository = clientRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task Create(ClientDataModel request)
        {
            var client = await _clientRepository.GetAsync(request.Name);

            if (client is not null)
            {
                Notify("Já existe um cliente cadastrado com este nome informado.");
                return;
            }

            var newClient = new Client(request.Name, request.BirthDate, request.Address, request.Status);

            if (newClient.IsValid())
                await _clientRepository.AddAsync(newClient);
            else
            {
                Notify(newClient.ValidationResult);
                return;
            }

            if (await CommitAsync() is false)
                Notify("Erro ao salvar dados.");
        }

        public async Task Delete(Guid id)
        {
            var client = await _clientRepository.GetAsync(id);

            if (client is null)
            {
                Notify("Dados do Cliente não encontrado.");
                return;
            }

            client.Delete();

            _clientRepository.Update(client);

            if (await CommitAsync() is false)
                Notify("Erro ao salvar dados.");
        }

        public async Task<ClientDataModel> Get(Guid id)
        {
            ClientDataModel clientModel = null;

            var client = await _clientRepository.GetAsync(id);

            if (client is null)
                Notify("Dados do Cliente não encontrado.");
            else
            {
                clientModel = new ClientDataModel()
                {
                    Id = client.Id,
                    Name = client.Name,
                    BirthDate = client.BirthDate,
                    Address = client.Address,
                    Status = client.Status
                };

                if (client.Contacts.Any())
                {
                    foreach (var contact in client.Contacts)
                    {
                        clientModel.Contatos.Add(new ContactDataModel()
                        {
                            Id = contact.Id,
                            Value = contact.Value,
                            Type = contact.Type
                        });
                    }
                }
            }

            return clientModel;
        }

        public async Task<IEnumerable<ClientDataModel>> GetAll()
        {
            var clients = await _clientRepository.GetAllAsync();

            var clientsModel = new List<ClientDataModel>();

            foreach (var client in clients)
            {
                clientsModel.Add(new ClientDataModel()
                {
                    Id = client.Id,
                    Name = client.Name,
                    BirthDate = client.BirthDate,
                    Address = client.Address,
                    Status = client.Status
                });
            }

            return clientsModel;
        }

        public async Task Update(ClientDataModel request)
        {
            var client = await _clientRepository.GetAsync(request.Id);

            if (client is null)
            {
                Notify("Dados do Cliente não encontrado.");
                return;
            }

            client.Update(request.Name, request.BirthDate, request.Address, request.Status);

            if (client.IsValid())
                _clientRepository.Update(client);
            else
            {
                Notify(client.ValidationResult);
                return;
            }

            if (await CommitAsync() is false)
                Notify("Erro ao salvar dados.");
        }

        #endregion Methods
    }
}