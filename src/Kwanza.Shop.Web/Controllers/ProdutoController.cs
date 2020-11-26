using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Kwanza.Shop.Web.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly InterfaceProdutoApp _interfaceProdutoApp;

        public ProdutoController(InterfaceProdutoApp interfaceProdutoApp)
        {
            _interfaceProdutoApp = interfaceProdutoApp;
        }

        // GET: ProdutoController
        public async Task<IActionResult> Index()
        {
            return View(await _interfaceProdutoApp.List());
        }

        // GET: ProdutoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // GET: ProdutoController/Create
        public  async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                await _interfaceProdutoApp.AddProduct(produto);
                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);
                    }

                    return View("Edit", produto);
                }
            }
            catch
            {
                return View("Edit", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _interfaceProdutoApp.UpdateProduct(produto);
                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);
                    }

                    return View("Edit", produto);
                }
            }
            catch
            {
                return View("Edit", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _interfaceProdutoApp.GetEntityById(id);

                await _interfaceProdutoApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
