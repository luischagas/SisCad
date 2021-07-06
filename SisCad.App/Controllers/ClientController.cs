using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SisCad.Application.Interfaces;
using SisCad.Application.Models.Client;
using SisCad.Domain.Interfaces.Notification;

namespace SisCad.App.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClienteService _clienteService;

        public ClientController(INotifier notifier, 
            IClienteService clienteService)
            : base(notifier)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clienteService.GetAll();

            return View(clients);
        }

        [Route("dados-do-cliente/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = await _clienteService.Get(id);

            if (ValidOperation() is false)
                return NotFound();

            return View(client);
        }

        [Route("novo-cliente")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-cliente")]
        [HttpPost]
        public async Task<IActionResult> Create(ClientDataModel request)
        {
            if (ModelState.IsValid is false) 
                return View(request);

            await _clienteService.Create(request);

            if (ValidOperation() is false)
                return View(request);

            return RedirectToAction("Index");
        }

        [Route("editar-vliente/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = await _clienteService.Get(id);

            if (ValidOperation() is false)
                return NotFound();

            return View(client);
        }
        [Route("editar-cliente/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ClientDataModel request)
        {
            if (ModelState.IsValid is false)
                return View(request);

            request.Id = id;

            await _clienteService.Update(request);

            if (ValidOperation() is false)
                return View(await _clienteService.Get(id));

            return RedirectToAction("Index");
        }


        [Route("excluir-cliente/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await _clienteService.Get(id);

            if (ValidOperation() is false)
                return NotFound();

            return View(client);
        }


        [Route("excluir-cliente/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid is false)
                return View(await _clienteService.Get(id));

            await _clienteService.Delete(id);

            if (ValidOperation() is false)
                return View(await _clienteService.Get(id));

            return RedirectToAction("Index");
        }
    }
}
