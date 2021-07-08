using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SisCad.Application.Interfaces;
using SisCad.Application.Models.Client;
using SisCad.Application.Models.Contact;
using SisCad.Domain.Interfaces.Notification;

namespace SisCad.App.Controllers
{
    public class ContactController : BaseController
    {
        #region Fields

        private readonly IContactService _contactService;
        private readonly IClienteService _clienteService;

        #endregion Fields

        #region Constructors

        public ContactController(INotifier notifier,
            IContactService contactService, IClienteService clienteService)
            : base(notifier)
        {
            _contactService = contactService;
            _clienteService = clienteService;
        }

        #endregion Constructors

        #region Methods

        [Route("novo-contato/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> Create(Guid id)
        {
            var contactDataModel = new ContactDataModel()
            {
                Client = await _clienteService.Get(id)
            };

            return View(contactDataModel);
        }

        [Route("novo-contato/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Create(ContactDataModel request, Guid id)
        {
            if (ModelState.IsValid is false)
                return View(request);

            request.ClientId = id;

            await _contactService.Create(request);

            if (ValidOperation() is false)
                return View(request);

            return RedirectToAction("Edit", "Client", new { id = id });
        }

        [Route("excluir-contato/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var contact = await _contactService.Get(id);

            if (ValidOperation() is false)
                return NotFound();

            return View(contact);
        }

        [Route("excluir-contato/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid is false)
                return View(await _contactService.Get(id));

            var contact = await _contactService.Get(id);

            await _contactService.Delete(id);

            if (ValidOperation() is false)
                return View(await _contactService.Get(id));

            return RedirectToAction("Edit", "Client", new { id = contact.Client.Id });
        }

        [Route("dados-do-contato/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var contact = await _contactService.Get(id);

            if (ValidOperation() is false)
                return NotFound();

            return View(contact);
        }

        [Route("editar-contato/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var contact = await _contactService.Get(id);

            if (ValidOperation() is false)
                return NotFound();

            return View(contact);
        }

        [Route("editar-contato/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ContactDataModel request)
        {
            if (ModelState.IsValid is false)
                return View(request);

            request.Id = id;

            await _contactService.Update(request);

            if (ValidOperation() is false)
                return View(await _contactService.Get(id));

            var contact = await _contactService.Get(id);

            return RedirectToAction("Edit", "Client", new { id = contact.Client.Id });
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAll();

            return View(contacts);
        }

        #endregion Methods
    }
}