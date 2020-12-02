using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kwanza.Shop.Web.Controllers
{
    public class CompraUsuarioController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;

        public CompraUsuarioController(UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp)
        {
            _userManager = userManager;
            _interfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
        }


        [HttpPost("/api/AdicionarProdutoCarrinho")]
        public async Task<JsonResult> AdicionarProdutoCarrinho(string id, string nome, string qtd)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if(usuario != null)
            {
                await _interfaceCompraUsuarioApp.Add(
                    new CompraUsuario
                    {
                        IdProduto = Convert.ToInt32(id),
                        QtdCompra = Convert.ToInt32(qtd),
                        Situacao = EstadoCompra.Carrinho,
                        UserId = usuario.Id
                    });
                return Json(new { sucesso = true });
            }

            return Json(new { sucesso = false });
        }

        [HttpGet("/api/QtdProdutosCarrinho")]
        public async Task<JsonResult> QtdProdutosCarrinho()
        {
            var usuario = await _userManager.GetUserAsync(User);

            var qtd = 0;

            if(usuario != null)
            {
                qtd = await _interfaceCompraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(usuario.Id);

                return Json(new { sucesso = true, qtd = qtd });
            }

            return Json(new { sucesso = false, qtd = qtd });
        }
    }
}
